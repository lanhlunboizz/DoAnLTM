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
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            button1 = new Button();
            progressBar1 = new ProgressBar();
            label1 = new Label();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            SuspendLayout();
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(12, 66);
            webView21.Name = "webView21";
            webView21.Size = new Size(776, 557);
            webView21.TabIndex = 0;
            webView21.ZoomFactor = 1D;
            // 
            // button1
            // 
            button1.Location = new Point(551, 12);
            button1.Name = "button1";
            button1.Size = new Size(237, 39);
            button1.TabIndex = 1;
            button1.Text = "Tải danh sách phim";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(99, 659);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(611, 29);
            progressBar1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(256, 41);
            label1.TabIndex = 3;
            label1.Text = "Phim đang chiếu";
            // 
            // button2
            // 
            button2.Location = new Point(286, 717);
            button2.Name = "button2";
            button2.Size = new Size(237, 39);
            button2.TabIndex = 4;
            button2.Text = "Đặt vé";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 803);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(progressBar1);
            Controls.Add(button1);
            Controls.Add(webView21);
            Name = "Menu";
            Text = "test";
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Button button1;
        private ProgressBar progressBar1;
        private Label label1;
        private Button button2;
    }
}