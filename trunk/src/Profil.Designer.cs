namespace Rudy_103.src
{
    partial class Profil
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.nowyProfilButton = new System.Windows.Forms.Button();
            this.wczytajProfilButton = new System.Windows.Forms.Button();
            this.profil3radioButton = new System.Windows.Forms.RadioButton();
            this.profil2radioButton = new System.Windows.Forms.RadioButton();
            this.profil1radioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LoadCustomButton = new System.Windows.Forms.Button();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.NewGameButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.nowyProfilButton);
            this.panel2.Controls.Add(this.wczytajProfilButton);
            this.panel2.Controls.Add(this.profil3radioButton);
            this.panel2.Controls.Add(this.profil2radioButton);
            this.panel2.Controls.Add(this.profil1radioButton);
            this.panel2.Location = new System.Drawing.Point(4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(313, 131);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 20);
            this.label1.Text = "Wybierz Profil";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // nowyProfilButton
            // 
            this.nowyProfilButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nowyProfilButton.BackColor = System.Drawing.Color.Red;
            this.nowyProfilButton.Enabled = false;
            this.nowyProfilButton.Location = new System.Drawing.Point(210, 105);
            this.nowyProfilButton.Name = "nowyProfilButton";
            this.nowyProfilButton.Size = new System.Drawing.Size(100, 20);
            this.nowyProfilButton.TabIndex = 4;
            this.nowyProfilButton.Text = "Nowy Profil";
            this.nowyProfilButton.Click += new System.EventHandler(this.nowyProfilButton_Click);
            // 
            // wczytajProfilButton
            // 
            this.wczytajProfilButton.BackColor = System.Drawing.Color.Red;
            this.wczytajProfilButton.Enabled = false;
            this.wczytajProfilButton.Location = new System.Drawing.Point(6, 105);
            this.wczytajProfilButton.Name = "wczytajProfilButton";
            this.wczytajProfilButton.Size = new System.Drawing.Size(100, 20);
            this.wczytajProfilButton.TabIndex = 3;
            this.wczytajProfilButton.Text = "Wczytaj Profil";
            this.wczytajProfilButton.Click += new System.EventHandler(this.wczytajProfilButton_Click);
            // 
            // profil3radioButton
            // 
            this.profil3radioButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.profil3radioButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.profil3radioButton.Location = new System.Drawing.Point(111, 82);
            this.profil3radioButton.Name = "profil3radioButton";
            this.profil3radioButton.Size = new System.Drawing.Size(118, 20);
            this.profil3radioButton.TabIndex = 2;
            this.profil3radioButton.Text = "profil 3";
            // 
            // profil2radioButton
            // 
            this.profil2radioButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.profil2radioButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.profil2radioButton.Location = new System.Drawing.Point(111, 56);
            this.profil2radioButton.Name = "profil2radioButton";
            this.profil2radioButton.Size = new System.Drawing.Size(118, 20);
            this.profil2radioButton.TabIndex = 1;
            this.profil2radioButton.Text = "profil 2";
            // 
            // profil1radioButton
            // 
            this.profil1radioButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.profil1radioButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.profil1radioButton.Location = new System.Drawing.Point(111, 30);
            this.profil1radioButton.Name = "profil1radioButton";
            this.profil1radioButton.Size = new System.Drawing.Size(118, 20);
            this.profil1radioButton.TabIndex = 0;
            this.profil1radioButton.Text = "profil 1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Controls.Add(this.LoadCustomButton);
            this.panel1.Controls.Add(this.ContinueButton);
            this.panel1.Controls.Add(this.NewGameButton);
            this.panel1.Location = new System.Drawing.Point(4, 135);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(313, 102);
            // 
            // LoadCustomButton
            // 
            this.LoadCustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadCustomButton.BackColor = System.Drawing.Color.Red;
            this.LoadCustomButton.Enabled = false;
            this.LoadCustomButton.Location = new System.Drawing.Point(3, 71);
            this.LoadCustomButton.Name = "LoadCustomButton";
            this.LoadCustomButton.Size = new System.Drawing.Size(307, 27);
            this.LoadCustomButton.TabIndex = 2;
            this.LoadCustomButton.Text = "Wczytaj Własną Mapę";
            this.LoadCustomButton.Click += new System.EventHandler(this.LoadCustomButton_Click);
            // 
            // ContinueButton
            // 
            this.ContinueButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ContinueButton.BackColor = System.Drawing.Color.Red;
            this.ContinueButton.Enabled = false;
            this.ContinueButton.Location = new System.Drawing.Point(3, 38);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(307, 27);
            this.ContinueButton.TabIndex = 1;
            this.ContinueButton.Text = "Kontynuuj";
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // NewGameButton
            // 
            this.NewGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NewGameButton.BackColor = System.Drawing.Color.Red;
            this.NewGameButton.Enabled = false;
            this.NewGameButton.Location = new System.Drawing.Point(3, 5);
            this.NewGameButton.Name = "NewGameButton";
            this.NewGameButton.Size = new System.Drawing.Size(307, 27);
            this.NewGameButton.TabIndex = 0;
            this.NewGameButton.Text = "Nowa Gra";
            this.NewGameButton.Click += new System.EventHandler(this.NewGameButton_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Profil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(320, 240);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Profil";
            this.Text = "Profil";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton profil3radioButton;
        private System.Windows.Forms.RadioButton profil2radioButton;
        private System.Windows.Forms.RadioButton profil1radioButton;
        private System.Windows.Forms.Button nowyProfilButton;
        private System.Windows.Forms.Button wczytajProfilButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button LoadCustomButton;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Button NewGameButton;
        private System.Windows.Forms.Timer timer1;
    }
}