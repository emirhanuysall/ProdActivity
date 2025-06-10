namespace ProdActivity.WinForms
{
    partial class OrderManagementForm 
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
            label1 = new Label();
            dashboard_panel = new Panel();
            dataGridView1 = new DataGridView();
            filter_panel = new Panel();
            label5 = new Label();
            textBox1 = new TextBox();
            label4 = new Label();
            label3 = new Label();
            dtpEnd = new DateTimePicker();
            dtpStart = new DateTimePicker();
            btnApply = new Button();
            chkLastYear = new CheckBox();
            chkLastMonth = new CheckBox();
            chkLastWeek = new CheckBox();
            chkLastDay = new CheckBox();
            chkLastHour = new CheckBox();
            label2 = new Label();
            productControl_panel = new Panel();
            label10 = new Label();
            panel4 = new Panel();
            lblRole = new Label();
            lblUsername = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            btnLogout = new Button();
            btnReturn = new Button();
            lblDetectedObjects = new Label();
            lblConnectionStatus = new Label();
            label6 = new Label();
            dashboard_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            filter_panel.SuspendLayout();
            productControl_panel.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.Location = new Point(8, 9);
            label1.Name = "label1";
            label1.Size = new Size(198, 25);
            label1.TabIndex = 2;
            label1.Text = "Kalite Yönetim Paneli";
            // 
            // dashboard_panel
            // 
            dashboard_panel.BorderStyle = BorderStyle.FixedSingle;
            dashboard_panel.Controls.Add(dataGridView1);
            dashboard_panel.Location = new Point(210, 122);
            dashboard_panel.Name = "dashboard_panel";
            dashboard_panel.Size = new Size(914, 445);
            dashboard_panel.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(905, 437);
            dataGridView1.TabIndex = 0;
            // 
            // filter_panel
            // 
            filter_panel.BorderStyle = BorderStyle.FixedSingle;
            filter_panel.Controls.Add(label5);
            filter_panel.Controls.Add(textBox1);
            filter_panel.Controls.Add(label4);
            filter_panel.Controls.Add(label3);
            filter_panel.Controls.Add(dtpEnd);
            filter_panel.Controls.Add(dtpStart);
            filter_panel.Controls.Add(btnApply);
            filter_panel.Controls.Add(chkLastYear);
            filter_panel.Controls.Add(chkLastMonth);
            filter_panel.Controls.Add(chkLastWeek);
            filter_panel.Controls.Add(chkLastDay);
            filter_panel.Controls.Add(chkLastHour);
            filter_panel.Controls.Add(label2);
            filter_panel.Location = new Point(210, 37);
            filter_panel.Name = "filter_panel";
            filter_panel.Size = new Size(914, 79);
            filter_panel.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label5.Location = new Point(517, 23);
            label5.Name = "label5";
            label5.Size = new Size(35, 21);
            label5.TabIndex = 24;
            label5.Text = "Ara";
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ScrollBar;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            textBox1.Location = new Point(517, 49);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(151, 23);
            textBox1.TabIndex = 23;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label4.Location = new Point(272, 48);
            label4.Name = "label4";
            label4.Size = new Size(41, 21);
            label4.TabIndex = 22;
            label4.Text = "Bitiş";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label3.Location = new Point(272, 24);
            label3.Name = "label3";
            label3.Size = new Size(78, 21);
            label3.TabIndex = 21;
            label3.Text = "Başlangıç";
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(361, 49);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(109, 23);
            dtpEnd.TabIndex = 20;
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(361, 23);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(109, 23);
            dtpStart.TabIndex = 19;
            // 
            // btnApply
            // 
            btnApply.BackColor = Color.Green;
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold);
            btnApply.Location = new Point(733, 49);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(71, 23);
            btnApply.TabIndex = 18;
            btnApply.Text = "Uygula";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += btnApply_Click;
            // 
            // chkLastYear
            // 
            chkLastYear.AutoSize = true;
            chkLastYear.Location = new Point(185, 28);
            chkLastYear.Name = "chkLastYear";
            chkLastYear.Size = new Size(71, 19);
            chkLastYear.TabIndex = 15;
            chkLastYear.Text = "Son 1 Yıl";
            chkLastYear.UseVisualStyleBackColor = true;
            // 
            // chkLastMonth
            // 
            chkLastMonth.AutoSize = true;
            chkLastMonth.Location = new Point(92, 53);
            chkLastMonth.Name = "chkLastMonth";
            chkLastMonth.Size = new Size(72, 19);
            chkLastMonth.TabIndex = 14;
            chkLastMonth.Text = "Son 1 Ay";
            chkLastMonth.UseVisualStyleBackColor = true;
            // 
            // chkLastWeek
            // 
            chkLastWeek.AutoSize = true;
            chkLastWeek.Location = new Point(92, 28);
            chkLastWeek.Name = "chkLastWeek";
            chkLastWeek.Size = new Size(87, 19);
            chkLastWeek.TabIndex = 13;
            chkLastWeek.Text = "Son 1 Hafta";
            chkLastWeek.UseVisualStyleBackColor = true;
            // 
            // chkLastDay
            // 
            chkLastDay.AutoSize = true;
            chkLastDay.Location = new Point(6, 53);
            chkLastDay.Name = "chkLastDay";
            chkLastDay.Size = new Size(80, 19);
            chkLastDay.TabIndex = 12;
            chkLastDay.Text = "Son 1 Gün";
            chkLastDay.UseVisualStyleBackColor = true;
            // 
            // chkLastHour
            // 
            chkLastHour.AutoSize = true;
            chkLastHour.Location = new Point(6, 28);
            chkLastHour.Name = "chkLastHour";
            chkLastHour.Size = new Size(80, 19);
            chkLastHour.TabIndex = 11;
            chkLastHour.Text = "Son 1 Saat";
            chkLastHour.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.Location = new Point(3, 3);
            label2.Name = "label2";
            label2.Size = new Size(83, 21);
            label2.TabIndex = 8;
            label2.Text = "Filtreleme";
            // 
            // productControl_panel
            // 
            productControl_panel.BorderStyle = BorderStyle.FixedSingle;
            productControl_panel.Controls.Add(label10);
            productControl_panel.Controls.Add(panel4);
            productControl_panel.Controls.Add(btnLogout);
            productControl_panel.Controls.Add(btnReturn);
            productControl_panel.Controls.Add(lblDetectedObjects);
            productControl_panel.Controls.Add(lblConnectionStatus);
            productControl_panel.Controls.Add(label6);
            productControl_panel.Location = new Point(12, 37);
            productControl_panel.Name = "productControl_panel";
            productControl_panel.Size = new Size(189, 530);
            productControl_panel.TabIndex = 8;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label10.Location = new Point(7, 3);
            label10.Name = "label10";
            label10.Size = new Size(55, 17);
            label10.TabIndex = 30;
            label10.Text = "İşlemler";
            // 
            // panel4
            // 
            panel4.BackColor = Color.AliceBlue;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(lblRole);
            panel4.Controls.Add(lblUsername);
            panel4.Controls.Add(label7);
            panel4.Controls.Add(label8);
            panel4.Controls.Add(label9);
            panel4.Location = new Point(7, 27);
            panel4.Name = "panel4";
            panel4.Size = new Size(175, 66);
            panel4.TabIndex = 29;
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblRole.Location = new Point(84, 41);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(31, 13);
            lblRole.TabIndex = 12;
            lblRole.Text = "Rolü";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblUsername.Location = new Point(84, 23);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(24, 13);
            lblUsername.TabIndex = 11;
            lblUsername.Text = "Adı";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label7.Location = new Point(46, 41);
            label7.Name = "label7";
            label7.Size = new Size(37, 13);
            label7.TabIndex = 10;
            label7.Text = "Rolü :";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label8.Location = new Point(7, 23);
            label8.Name = "label8";
            label8.Size = new Size(76, 13);
            label8.TabIndex = 9;
            label8.Text = "Kullanıcı Adı :";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label9.Location = new Point(-1, 0);
            label9.Name = "label9";
            label9.Size = new Size(115, 13);
            label9.TabIndex = 7;
            label9.Text = "Aktif Kullanıcı Bilgiler";
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.Firebrick;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnLogout.Location = new Point(7, 484);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(175, 41);
            btnLogout.TabIndex = 28;
            btnLogout.Text = "Çıkış Yap";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnReturn
            // 
            btnReturn.BackColor = Color.SteelBlue;
            btnReturn.FlatStyle = FlatStyle.Flat;
            btnReturn.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnReturn.Location = new Point(7, 437);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(175, 41);
            btnReturn.TabIndex = 27;
            btnReturn.Text = "Önceki Sayfaya Dön";
            btnReturn.UseVisualStyleBackColor = false;
            btnReturn.Click += btnReturn_Click;
            // 
            // lblDetectedObjects
            // 
            lblDetectedObjects.AutoSize = true;
            lblDetectedObjects.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblDetectedObjects.Location = new Point(9, 172);
            lblDetectedObjects.Name = "lblDetectedObjects";
            lblDetectedObjects.Size = new Size(114, 13);
            lblDetectedObjects.TabIndex = 26;
            lblDetectedObjects.Text = "Tespit Edilen Ürünler";
            // 
            // lblConnectionStatus
            // 
            lblConnectionStatus.AutoSize = true;
            lblConnectionStatus.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblConnectionStatus.Location = new Point(9, 148);
            lblConnectionStatus.Name = "lblConnectionStatus";
            lblConnectionStatus.Size = new Size(71, 13);
            lblConnectionStatus.TabIndex = 25;
            lblConnectionStatus.Text = "WebSocket: ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label6.Location = new Point(3, 116);
            label6.Name = "label6";
            label6.Size = new Size(175, 21);
            label6.TabIndex = 9;
            label6.Text = "WebSocket Anlık Takip";
            // 
            // OrderManagementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSlateGray;
            ClientSize = new Size(1128, 581);
            Controls.Add(productControl_panel);
            Controls.Add(filter_panel);
            Controls.Add(dashboard_panel);
            Controls.Add(label1);
            Name = "OrderManagementForm";
            Text = "OrderManagementForm";
            Load += DashboardForm_Load;
            dashboard_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            filter_panel.ResumeLayout(false);
            filter_panel.PerformLayout();
            productControl_panel.ResumeLayout(false);
            productControl_panel.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel dashboard_panel;
        private Panel filter_panel;
        private Panel productControl_panel;
        private DataGridView dataGridView1;
        private Label label2;
        private CheckBox chkLastYear;
        private CheckBox chkLastMonth;
        private CheckBox chkLastWeek;
        private CheckBox chkLastDay;
        private CheckBox chkLastHour;
        private Button btnApply;
        private Label label4;
        private Label label3;
        private DateTimePicker dtpEnd;
        private DateTimePicker dtpStart;
        private Label label5;
        private TextBox textBox1;
        private Label lblConnectionStatus;
        private Label label6;
        private Label lblDetectedObjects;
        private Button btnReturn;
        private Button btnLogout;
        private Panel panel4;
        private Label lblRole;
        private Label lblUsername;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
    }
}