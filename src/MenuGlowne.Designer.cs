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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.WyjdzButton = new System.Windows.Forms.Button();
            this.Top10Button = new System.Windows.Forms.Button();
            this.NowaGraButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.WyjdzButton);
            this.panel1.Controls.Add(this.Top10Button);
            this.panel1.Controls.Add(this.NowaGraButton);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 294);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 20);
            this.label1.Text = "Programowanie Urządzeń Mobilnych";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // WyjdzButton
            // 
            this.WyjdzButton.BackColor = System.Drawing.Color.White;
            this.WyjdzButton.Location = new System.Drawing.Point(2, 186);
            this.WyjdzButton.Name = "WyjdzButton";
            this.WyjdzButton.Size = new System.Drawing.Size(233, 25);
            this.WyjdzButton.TabIndex = 3;
            this.WyjdzButton.Text = "Wyjdź";
            this.WyjdzButton.Click += new System.EventHandler(this.WyjdzButton_Click);
            // 
            // Top10Button
            // 
            this.Top10Button.BackColor = System.Drawing.Color.White;
            this.Top10Button.Location = new System.Drawing.Point(3, 155);
            this.Top10Button.Name = "Top10Button";
            this.Top10Button.Size = new System.Drawing.Size(233, 25);
            this.Top10Button.TabIndex = 2;
            this.Top10Button.Text = "Top 10";
            this.Top10Button.Click += new System.EventHandler(this.Top10Button_Click);
            // 
            // NowaGraButton
            // 
            this.NowaGraButton.BackColor = System.Drawing.Color.White;
            this.NowaGraButton.Location = new System.Drawing.Point(3, 124);
            this.NowaGraButton.Name = "NowaGraButton";
            this.NowaGraButton.Size = new System.Drawing.Size(233, 25);
            this.NowaGraButton.TabIndex = 1;
            this.NowaGraButton.Text = "Nowa Gra";
            this.NowaGraButton.Click += new System.EventHandler(this.NowaGraButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Silver;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(238, 83);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Underline);
            this.linkLabel1.Location = new System.Drawing.Point(3, 263);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(233, 20);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.Text = "Strona Główna Projektu";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Rudy 103";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainWindow_Closing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button WyjdzButton;
        private System.Windows.Forms.Button Top10Button;
        private System.Windows.Forms.Button NowaGraButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

