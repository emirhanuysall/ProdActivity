using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using ProdActivity.Entities;
using ProdActivity.DataAccess.Repositories;
using ProdActivity.Core;

namespace ProdActivity.WinForms
{
    public partial class OrderManagementForm : Form
    {
        private readonly DetectionRepository _detectionRepository;
        private IWebSocketService _webSocketService;
        private readonly object _lock = new object();
        private volatile bool _inProgress = false;

        public OrderManagementForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();
            _detectionRepository = new DetectionRepository();
        }

        private async void DashboardForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (CurrentUser.User != null)
                {
                    lblUsername.Text = CurrentUser.User.Name;
                    lblRole.Text = CurrentUser.User.UserRole?.Name ?? "Rol Yok";
                    Logger.Info($"Kullanıcı girişi yapıldı: {CurrentUser.User.Name} ({CurrentUser.User.UserRole?.Name ?? "Rol Yok"})");
                }

                LoadDetectionData();
                Logger.Info("Tespit verileri yüklendi.");

                // WebSocket başlat
                _webSocketService = new WebSocketService();
                _webSocketService.OnMessageReceived += WebSocketService_OnMessageReceived;
                _webSocketService.OnStatusChanged += WebSocketService_OnStatusChanged;

                try
                {
                    await _webSocketService.StartListeningAsync("ws://localhost:8765");
                    Logger.Info("WebSocket bağlantısı başlatıldı.");
                }
                catch (Exception ex)
                {
                    Logger.Error($"WebSocket bağlantı hatası: {ex.Message}");
                    MessageBox.Show($"Bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                DetectionRepository.OnNewDetectionSaved += DetectionRepository_OnNewDetectionSaved;

                chkLastHour.CheckedChanged += FilterCheckbox_CheckedChanged;
                chkLastDay.CheckedChanged += FilterCheckbox_CheckedChanged;
                chkLastWeek.CheckedChanged += FilterCheckbox_CheckedChanged;
                chkLastMonth.CheckedChanged += FilterCheckbox_CheckedChanged;
                chkLastYear.CheckedChanged += FilterCheckbox_CheckedChanged;

                dtpStart.Enabled = true;
                dtpEnd.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Form yüklenirken hata oluştu: {ex.Message}");
                MessageBox.Show($"Form yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WebSocketService_OnStatusChanged(object sender, string message)
        {
            SafeInvoke(() =>
            {
                lblConnectionStatus.Text = $"WebSocket: {message}";

                if (message.Contains("connected", StringComparison.OrdinalIgnoreCase))
                {
                    Logger.Info("WebSocket bağlantısı başarıyla kuruldu.");
                }
                else if (message.Contains("disconnected", StringComparison.OrdinalIgnoreCase) || message.Contains("closed", StringComparison.OrdinalIgnoreCase))
                {
                    Logger.Warning("WebSocket bağlantısı kesildi.");
                }
                else if (message.Contains("error", StringComparison.OrdinalIgnoreCase))
                {
                    Logger.Error($"WebSocket bağlantı hatası: {message}");
                }
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
                        Logger.Info($"Yeni tespit alındı: {rawData.detections.Count} adet nesne tespit edildi.");
                        
                        var root = new Root
                        {
                            Type = rawData.type,
                            Timestamp = DateTimeOffset.FromUnixTimeSeconds((long)rawData.timestamp).UtcDateTime,
                            Detections = new List<Detection>()
                        };

                        var detectionText = new StringBuilder();

                        foreach (var d in rawData.detections)
                        {
                            detectionText.AppendLine($"- {d.label} ({d.confidence:P})");

                            root.Detections.Add(new Detection
                            {
                                Label = d.label,
                                Confidence = d.confidence,
                                BBox = JsonSerializer.Serialize(d.bbox)
                            });
                        }

                        SafeInvoke(() =>
                        {
                            lblDetectedObjects.Text = detectionText.ToString();
                        });

                        _detectionRepository.SaveDetectionData(root);
                        Logger.Info("Tespit verileri başarıyla kaydedildi.");
                    }
                    else
                    {
                        Logger.Warning("Geçersiz veya boş tespit verisi alındı.");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"JSON parsing hatası: {ex.Message}");
                    SafeInvoke(() =>
                    {
                        MessageBox.Show($"JSON parsing hatası: {ex.Message}");
                    });
                }
                finally
                {
                    _inProgress = false;
                }
            }
        }

        private void DetectionRepository_OnNewDetectionSaved()
        {
            if (InvokeRequired)
                Invoke(() => ApplyCurrentFilter());
            else
                ApplyCurrentFilter();
        }

        private void ApplyCurrentFilter()
        {
            DateTime start, end;

            if (chkLastHour.Checked)
            {
                start = DateTime.Now.AddHours(-1);
                end = DateTime.Now;
            }
            else if (chkLastDay.Checked)
            {
                start = DateTime.Now.AddDays(-1);
                end = DateTime.Now;
            }
            else if (chkLastWeek.Checked)
            {
                start = DateTime.Now.AddDays(-7);
                end = DateTime.Now;
            }
            else if (chkLastMonth.Checked)
            {
                start = DateTime.Now.AddMonths(-1);
                end = DateTime.Now;
            }
            else if (chkLastYear.Checked)
            {
                start = DateTime.Now.AddYears(-1);
                end = DateTime.Now;
            }
            else
            {
                start = dtpStart.Value;
                end = dtpEnd.Value;
            }

            var filtered = _detectionRepository.GetDetectionsBetween(start, end);

            DisplayDetections(filtered);
        }

        private void LoadDetectionData()
        {
            try
            {
                var detections = _detectionRepository.GetAllDetections();
                DisplayDetections(detections);
                Logger.Info($"Toplam {detections.Count} adet tespit verisi yüklendi.");
            }
            catch (Exception ex)
            {
                Logger.Error($"Tespit verileri yüklenirken hata oluştu: {ex.Message}");
                MessageBox.Show($"Tespit verileri yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeDataGridView()
        {
            // DataGridView stilini güncelle
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(34, 36, 49);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Satır stilini güncelle
            dataGridView1.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(40, 42, 54);
            dataGridView1.RowsDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridView1.RowsDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridView1.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(75, 75, 75);
            dataGridView1.RowsDefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            // Alternatif satır renkleri
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(50, 52, 63);

            // Kenarlıkları daha ince yap
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(50, 60, 70);
            dataGridView1.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            // Sütunlar için stil
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Genişlik ayarları
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 40;

            // Tablonun genel arka plan rengini değiştirme
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(48, 50, 61);
        }

        private void DisplayDetections(List<Detection> detections)
        {
            var data = detections.Select(d => new
            {
                Etiket = d.Label,
                Güven = $"{d.Confidence:P2}",
                Tarih = d.CreatedAt.ToString("dd.MM.yyyy HH:mm:ss")
            }).ToList();

            dataGridView1.DataSource = data;

            InitializeDataGridView();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var confidenceValue = row.Cells["Güven"].Value.ToString();
                double confidencePercentage = double.Parse(confidenceValue.Replace("%", "")) / 100;

                if (confidencePercentage >= 0.7)
                {
                    row.Cells["Güven"].Style.BackColor = System.Drawing.Color.Green;
                    row.Cells["Güven"].Style.ForeColor = System.Drawing.Color.White;
                }
                else if (confidencePercentage >= 0.5)
                {
                    row.Cells["Güven"].Style.BackColor = System.Drawing.Color.Orange;
                    row.Cells["Güven"].Style.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    row.Cells["Güven"].Style.BackColor = System.Drawing.Color.Red;
                    row.Cells["Güven"].Style.ForeColor = System.Drawing.Color.White;
                }
            }
        }




        private void FilterCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            var current = sender as CheckBox;
            if (current.Checked)
            {
                foreach (var cb in new[] { chkLastHour, chkLastDay, chkLastWeek, chkLastMonth, chkLastYear })
                {
                    if (cb != current)
                        cb.Checked = false;
                }

                dtpStart.Enabled = false;
                dtpEnd.Enabled = false;
            }
            else
            {
                dtpStart.Enabled = true;
                dtpEnd.Enabled = true;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                ApplyCurrentFilter();
                Logger.Info("Filtre uygulandı.");
            }
            catch (Exception ex)
            {
                Logger.Error($"Filtre uygulanırken hata oluştu: {ex.Message}");
                MessageBox.Show($"Filtre uygulanırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Önceki sayfaya dönüldü.");
                this.Close();
            }
            catch (Exception ex)
            {
                Logger.Error($"Sayfa kapatılırken hata oluştu: {ex.Message}");
                MessageBox.Show($"Sayfa kapatılırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Kullanıcı çıkış yaptı.");
                Application.Exit();
            }
            catch (Exception ex)
            {
                Logger.Error($"Çıkış yapılırken hata oluştu: {ex.Message}");
                MessageBox.Show($"Çıkış yapılırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
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
            _webSocketService?.StopListeningAsync().Wait();
        }
    }
}
