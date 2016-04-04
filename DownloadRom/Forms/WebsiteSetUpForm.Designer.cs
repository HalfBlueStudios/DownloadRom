namespace DownloadRom
{
    partial class WebsiteSetUpForm
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
            this.browserOfWeb = new System.Windows.Forms.WebBrowser();
            this.NavigationBox = new System.Windows.Forms.TextBox();
            this.navButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // browserOfWeb
            // 
            this.browserOfWeb.Location = new System.Drawing.Point(0, 44);
            this.browserOfWeb.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserOfWeb.Name = "browserOfWeb";
            this.browserOfWeb.Size = new System.Drawing.Size(1491, 748);
            this.browserOfWeb.TabIndex = 0;
            this.browserOfWeb.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // NavigationBox
            // 
            this.NavigationBox.Location = new System.Drawing.Point(472, 12);
            this.NavigationBox.Name = "NavigationBox";
            this.NavigationBox.Size = new System.Drawing.Size(593, 26);
            this.NavigationBox.TabIndex = 1;
            // 
            // navButton
            // 
            this.navButton.Location = new System.Drawing.Point(1080, 15);
            this.navButton.Name = "navButton";
            this.navButton.Size = new System.Drawing.Size(75, 23);
            this.navButton.TabIndex = 2;
            this.navButton.Text = "button1";
            this.navButton.UseVisualStyleBackColor = true;
            this.navButton.Click += new System.EventHandler(this.navButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 813);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(927, 28);
            this.comboBox1.TabIndex = 3;
            // 
            // WebsiteSetUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 1236);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.navButton);
            this.Controls.Add(this.NavigationBox);
            this.Controls.Add(this.browserOfWeb);
            this.Name = "WebsiteSetUpForm";
            this.Text = "WebsiteSetUpForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser browserOfWeb;
        private System.Windows.Forms.TextBox NavigationBox;
        private System.Windows.Forms.Button navButton;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}