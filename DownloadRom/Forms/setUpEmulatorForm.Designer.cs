namespace DownloadRom
{
    partial class setUpEmulatorForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelForFileType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.systemNameLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PathText = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(427, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "This is the first time you are opening a file with the file Type: ";
            // 
            // labelForFileType
            // 
            this.labelForFileType.AutoSize = true;
            this.labelForFileType.Location = new System.Drawing.Point(578, 32);
            this.labelForFileType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelForFileType.Name = "labelForFileType";
            this.labelForFileType.Size = new System.Drawing.Size(51, 20);
            this.labelForFileType.TabIndex = 1;
            this.labelForFileType.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(354, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "This file type is used for emulators of the system: ";
            // 
            // systemNameLabel
            // 
            this.systemNameLabel.AutoSize = true;
            this.systemNameLabel.Location = new System.Drawing.Point(579, 66);
            this.systemNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.systemNameLabel.Name = "systemNameLabel";
            this.systemNameLabel.Size = new System.Drawing.Size(51, 20);
            this.systemNameLabel.TabIndex = 3;
            this.systemNameLabel.Text = "label3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 148);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(516, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Please browse to the program you would like to open this type of file with";
            // 
            // PathText
            // 
            this.PathText.Location = new System.Drawing.Point(201, 222);
            this.PathText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PathText.Name = "PathText";
            this.PathText.Size = new System.Drawing.Size(271, 26);
            this.PathText.TabIndex = 5;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(518, 217);
            this.browseButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(112, 35);
            this.browseButton.TabIndex = 6;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(201, 282);
            this.submitButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(273, 62);
            this.submitButton.TabIndex = 7;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(518, 282);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(112, 62);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // setUpEmulatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 402);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.PathText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.systemNameLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelForFileType);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "setUpEmulatorForm";
            this.Text = "setUpEmulatorForm";
            this.Load += new System.EventHandler(this.setUpEmulatorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelForFileType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label systemNameLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PathText;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button cancelButton;
    }
}