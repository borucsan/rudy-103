﻿namespace Rudy_103.src
{
    partial class NowaGra
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
            this.timer1 = new System.Windows.Forms.Timer();
            this.czas_rozgrywki = new System.Windows.Forms.Timer();
            this.czas_efektow = new System.Windows.Forms.Timer();
            this.czas_informacji = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // czas_rozgrywki
            // 
            this.czas_rozgrywki.Interval = 1000;
            this.czas_rozgrywki.Tick += new System.EventHandler(this.czas_rozgrywki_Tick);
            // 
            // czas_efektow
            // 
            this.czas_efektow.Tick += new System.EventHandler(this.czas_efektow_Tick);
            // 
            // czas_informacji
            // 
            this.czas_informacji.Interval = 3000;
            this.czas_informacji.Tick += new System.EventHandler(this.czas_informacji_Tick);
            // 
            // NowaGra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimizeBox = false;
            this.Name = "NowaGra";
            this.Text = "Rudy 103 - Nowa Gra";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.NowaGra_Paint);
            this.Click += new System.EventHandler(this.NowaGra_Click);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NowaGra_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NowaGra_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer czas_rozgrywki;
        private System.Windows.Forms.Timer czas_efektow;
        private System.Windows.Forms.Timer czas_informacji;
    }
}