namespace ProdActivity.WinForms
{
    partial class RedirectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RedirectForm));
            btnUserManagement = new Button();
            btnOrderManagement = new Button();
            pcboxUser = new PictureBox();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            btnTest = new Button();
            pictureBox4 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pcboxUser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // btnUserManagement
            // 
            btnUserManagement.BackColor = Color.DodgerBlue;
            btnUserManagement.FlatStyle = FlatStyle.Flat;
            btnUserManagement.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnUserManagement.Location = new Point(409, 156);
            btnUserManagement.Name = "btnUserManagement";
            btnUserManagement.Size = new Size(186, 151);
            btnUserManagement.TabIndex = 27;
            btnUserManagement.Text = "Kullanıcı Yönetimi";
            btnUserManagement.TextAlign = ContentAlignment.BottomCenter;
            btnUserManagement.UseVisualStyleBackColor = false;
            btnUserManagement.Click += btnUserManagement_Click;
            // 
            // btnOrderManagement
            // 
            btnOrderManagement.BackColor = Color.Green;
            btnOrderManagement.FlatStyle = FlatStyle.Flat;
            btnOrderManagement.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnOrderManagement.Location = new Point(217, 156);
            btnOrderManagement.Name = "btnOrderManagement";
            btnOrderManagement.Size = new Size(186, 151);
            btnOrderManagement.TabIndex = 23;
            btnOrderManagement.Text = "Ürün Takibi";
            btnOrderManagement.TextAlign = ContentAlignment.BottomCenter;
            btnOrderManagement.UseVisualStyleBackColor = false;
            btnOrderManagement.Click += btnOrderManagement_Click;
            // 
            // pcboxUser
            // 
            pcboxUser.BackColor = Color.DodgerBlue;
            pcboxUser.Image = (Image)resources.GetObject("pcboxUser.Image");
            pcboxUser.Location = new Point(422, 166);
            pcboxUser.Name = "pcboxUser";
            pcboxUser.Size = new Size(162, 121);
            pcboxUser.SizeMode = PictureBoxSizeMode.Zoom;
            pcboxUser.TabIndex = 28;
            pcboxUser.TabStop = false;
            pcboxUser.Click += btnUserManagement_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Green;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(230, 166);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(162, 121);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 24;
            pictureBox1.TabStop = false;
            pictureBox1.Click += btnUserManagement_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.Location = new Point(314, 67);
            label1.Name = "label1";
            label1.Size = new Size(179, 37);
            label1.TabIndex = 29;
            label1.Text = "ProdActivity";
            // 
            // btnTest
            // 
            btnTest.BackColor = Color.Goldenrod;
            btnTest.FlatStyle = FlatStyle.Flat;
            btnTest.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnTest.Location = new Point(12, 12);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(100, 36);
            btnTest.TabIndex = 30;
            btnTest.Text = "Test ";
            btnTest.TextAlign = ContentAlignment.MiddleRight;
            btnTest.UseVisualStyleBackColor = false;
            btnTest.Click += btnTest_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Goldenrod;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(23, 18);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(40, 24);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 31;
            pictureBox4.TabStop = false;
            // 
            // RedirectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSlateGray;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox4);
            Controls.Add(btnTest);
            Controls.Add(label1);
            Controls.Add(pcboxUser);
            Controls.Add(btnUserManagement);
            Controls.Add(pictureBox1);
            Controls.Add(btnOrderManagement);
            Name = "RedirectForm";
            Text = "RedirectForm";
            Load += RedirectForm_Load;
            ((System.ComponentModel.ISupportInitialize)pcboxUser).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnUserManagement;
        private Button btnOrderManagement;
        private PictureBox pcboxUser;
        private PictureBox pictureBox1;
        private Label label1;
        private Button btnTest;
        private PictureBox pictureBox4;
    }
}