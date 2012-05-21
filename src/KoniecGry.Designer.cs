namespace Rudy_103.src
{
    partial class KoniecGry
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.panel1 = new System.Windows.Forms.Panel();
            this.zatwierdzambutton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.namelabel = new System.Windows.Forms.Label();
            this.punktylabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.wyjdzbutton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.wyjdzbutton2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Controls.Add(this.zatwierdzambutton);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.namelabel);
            this.panel1.Location = new System.Drawing.Point(4, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 53);
            // 
            // zatwierdzambutton
            // 
            this.zatwierdzambutton.BackColor = System.Drawing.Color.Yellow;
            this.zatwierdzambutton.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.zatwierdzambutton.Location = new System.Drawing.Point(22, 27);
            this.zatwierdzambutton.Name = "zatwierdzambutton";
            this.zatwierdzambutton.Size = new System.Drawing.Size(289, 20);
            this.zatwierdzambutton.TabIndex = 3;
            this.zatwierdzambutton.Text = "Zatwierdzam";
            this.zatwierdzambutton.Click += new System.EventHandler(this.zatwierdzambutton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(85, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(226, 21);
            this.textBox1.TabIndex = 1;
            // 
            // namelabel
            // 
            this.namelabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.namelabel.Location = new System.Drawing.Point(4, 4);
            this.namelabel.Name = "namelabel";
            this.namelabel.Size = new System.Drawing.Size(76, 20);
            this.namelabel.Text = "Nick:";
            this.namelabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // punktylabel
            // 
            this.punktylabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.punktylabel.Location = new System.Drawing.Point(143, 8);
            this.punktylabel.Name = "punktylabel";
            this.punktylabel.Size = new System.Drawing.Size(107, 20);
            this.punktylabel.Text = "Punkty: 0";
            this.punktylabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.Controls.Add(this.listBox1);
            this.panel2.Location = new System.Drawing.Point(4, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(330, 65);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.Location = new System.Drawing.Point(22, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(289, 58);
            this.listBox1.TabIndex = 2;
            // 
            // wyjdzbutton
            // 
            this.wyjdzbutton.BackColor = System.Drawing.Color.Yellow;
            this.wyjdzbutton.Enabled = false;
            this.wyjdzbutton.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.wyjdzbutton.Location = new System.Drawing.Point(4, 174);
            this.wyjdzbutton.Name = "wyjdzbutton";
            this.wyjdzbutton.Size = new System.Drawing.Size(158, 20);
            this.wyjdzbutton.TabIndex = 3;
            this.wyjdzbutton.Text = "Wyjdź i Zapisz";
            this.wyjdzbutton.Click += new System.EventHandler(this.wyjdzbutton_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Red;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.punktylabel);
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(330, 34);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 21);
            this.label1.Text = "Koniec Gry";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // wyjdzbutton2
            // 
            this.wyjdzbutton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wyjdzbutton2.BackColor = System.Drawing.Color.Yellow;
            this.wyjdzbutton2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.wyjdzbutton2.Location = new System.Drawing.Point(176, 174);
            this.wyjdzbutton2.Name = "wyjdzbutton2";
            this.wyjdzbutton2.Size = new System.Drawing.Size(158, 20);
            this.wyjdzbutton2.TabIndex = 6;
            this.wyjdzbutton2.Text = "Wyjdź Bez Zapisu";
            this.wyjdzbutton2.Click += new System.EventHandler(this.wyjdzbutton2_Click);
            // 
            // KoniecGry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(337, 214);
            this.ControlBox = false;
            this.Controls.Add(this.wyjdzbutton2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.wyjdzbutton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "KoniecGry";
            this.Text = "KoniecGry";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label punktylabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label namelabel;
        private System.Windows.Forms.Button zatwierdzambutton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button wyjdzbutton;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button wyjdzbutton2;
    }
}