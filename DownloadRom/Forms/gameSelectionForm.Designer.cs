namespace DownloadRom.Forms
{
    partial class gameSelectionForm
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
            this.SelectionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SelectionLabel
            // 
            this.SelectionLabel.AutoSize = true;
            this.SelectionLabel.BackColor = System.Drawing.SystemColors.InfoText;
            this.SelectionLabel.Location = new System.Drawing.Point(31, 159);
            this.SelectionLabel.Name = "SelectionLabel";
            this.SelectionLabel.Size = new System.Drawing.Size(51, 20);
            this.SelectionLabel.TabIndex = 0;
            this.SelectionLabel.Text = "label1";
            // 
            // gameSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 712);
            this.Controls.Add(this.SelectionLabel);
            this.Name = "gameSelectionForm";
            this.Text = "gameSelectionForm";
            this.Load += new System.EventHandler(this.gameSelectionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SelectionLabel;
    }
}