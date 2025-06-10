using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using ProdActivity.Core;
using ProdActivity.Entities;
using ProdActivity.DataAccess.Repositories;

namespace ProdActivity.WinForms
{
    public partial class MainForm : Form
    {
        private readonly IWebSocketService _webSocketService;
        private readonly object _lock = new object();
        private volatile bool _inProgress = false;

        public MainForm()
        {
            InitializeComponent();
            _webSocketService = new WebSocketService();

            _webSocketService.OnMessageReceived += WebSocketService_OnMessageReceived;
            _webSocketService.OnStatusChanged += WebSocketService_OnStatusChanged;
        }

        #region WebSocket Event Handlers

        private void WebSocketService_OnStatusChanged(object sender, string message)
        {
            SafeInvoke(() =>
            {
                lblStatus.Text = $"Bağlantı durumu: {message}";
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            });
        }

        private void WebSocketService_OnMessageReceived(object sender, WebSocketMessageEventArgs e)
        {
            if (_inProgress) return;

            lock (_lock)
            {
                _inProgress = true;

                try
                {
                    var rawData = JsonSerializer.Deserialize<RawRoot>(e.Message);

                    if (rawData != null && rawData.type == "detection" && rawData.detections != null)
                    {
                        var root = new Root
                        {
                            Type = rawData.type,
                            Timestamp = DateTimeOffset.FromUnixTimeSeconds((long)rawData.timestamp).UtcDateTime,
                            Detections = new List<Detection>()
                        };

                        var detectionText = new StringBuilder("Tespit Edilen Nesneler:\n");

                        foreach (var d in rawData.detections)
                        {
                            detectionText.AppendLine($"- {d.label} (Güven: {d.confidence:P})");

                            root.Detections.Add(new Detection
                            {
                                Label = d.label,
                                Confidence = d.confidence,
                                BBox = JsonSerializer.Serialize(d.bbox) // int[]'i string yap
                            });
                        }

                        SafeInvoke(() =>
                        {
                            lblDetections.Text = detectionText.ToString();
                            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Yeni tespit:\n{detectionText}\n");
                        });

                        var repo = new DetectionRepository();
                        repo.SaveDetectionData(root);
                    }
                }
                catch (Exception ex)
                {
                    SafeInvoke(() =>
                    {
                        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] JSON parsing hatası: {ex.Message}\n");
                    });
                }
                finally
                {
                    _inProgress = false;
                }
            }
        }



        #endregion

        #region Button Events

        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            BtnConnect.Enabled = false;
            BtnDisconnect.Enabled = true;

            try
            {
                await _webSocketService.StartListeningAsync("ws://localhost:8765");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BtnConnect.Enabled = true;
                BtnDisconnect.Enabled = false;
            }
        }

        private async void BtnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                await _webSocketService.StopListeningAsync();

                SafeInvoke(() =>
                {
                    lblStatus.Text = "Bağlantı kapalı";
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] WebSocket bağlantısı kapatıldı.{Environment.NewLine}");
                    BtnConnect.Enabled = true;
                    BtnDisconnect.Enabled = false;
                });
            }
            catch (Exception ex)
            {
                // Hata durumunda log kaydı yapılır
                SafeInvoke(() =>
                {
                    MessageBox.Show($"Bağlantı kesme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }


        #endregion

        #region Helpers

        private void SafeInvoke(Action action)
        {
            if (InvokeRequired)
                BeginInvoke(action);
            else
                action();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _webSocketService.StopListeningAsync().Wait();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        #endregion
    }

    //#region Model Classes

    //public class Root
    //{
    //    public string type { get; set; }
    //    public double timestamp { get; set; }
    //    public List<Detection> detections { get; set; }
    //}

    //public class Detection
    //{
    //    public string label { get; set; }
    //    public double confidence { get; set; }
    //    public int[] bbox { get; set; }
    //}

    //#endregion
}
