namespace Rudy_103.src
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NowaGra));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.energiapicture = new System.Windows.Forms.PictureBox();
            this.poziompanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.czaspanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.czaslabel = new System.Windows.Forms.Label();
            this.mapapanel = new System.Windows.Forms.Panel();
            this.minimapapictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer();
            this.czas_rozgrywki = new System.Windows.Forms.Timer();
            this.panel1.SuspendLayout();
            this.poziompanel.SuspendLayout();
            this.czaspanel.SuspendLayout();
            this.mapapanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.energiapicture);
            this.panel1.Controls.Add(this.poziompanel);
            this.panel1.Controls.Add(this.czaspanel);
            this.panel1.Controls.Add(this.mapapanel);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 320);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label4.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(122, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.Text = "Energia";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // energiapicture
            // 
            this.energiapicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.energiapicture.Location = new System.Drawing.Point(122, 0);
            this.energiapicture.Name = "energiapicture";
            this.energiapicture.Size = new System.Drawing.Size(57, 60);
            this.energiapicture.Paint += new System.Windows.Forms.PaintEventHandler(this.energiapicture_Paint);
            // 
            // poziompanel
            // 
            this.poziompanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.poziompanel.Controls.Add(this.label2);
            this.poziompanel.Controls.Add(this.label1);
            this.poziompanel.Location = new System.Drawing.Point(0, 0);
            this.poziompanel.Name = "poziompanel";
            this.poziompanel.Size = new System.Drawing.Size(57, 60);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 33);
            this.label2.Text = "1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 24);
            this.label1.Text = "Poziom";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // czaspanel
            // 
            this.czaspanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.czaspanel.Controls.Add(this.label3);
            this.czaspanel.Controls.Add(this.czaslabel);
            this.czaspanel.Location = new System.Drawing.Point(61, 0);
            this.czaspanel.Name = "czaspanel";
            this.czaspanel.Size = new System.Drawing.Size(57, 60);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.Text = "Czas";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // czaslabel
            // 
            this.czaslabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.czaslabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.czaslabel.ForeColor = System.Drawing.Color.White;
            this.czaslabel.Location = new System.Drawing.Point(4, 24);
            this.czaslabel.Name = "czaslabel";
            this.czaslabel.Size = new System.Drawing.Size(51, 22);
            this.czaslabel.Text = "00:00";
            this.czaslabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mapapanel
            // 
            this.mapapanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mapapanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.mapapanel.Controls.Add(this.minimapapictureBox);
            this.mapapanel.Location = new System.Drawing.Point(183, 0);
            this.mapapanel.Name = "mapapanel";
            this.mapapanel.Size = new System.Drawing.Size(57, 60);
            // 
            // minimapapictureBox
            // 
            this.minimapapictureBox.BackColor = System.Drawing.Color.White;
            this.minimapapictureBox.Location = new System.Drawing.Point(4, 7);
            this.minimapapictureBox.Name = "minimapapictureBox";
            this.minimapapictureBox.Size = new System.Drawing.Size(50, 50);
            this.minimapapictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.minimapapictureBox_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 260);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // czas_rozgrywki
            // 
            this.czas_rozgrywki.Interval = 1000;
            this.czas_rozgrywki.Tick += new System.EventHandler(this.czas_rozgrywki_Tick);
            // 
            // NowaGra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimizeBox = false;
            this.Name = "NowaGra";
            this.Text = "Rudy 103 - Nowa Gra";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NowaGra_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NowaGra_KeyDown);
            this.panel1.ResumeLayout(false);
            this.poziompanel.ResumeLayout(false);
            this.czaspanel.ResumeLayout(false);
            this.mapapanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel mapapanel;
        private System.Windows.Forms.PictureBox minimapapictureBox;
        private System.Windows.Forms.Timer czas_rozgrywki;
        private System.Windows.Forms.Panel czaspanel;
        private System.Windows.Forms.Label czaslabel;
        private System.Windows.Forms.Panel poziompanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox energiapicture;
        private System.Windows.Forms.Label label4;
    }
}