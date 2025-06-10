using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProdActivity.Core
{
    public class WebSocketService : IWebSocketService
    {
        private ClientWebSocket _webSocket;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly int _bufferSize = 8192;
        private string _lastUri;

        public event EventHandler<string> OnStatusChanged;
        public event EventHandler<WebSocketMessageEventArgs> OnMessageReceived;

        public WebSocketService()
        {
            InitWebSocket();
        }

        private void InitWebSocket()
        {
            _webSocket = new ClientWebSocket();
            _webSocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(30); 
            //_webSocket.Options.SetBuffer(_bufferSize, _bufferSize);
            _webSocket.Options.UseDefaultCredentials = true;
            _webSocket.Options.Credentials = CredentialCache.DefaultNetworkCredentials;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task StartListeningAsync(string uri)
        {
            _lastUri = uri;

            try
            {
                if (_webSocket.State == WebSocketState.Open)
                {
                    await StopListeningAsync();
                }

                InitWebSocket();

                var uriBuilder = new UriBuilder(uri);
                if (uriBuilder.Host == "localhost")
                {
                    uriBuilder.Host = "127.0.0.1"; 
                }

                await _webSocket.ConnectAsync(uriBuilder.Uri, _cancellationTokenSource.Token);
                RaiseStatusChanged("Bağlantı başarılı!");

                _ = Task.Run(ReceiveMessages);
            }
            catch (Exception ex)
            {
                RaiseStatusChanged($"Bağlantı hatası: {ex.Message}");
                throw;
            }
        }

        private void RaiseStatusChanged(string message)
        {
            OnStatusChanged?.Invoke(this, message);
        }

        private async Task ReceiveMessages()
        {
            var buffer = new byte[_bufferSize];
            var messageBuilder = new StringBuilder();

            while (_webSocket.State == WebSocketState.Open)
            {
                try
                {
                    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15)); 
                    using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                        _cancellationTokenSource.Token, cts.Token);

                    var result = await _webSocket.ReceiveAsync(
                        new ArraySegment<byte>(buffer),
                        linkedCts.Token);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        messageBuilder.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));

                        if (result.EndOfMessage)
                        {
                            var message = messageBuilder.ToString();
                            messageBuilder.Clear();

                            try
                            {
                                var json = JObject.Parse(message);
                                OnMessageReceived?.Invoke(this, new WebSocketMessageEventArgs(
                                    json["type"]?.ToString(),
                                    json["timestamp"]?.ToString(),
                                    message));
                            }
                            catch (Exception ex)
                            {
                                RaiseStatusChanged($"JSON parsing hatası: {ex.Message}");
                            }
                        }
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        RaiseStatusChanged("Sunucu bağlantıyı kapattı.");
                        await StopListeningAsync();
                    }
                }
                catch (OperationCanceledException)
                {
                    RaiseStatusChanged("Veri alma zaman aşımına uğradı. Yeniden bağlanıyor...");
                    await RestartConnection();
                    break;
                }
                catch (Exception ex)
                {
                    RaiseStatusChanged($"Mesaj alma hatası: {ex.Message}");
                    await RestartConnection();
                    break;
                }
            }
        }

        private async Task RestartConnection()
        {
            await StopListeningAsync();
            await Task.Delay(2000); 
            if (!string.IsNullOrWhiteSpace(_lastUri))
            {
                await StartListeningAsync(_lastUri);
            }
        }

        public async Task StopListeningAsync()
        {
            try
            {
                if (_webSocket.State == WebSocketState.Open)
                {
                    await _webSocket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        "Closing",
                        CancellationToken.None);
                }
            }
            catch { }

            _webSocket?.Dispose();
            _cancellationTokenSource?.Cancel();
            RaiseStatusChanged("Bağlantı kapatıldı.");
        }
    }

    public class WebSocketMessageEventArgs : EventArgs
    {
        public string MessageType { get; }
        public string Timestamp { get; }
        public string Message { get; }

        public WebSocketMessageEventArgs(string messageType, string timestamp, string message)
        {
            MessageType = messageType;
            Timestamp = timestamp;
            Message = message;
        }
    }
}
