﻿namespace Rudy_103.src
{
    partial class Top10
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.WrocButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listawynikow = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.listawynikow);
            this.panel1.Controls.Add(this.WrocButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 320);
            // 
            // WrocButton
            // 
            this.WrocButton.BackColor = System.Drawing.Color.White;
            this.WrocButton.Location = new System.Drawing.Point(4, 293);
            this.WrocButton.Name = "WrocButton";
            this.WrocButton.Size = new System.Drawing.Size(233, 24);
            this.WrocButton.TabIndex = 1;
            this.WrocButton.Text = "Wróć do Menu Głównego";
            this.WrocButton.Click += new System.EventHandler(this.WrocButton_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 40);
            this.label1.Text = "Top 10";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // listawynikow
            // 
            this.listawynikow.Location = new System.Drawing.Point(4, 47);
            this.listawynikow.Name = "listawynikow";
            this.listawynikow.Size = new System.Drawing.Size(233, 226);
            this.listawynikow.TabIndex = 3;
            // 
            // Top10
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimizeBox = false;
            this.Name = "Top10";
            this.Text = "Rudy 103 - Top 10";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button WrocButton;
        private System.Windows.Forms.ListBox listawynikow;
    }
}