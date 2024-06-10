namespace Bai04
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            film_download_button = new Button();
            progressBar1 = new ProgressBar();
            label1 = new Label();
            show_download_button = new Button();
            label2 = new Label();
            alarm_button = new Button();
            time_lb = new Label();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            SuspendLayout();
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.BackColor = Color.FromArgb(255, 224, 192);
            webView21.BackgroundImage = (Image)resources.GetObject("webView21.BackgroundImage");
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.Lavender;
            webView21.Location = new Point(11, 67);
            webView21.Name = "webView21";
            webView21.Size = new Size(944, 643);
            webView21.TabIndex = 0;
            webView21.Visible = false;
            webView21.ZoomFactor = 1D;
            webView21.SizeChanged += formSize_changed;
            // 
            // film_download_button
            // 
            film_download_button.Location = new Point(719, 12);
            film_download_button.Name = "film_download_button";
            film_download_button.Size = new Size(237, 39);
            film_download_button.TabIndex = 1;
            film_download_button.Text = "Tải danh sách phim";
            film_download_button.UseVisualStyleBackColor = true;
            film_download_button.Click += film_download_button_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(40, 736);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(611, 45);
            progressBar1.TabIndex = 2;
            progressBar1.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.LavenderBlush;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(240, 41);
            label1.TabIndex = 3;
            label1.Text = "Lịch chiếu phim";
            // 
            // show_download_button
            // 
            show_download_button.Font = new Font("Segoe UI", 9F);
            show_download_button.Location = new Point(476, 12);
            show_download_button.Name = "show_download_button";
            show_download_button.Size = new Size(237, 39);
            show_download_button.TabIndex = 4;
            show_download_button.Text = "Tải danh sách show";
            show_download_button.UseVisualStyleBackColor = true;
            show_download_button.Click += sh_down_button_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.LavenderBlush;
            label2.ImageKey = "(none)";
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(349, 41);
            label2.TabIndex = 5;
            label2.Text = "Lịch chiếu chương trình";
            // 
            // alarm_button
            // 
            alarm_button.Location = new Point(719, 736);
            alarm_button.Name = "alarm_button";
            alarm_button.Size = new Size(237, 45);
            alarm_button.TabIndex = 6;
            alarm_button.Text = "Đặt báo thức ";
            alarm_button.UseVisualStyleBackColor = true;
            alarm_button.Visible = false;
            alarm_button.Click += alarm_button_ClickAsync;
            // 
            // time_lb
            // 
            time_lb.AutoSize = true;
            time_lb.BackColor = Color.Transparent;
            time_lb.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            time_lb.ForeColor = Color.LavenderBlush;
            time_lb.Location = new Point(40, 740);
            time_lb.Name = "time_lb";
            time_lb.Size = new Size(349, 41);
            time_lb.TabIndex = 7;
            time_lb.Text = "Lịch chiếu chương trình";
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(987, 803);
            Controls.Add(time_lb);
            Controls.Add(alarm_button);
            Controls.Add(label2);
            Controls.Add(show_download_button);
            Controls.Add(label1);
            Controls.Add(progressBar1);
            Controls.Add(film_download_button);
            Controls.Add(webView21);
            Name = "Menu";
            Text = "test";
            Load += Form1_Load;
            SizeChanged += formSize_changed;
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Button film_download_button;
        private ProgressBar progressBar1;
        private Label label1;
        private Button show_download_button;
        private Label label2;
        private Button alarm_button;
        private Label time_lb;
    }
}