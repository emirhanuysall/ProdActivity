namespace ProdActivity.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BtnConnect = new Button();
            BtnDisconnect = new Button();
            lblDetections = new Label();
            lblStatus = new Label();
            txtLog = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // BtnConnect
            // 
            BtnConnect.BackColor = Color.Green;
            BtnConnect.FlatAppearance.BorderColor = Color.DarkGreen;
            BtnConnect.FlatStyle = FlatStyle.Flat;
            BtnConnect.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            BtnConnect.ForeColor = SystemColors.ButtonHighlight;
            BtnConnect.Location = new Point(32, 325);
            BtnConnect.Name = "BtnConnect";
            BtnConnect.Size = new Size(170, 55);
            BtnConnect.TabIndex = 0;
            BtnConnect.Text = "Bağlan";
            BtnConnect.UseVisualStyleBackColor = false;
            BtnConnect.Click += BtnConnect_Click;
            // 
            // BtnDisconnect
            // 
            BtnDisconnect.BackColor = Color.Firebrick;
            BtnDisconnect.FlatAppearance.BorderColor = Color.Maroon;
            BtnDisconnect.FlatStyle = FlatStyle.Flat;
            BtnDisconnect.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            BtnDisconnect.ForeColor = SystemColors.ButtonHighlight;
            BtnDisconnect.Location = new Point(220, 325);
            BtnDisconnect.Name = "BtnDisconnect";
            BtnDisconnect.Size = new Size(170, 55);
            BtnDisconnect.TabIndex = 1;
            BtnDisconnect.Text = "Bağlantıyı Kes";
            BtnDisconnect.UseVisualStyleBackColor = false;
            BtnDisconnect.Click += BtnDisconnect_Click;
            // 
            // lblDetections
            // 
            lblDetections.AutoSize = true;
            lblDetections.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDetections.Location = new Point(436, 35);
            lblDetections.Name = "lblDetections";
            lblDetections.Size = new Size(169, 21);
            lblDetections.TabIndex = 2;
            lblDetections.Text = "Tespit Edilen Ürünler";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblStatus.Location = new Point(32, 404);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(141, 21);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Bağlantı Durumu";
            // 
            // txtLog
            // 
            txtLog.BackColor = SystemColors.GradientInactiveCaption;
            txtLog.Location = new Point(32, 80);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(358, 226);
            txtLog.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.Location = new Point(33, 35);
            label1.Name = "label1";
            label1.Size = new Size(214, 21);
            label1.TabIndex = 5;
            label1.Text = "WebSocket Canlı Veri Akışı";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSlateGray;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(txtLog);
            Controls.Add(lblStatus);
            Controls.Add(lblDetections);
            Controls.Add(BtnDisconnect);
            Controls.Add(BtnConnect);
            DoubleBuffered = true;
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnConnect;
        private Button BtnDisconnect;
        private Label lblDetections;
        private Label lblStatus;
        private TextBox txtLog;
        private Label label1;
    }
}