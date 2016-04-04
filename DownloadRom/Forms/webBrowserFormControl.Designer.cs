namespace DownloadRom
{
    partial class webBrowserFormControl
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
            this.browserToShow = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // browserToShow
            // 
            this.browserToShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserToShow.Location = new System.Drawing.Point(0, 0);
            this.browserToShow.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserToShow.Name = "browserToShow";
            this.browserToShow.Size = new System.Drawing.Size(628, 409);
            this.browserToShow.TabIndex = 0;
            this.browserToShow.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.browserToShow_DocumentCompleted);
            // 
            // webBrowserFormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 409);
            this.Controls.Add(this.browserToShow);
            this.Name = "webBrowserFormControl";
            this.Text = "webBrowserFormControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser browserToShow;
    }
}