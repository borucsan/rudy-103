namespace Rudy_103.src
{
    partial class Opcje
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
            this.WznowButton = new System.Windows.Forms.Button();
            this.WyjdzButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.WyjdzButton);
            this.panel1.Controls.Add(this.WznowButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 294);
            // 
            // WznowButton
            // 
            this.WznowButton.BackColor = System.Drawing.Color.White;
            this.WznowButton.Location = new System.Drawing.Point(3, 115);
            this.WznowButton.Name = "WznowButton";
            this.WznowButton.Size = new System.Drawing.Size(234, 26);
            this.WznowButton.TabIndex = 0;
            this.WznowButton.Text = "Wznów Grę";
            this.WznowButton.Click += new System.EventHandler(this.WznowButton_Click);
            // 
            // WyjdzButton
            // 
            this.WyjdzButton.BackColor = System.Drawing.Color.White;
            this.WyjdzButton.Location = new System.Drawing.Point(3, 147);
            this.WyjdzButton.Name = "WyjdzButton";
            this.WyjdzButton.Size = new System.Drawing.Size(234, 26);
            this.WyjdzButton.TabIndex = 2;
            this.WyjdzButton.Text = "Wyjdź z Gry";
            this.WyjdzButton.Click += new System.EventHandler(this.WyjdzButton_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 21);
            this.label1.Text = "Gra została wstrzymana.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Opcje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "Opcje";
            this.Text = "Rudy 103 - Opcje";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button WznowButton;
        private System.Windows.Forms.Button WyjdzButton;
        private System.Windows.Forms.Label label1;
    }
}