using System;
using System.Threading.Tasks;

namespace ProdActivity.Core
{
    public interface IWebSocketService
    {
        /// <summary>
        /// Belirtilen URI üzerinden WebSocket bağlantısını başlatır ve mesajları dinlemeye başlar.
        /// </summary>
        /// <param name="uri">WebSocket sunucu URI'si.</param>
        Task StartListeningAsync(string uri);

        /// <summary>
        /// Mevcut WebSocket bağlantısını durdurur ve bağlantıyı kapatır.
        /// </summary>
        Task StopListeningAsync();

        /// <summary>
        /// Bağlantı durumu değiştiğinde tetiklenen olay.
        /// </summary>
        event EventHandler<string> OnStatusChanged;

        /// <summary>
        /// WebSocket üzerinden bir mesaj alındığında tetiklenen olay.
        /// </summary>
        event EventHandler<WebSocketMessageEventArgs> OnMessageReceived;
    }
}
