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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.WyjdzButton = new System.Windows.Forms.Button();
            this.Top10Button = new System.Windows.Forms.Button();
            this.NowaGraButton = new System.Windows.Forms.Button();
            this.czas1 = new System.Windows.Forms.Timer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.czas_odswiezania = new System.Windows.Forms.Timer();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.linkLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Underline);
            this.linkLabel1.Location = new System.Drawing.Point(61, 123);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(191, 20);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.Text = "Strona Główna Projektu";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(61, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 20);
            this.label1.Text = "Programowanie Urządzeń Mobilnych";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // WyjdzButton
            // 
            this.WyjdzButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.WyjdzButton.BackColor = System.Drawing.Color.DarkOrange;
            this.WyjdzButton.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.WyjdzButton.ForeColor = System.Drawing.Color.White;
            this.WyjdzButton.Location = new System.Drawing.Point(61, 71);
            this.WyjdzButton.Name = "WyjdzButton";
            this.WyjdzButton.Size = new System.Drawing.Size(191, 28);
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
            this.Top10Button.Location = new System.Drawing.Point(61, 37);
            this.Top10Button.Name = "Top10Button";
            this.Top10Button.Size = new System.Drawing.Size(191, 28);
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
            this.NowaGraButton.Location = new System.Drawing.Point(61, 3);
            this.NowaGraButton.Name = "NowaGraButton";
            this.NowaGraButton.Size = new System.Drawing.Size(191, 28);
            this.NowaGraButton.TabIndex = 8;
            this.NowaGraButton.Text = "Graj";
            this.NowaGraButton.Click += new System.EventHandler(this.NowaGraButton_Click);
            // 
            // czas1
            // 
            this.czas1.Interval = 1000;
            this.czas1.Tick += new System.EventHandler(this.czas1_Tick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.NowaGraButton);
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.Top10Button);
            this.panel1.Controls.Add(this.WyjdzButton);
            this.panel1.Location = new System.Drawing.Point(3, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 143);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(3, 193);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(315, 44);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Vivaldi", 16F, System.Drawing.FontStyle.Italic);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(69, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 29);
            this.label3.Text = "Section 5 Studios";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("Traditional Arabic", 18F, System.Drawing.FontStyle.Italic);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(69, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 27);
            this.label2.Text = "RUDY 103";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(315, 35);
            // 
            // czas_odswiezania
            // 
            this.czas_odswiezania.Tick += new System.EventHandler(this.czas_odswiezania_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(320, 240);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
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
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button WyjdzButton;
        private System.Windows.Forms.Button Top10Button;
        private System.Windows.Forms.Button NowaGraButton;
        private System.Windows.Forms.Timer czas1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer czas_odswiezania;

    }
}

