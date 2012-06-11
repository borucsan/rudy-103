namespace Rudy_103.src
{
    /// <summary>
    /// Główne okno aplikacji.
    /// </summary>
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.WyjdzButton = new System.Windows.Forms.Button();
            this.Top10Button = new System.Windows.Forms.Button();
            this.NowaGraButton = new System.Windows.Forms.Button();
            this.czas1 = new System.Windows.Forms.Timer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.czas_odswiezania = new System.Windows.Forms.Timer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // WyjdzButton
            // 
            this.WyjdzButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.WyjdzButton.BackColor = System.Drawing.Color.OrangeRed;
            this.WyjdzButton.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.WyjdzButton.ForeColor = System.Drawing.Color.Black;
            this.WyjdzButton.Location = new System.Drawing.Point(61, 71);
            this.WyjdzButton.Name = "WyjdzButton";
            this.WyjdzButton.Size = new System.Drawing.Size(191, 28);
            this.WyjdzButton.TabIndex = 10;
            this.WyjdzButton.Text = "WYJDŹ Z GRY";
            this.WyjdzButton.Click += new System.EventHandler(this.WyjdzButton_Click);
            // 
            // Top10Button
            // 
            this.Top10Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Top10Button.BackColor = System.Drawing.Color.OrangeRed;
            this.Top10Button.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.Top10Button.ForeColor = System.Drawing.Color.Black;
            this.Top10Button.Location = new System.Drawing.Point(61, 37);
            this.Top10Button.Name = "Top10Button";
            this.Top10Button.Size = new System.Drawing.Size(191, 28);
            this.Top10Button.TabIndex = 9;
            this.Top10Button.Text = "TOP 10";
            this.Top10Button.Click += new System.EventHandler(this.Top10Button_Click);
            // 
            // NowaGraButton
            // 
            this.NowaGraButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NowaGraButton.BackColor = System.Drawing.Color.OrangeRed;
            this.NowaGraButton.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.NowaGraButton.ForeColor = System.Drawing.Color.Black;
            this.NowaGraButton.Location = new System.Drawing.Point(61, 3);
            this.NowaGraButton.Name = "NowaGraButton";
            this.NowaGraButton.Size = new System.Drawing.Size(191, 28);
            this.NowaGraButton.TabIndex = 8;
            this.NowaGraButton.Text = "GRA";
            this.NowaGraButton.Click += new System.EventHandler(this.NowaGraButton_Click);
            // 
            // czas1
            // 
            this.czas1.Interval = 500;
            this.czas1.Tick += new System.EventHandler(this.czas1_Tick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.NowaGraButton);
            this.panel1.Controls.Add(this.Top10Button);
            this.panel1.Controls.Add(this.WyjdzButton);
            this.panel1.Location = new System.Drawing.Point(3, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 109);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(60, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(191, 70);
            this.pictureBox1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(315, 43);
            // 
            // czas_odswiezania
            // 
            this.czas_odswiezania.Tick += new System.EventHandler(this.czas_odswiezania_Tick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(4, 167);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(316, 73);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(61, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(191, 43);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(320, 240);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Rudy 103";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainWindow_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button WyjdzButton;
        private System.Windows.Forms.Button Top10Button;
        private System.Windows.Forms.Button NowaGraButton;
        private System.Windows.Forms.Timer czas1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer czas_odswiezania;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;

    }
}

