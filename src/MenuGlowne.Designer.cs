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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.WyjdzButton = new System.Windows.Forms.Button();
            this.Top10Button = new System.Windows.Forms.Button();
            this.NowaGraButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.czas1 = new System.Windows.Forms.Timer();
            this.KontynuujButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.linkLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Underline);
            this.linkLabel1.Location = new System.Drawing.Point(12, 300);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(216, 20);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.Text = "Strona Główna Projektu";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 280);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 20);
            this.label1.Text = "Programowanie Urządzeń Mobilnych";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // WyjdzButton
            // 
            this.WyjdzButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.WyjdzButton.BackColor = System.Drawing.Color.DarkOrange;
            this.WyjdzButton.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.WyjdzButton.ForeColor = System.Drawing.Color.White;
            this.WyjdzButton.Location = new System.Drawing.Point(12, 209);
            this.WyjdzButton.Name = "WyjdzButton";
            this.WyjdzButton.Size = new System.Drawing.Size(216, 28);
            this.WyjdzButton.TabIndex = 10;
            this.WyjdzButton.Text = "Wyjdź z Gry";
            this.WyjdzButton.Click += new System.EventHandler(this.WyjdzButton_Click);
            // 
            // Top10Button
            // 
            this.Top10Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Top10Button.BackColor = System.Drawing.Color.DarkOrange;
            this.Top10Button.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.Top10Button.ForeColor = System.Drawing.Color.White;
            this.Top10Button.Location = new System.Drawing.Point(12, 175);
            this.Top10Button.Name = "Top10Button";
            this.Top10Button.Size = new System.Drawing.Size(216, 28);
            this.Top10Button.TabIndex = 9;
            this.Top10Button.Text = "Top 10";
            this.Top10Button.Click += new System.EventHandler(this.Top10Button_Click);
            // 
            // NowaGraButton
            // 
            this.NowaGraButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NowaGraButton.BackColor = System.Drawing.Color.DarkOrange;
            this.NowaGraButton.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.NowaGraButton.ForeColor = System.Drawing.Color.White;
            this.NowaGraButton.Location = new System.Drawing.Point(12, 141);
            this.NowaGraButton.Name = "NowaGraButton";
            this.NowaGraButton.Size = new System.Drawing.Size(216, 28);
            this.NowaGraButton.TabIndex = 8;
            this.NowaGraButton.Text = "Nowa Gra";
            this.NowaGraButton.Click += new System.EventHandler(this.NowaGraButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.BackColor = System.Drawing.Color.DimGray;
            this.pictureBox1.Location = new System.Drawing.Point(12, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(216, 54);
            // 
            // czas1
            // 
            this.czas1.Interval = 200;
            this.czas1.Tick += new System.EventHandler(this.czas1_Tick);
            // 
            // KontynuujButton
            // 
            this.KontynuujButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.KontynuujButton.BackColor = System.Drawing.Color.DarkOrange;
            this.KontynuujButton.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.KontynuujButton.ForeColor = System.Drawing.Color.White;
            this.KontynuujButton.Location = new System.Drawing.Point(12, 107);
            this.KontynuujButton.Name = "KontynuujButton";
            this.KontynuujButton.Size = new System.Drawing.Size(216, 28);
            this.KontynuujButton.TabIndex = 14;
            this.KontynuujButton.Text = "Kontynuuj";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Traditional Arabic", 18F, System.Drawing.FontStyle.Italic);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(24, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 27);
            this.label2.Text = "RUDY 103";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.KontynuujButton);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WyjdzButton);
            this.Controls.Add(this.Top10Button);
            this.Controls.Add(this.NowaGraButton);
            this.Controls.Add(this.pictureBox1);
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Rudy 103";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainWindow_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button WyjdzButton;
        private System.Windows.Forms.Button Top10Button;
        private System.Windows.Forms.Button NowaGraButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer czas1;
        private System.Windows.Forms.Button KontynuujButton;
        private System.Windows.Forms.Label label2;

    }
}

