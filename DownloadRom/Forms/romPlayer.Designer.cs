namespace DownloadRom
{
    partial class romPlayer
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
            this.playGamesButton = new System.Windows.Forms.Button();
            this.downloadButton = new System.Windows.Forms.Button();
            this.searchText = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.recentDownloadLabel = new System.Windows.Forms.Label();
            this.recentlyPlayedLabel = new System.Windows.Forms.Label();
            this.deleteGameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // playGamesButton
            // 
            this.playGamesButton.Location = new System.Drawing.Point(3, 40);
            this.playGamesButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.playGamesButton.Name = "playGamesButton";
            this.playGamesButton.Size = new System.Drawing.Size(112, 35);
            this.playGamesButton.TabIndex = 0;
            this.playGamesButton.Text = "Play";
            this.playGamesButton.UseVisualStyleBackColor = true;
            this.playGamesButton.Click += new System.EventHandler(this.playGamesButton_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(3, 86);
            this.downloadButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(112, 35);
            this.downloadButton.TabIndex = 1;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // searchText
            // 
            this.searchText.Location = new System.Drawing.Point(144, 180);
            this.searchText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(148, 26);
            this.searchText.TabIndex = 2;
            this.searchText.TextChanged += new System.EventHandler(this.searchText_TextChanged);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(303, 175);
            this.searchButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(112, 35);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // recentDownloadLabel
            // 
            this.recentDownloadLabel.AutoSize = true;
            this.recentDownloadLabel.Location = new System.Drawing.Point(1044, 14);
            this.recentDownloadLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.recentDownloadLabel.Name = "recentDownloadLabel";
            this.recentDownloadLabel.Size = new System.Drawing.Size(164, 20);
            this.recentDownloadLabel.TabIndex = 4;
            this.recentDownloadLabel.Text = "Recently Downloaded";
            // 
            // recentlyPlayedLabel
            // 
            this.recentlyPlayedLabel.AutoSize = true;
            this.recentlyPlayedLabel.Location = new System.Drawing.Point(591, 14);
            this.recentlyPlayedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.recentlyPlayedLabel.Name = "recentlyPlayedLabel";
            this.recentlyPlayedLabel.Size = new System.Drawing.Size(122, 20);
            this.recentlyPlayedLabel.TabIndex = 5;
            this.recentlyPlayedLabel.Text = "Recently Played";
            this.recentlyPlayedLabel.Click += new System.EventHandler(this.recentlyPlayedLabel_Click);
            // 
            // deleteGameButton
            // 
            this.deleteGameButton.Location = new System.Drawing.Point(471, 14);
            this.deleteGameButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deleteGameButton.Name = "deleteGameButton";
            this.deleteGameButton.Size = new System.Drawing.Size(112, 35);
            this.deleteGameButton.TabIndex = 6;
            this.deleteGameButton.Text = "Delete game";
            this.deleteGameButton.UseVisualStyleBackColor = true;
            this.deleteGameButton.Click += new System.EventHandler(this.deleteGameButton_Click);
            // 
            // romPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 697);
            this.Controls.Add(this.deleteGameButton);
            this.Controls.Add(this.recentlyPlayedLabel);
            this.Controls.Add(this.recentDownloadLabel);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchText);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.playGamesButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "romPlayer";
            this.Text = "romPlayer";
            this.Load += new System.EventHandler(this.romPlayer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button playGamesButton;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.TextBox searchText;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label recentDownloadLabel;
        private System.Windows.Forms.Label recentlyPlayedLabel;
        private System.Windows.Forms.Button deleteGameButton;
    }
}