namespace DownloadRom
{
    partial class initialSetupForm
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
            this.directoryText = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.defaultButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.customDirectoryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // directoryText
            // 
            this.directoryText.Location = new System.Drawing.Point(108, 116);
            this.directoryText.Name = "directoryText";
            this.directoryText.Size = new System.Drawing.Size(212, 20);
            this.directoryText.TabIndex = 0;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(349, 113);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label1.Location = new System.Drawing.Point(189, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "OR";
            // 
            // defaultButton
            // 
            this.defaultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.defaultButton.Location = new System.Drawing.Point(107, 223);
            this.defaultButton.Name = "defaultButton";
            this.defaultButton.Size = new System.Drawing.Size(213, 71);
            this.defaultButton.TabIndex = 3;
            this.defaultButton.Text = "Use default Directory";
            this.defaultButton.UseVisualStyleBackColor = true;
            this.defaultButton.Click += new System.EventHandler(this.defaultButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(0, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(484, 68);
            this.label2.TabIndex = 4;
            this.label2.Text = "Please choose where you would like emulator games and data to be stored.\r\nYou wil" +
    "l not be asked this again. \r\n\r\nYou may browse to a specific file or just use the" +
    " default directory\r\n";
            // 
            // customDirectoryButton
            // 
            this.customDirectoryButton.Location = new System.Drawing.Point(108, 143);
            this.customDirectoryButton.Name = "customDirectoryButton";
            this.customDirectoryButton.Size = new System.Drawing.Size(212, 27);
            this.customDirectoryButton.TabIndex = 5;
            this.customDirectoryButton.Text = "Use this directory";
            this.customDirectoryButton.UseVisualStyleBackColor = true;
            this.customDirectoryButton.Click += new System.EventHandler(this.customDirectoryButton_Click);
            // 
            // initialSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 306);
            this.Controls.Add(this.customDirectoryButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.defaultButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.directoryText);
            this.Name = "initialSetupForm";
            this.Text = "initialSetupForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox directoryText;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button defaultButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button customDirectoryButton;
    }
}