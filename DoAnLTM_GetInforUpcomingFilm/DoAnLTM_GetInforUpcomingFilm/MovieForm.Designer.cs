namespace DoAnLTM_GetInforUpcomingFilm
{
    partial class MovieForm
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
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btdGetInforFilm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            this.SuspendLayout();
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Location = new System.Drawing.Point(22, 63);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(751, 375);
            this.webView21.TabIndex = 1;
            this.webView21.ZoomFactor = 1D;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(198, 457);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(396, 16);
            this.progressBar1.TabIndex = 2;
            // 
            // btdGetInforFilm
            // 
            this.btdGetInforFilm.Location = new System.Drawing.Point(22, 24);
            this.btdGetInforFilm.Name = "btdGetInforFilm";
            this.btdGetInforFilm.Size = new System.Drawing.Size(119, 23);
            this.btdGetInforFilm.TabIndex = 3;
            this.btdGetInforFilm.Text = "GET";
            this.btdGetInforFilm.UseVisualStyleBackColor = true;
            this.btdGetInforFilm.Click += new System.EventHandler(this.btdGetInforFilm_Click);
            // 
            // MovieForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 495);
            this.Controls.Add(this.btdGetInforFilm);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.webView21);
            this.Name = "MovieForm";
            this.Text = "MovieForm";
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btdGetInforFilm;
    }
}