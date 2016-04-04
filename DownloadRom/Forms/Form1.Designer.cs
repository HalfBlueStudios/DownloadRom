namespace DownloadRom
{
    partial class RomDownloader
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
            this.SearchTermBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.ResultsPic = new System.Windows.Forms.PictureBox();
            this.chooseSiteBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chooseSystemBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gameListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.downloadButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.labelPerc = new System.Windows.Forms.Label();
            this.labelDownloaded = new System.Windows.Forms.Label();
            this.getSystemText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.formBrowser = new System.Windows.Forms.WebBrowser();
            this.label6 = new System.Windows.Forms.Label();
            this.downloadNameLabel = new System.Windows.Forms.Label();
            this.downloadQueueBox = new System.Windows.Forms.ListBox();
            this.removeFromDownloadQueueButton = new System.Windows.Forms.Button();
            this.cancelDownloadButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.alreadyDownloadedFilter = new System.Windows.Forms.CheckBox();
            this.filterLabel = new System.Windows.Forms.Label();
            this.uniqueFilterBox = new System.Windows.Forms.CheckBox();
            this.chooseLanguageBox = new System.Windows.Forms.ComboBox();
            this.chooseLanguageLabel = new System.Windows.Forms.Label();
            this.playGamesButton = new System.Windows.Forms.Button();
            this.downloadGamesButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsPic)).BeginInit();
            this.SuspendLayout();
            // 
            // SearchTermBox
            // 
            this.SearchTermBox.Location = new System.Drawing.Point(136, 32);
            this.SearchTermBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SearchTermBox.Name = "SearchTermBox";
            this.SearchTermBox.Size = new System.Drawing.Size(148, 26);
            this.SearchTermBox.TabIndex = 0;
            this.SearchTermBox.TextChanged += new System.EventHandler(this.SearchTermBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search Terms";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(315, 29);
            this.searchButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(112, 35);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // ResultsPic
            // 
            this.ResultsPic.Location = new System.Drawing.Point(750, 178);
            this.ResultsPic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ResultsPic.Name = "ResultsPic";
            this.ResultsPic.Size = new System.Drawing.Size(216, 240);
            this.ResultsPic.TabIndex = 4;
            this.ResultsPic.TabStop = false;
            // 
            // chooseSiteBox
            // 
            this.chooseSiteBox.FormattingEnabled = true;
            this.chooseSiteBox.Location = new System.Drawing.Point(134, 126);
            this.chooseSiteBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chooseSiteBox.Name = "chooseSiteBox";
            this.chooseSiteBox.Size = new System.Drawing.Size(180, 28);
            this.chooseSiteBox.TabIndex = 5;
            this.chooseSiteBox.Text = "ChooseSiteBox";
            this.chooseSiteBox.SelectedIndexChanged += new System.EventHandler(this.chooseSiteBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 95);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Choose Site";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // chooseSystemBox
            // 
            this.chooseSystemBox.FormattingEnabled = true;
            this.chooseSystemBox.Location = new System.Drawing.Point(380, 126);
            this.chooseSystemBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chooseSystemBox.Name = "chooseSystemBox";
            this.chooseSystemBox.Size = new System.Drawing.Size(356, 28);
            this.chooseSystemBox.TabIndex = 8;
            this.chooseSystemBox.SelectedIndexChanged += new System.EventHandler(this.chooseSystemBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(375, 95);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Choose System";
            // 
            // gameListBox
            // 
            this.gameListBox.FormattingEnabled = true;
            this.gameListBox.ItemHeight = 20;
            this.gameListBox.Location = new System.Drawing.Point(134, 178);
            this.gameListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gameListBox.Name = "gameListBox";
            this.gameListBox.Size = new System.Drawing.Size(602, 704);
            this.gameListBox.TabIndex = 10;
            this.gameListBox.SelectedIndexChanged += new System.EventHandler(this.gameListBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(810, 154);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Preview Pic";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(747, 629);
            this.downloadButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(207, 188);
            this.downloadButton.TabIndex = 12;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(750, 557);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(216, 35);
            this.progressBar.TabIndex = 14;
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(748, 532);
            this.labelSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(89, 20);
            this.labelSpeed.TabIndex = 15;
            this.labelSpeed.Text = "labelSpeed";
            this.labelSpeed.Click += new System.EventHandler(this.label6_Click);
            // 
            // labelPerc
            // 
            this.labelPerc.AutoSize = true;
            this.labelPerc.Location = new System.Drawing.Point(752, 512);
            this.labelPerc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPerc.Name = "labelPerc";
            this.labelPerc.Size = new System.Drawing.Size(51, 20);
            this.labelPerc.TabIndex = 16;
            this.labelPerc.Text = "label7";
            // 
            // labelDownloaded
            // 
            this.labelDownloaded.AutoSize = true;
            this.labelDownloaded.Location = new System.Drawing.Point(750, 494);
            this.labelDownloaded.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDownloaded.Name = "labelDownloaded";
            this.labelDownloaded.Size = new System.Drawing.Size(51, 20);
            this.labelDownloaded.TabIndex = 17;
            this.labelDownloaded.Text = "label6";
            // 
            // getSystemText
            // 
            this.getSystemText.Location = new System.Drawing.Point(975, 178);
            this.getSystemText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.getSystemText.Multiline = true;
            this.getSystemText.Name = "getSystemText";
            this.getSystemText.Size = new System.Drawing.Size(416, 238);
            this.getSystemText.TabIndex = 18;
            this.getSystemText.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1106, 126);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 19;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // formBrowser
            // 
            this.formBrowser.Location = new System.Drawing.Point(1402, 423);
            this.formBrowser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.formBrowser.MinimumSize = new System.Drawing.Size(30, 31);
            this.formBrowser.Name = "formBrowser";
            this.formBrowser.Size = new System.Drawing.Size(320, 480);
            this.formBrowser.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(747, 434);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(172, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Currently Downloading:";
            // 
            // downloadNameLabel
            // 
            this.downloadNameLabel.AutoSize = true;
            this.downloadNameLabel.Location = new System.Drawing.Point(748, 454);
            this.downloadNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.downloadNameLabel.Name = "downloadNameLabel";
            this.downloadNameLabel.Size = new System.Drawing.Size(51, 20);
            this.downloadNameLabel.TabIndex = 22;
            this.downloadNameLabel.Text = "label7";
            // 
            // downloadQueueBox
            // 
            this.downloadQueueBox.FormattingEnabled = true;
            this.downloadQueueBox.ItemHeight = 20;
            this.downloadQueueBox.Location = new System.Drawing.Point(975, 428);
            this.downloadQueueBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.downloadQueueBox.Name = "downloadQueueBox";
            this.downloadQueueBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.downloadQueueBox.Size = new System.Drawing.Size(416, 364);
            this.downloadQueueBox.TabIndex = 23;
            // 
            // removeFromDownloadQueueButton
            // 
            this.removeFromDownloadQueueButton.Location = new System.Drawing.Point(975, 805);
            this.removeFromDownloadQueueButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.removeFromDownloadQueueButton.Name = "removeFromDownloadQueueButton";
            this.removeFromDownloadQueueButton.Size = new System.Drawing.Size(418, 35);
            this.removeFromDownloadQueueButton.TabIndex = 24;
            this.removeFromDownloadQueueButton.Text = "Remove from Queue";
            this.removeFromDownloadQueueButton.UseVisualStyleBackColor = true;
            this.removeFromDownloadQueueButton.Click += new System.EventHandler(this.removeFromDownloadQueueButton_Click);
            // 
            // cancelDownloadButton
            // 
            this.cancelDownloadButton.Location = new System.Drawing.Point(747, 831);
            this.cancelDownloadButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cancelDownloadButton.Name = "cancelDownloadButton";
            this.cancelDownloadButton.Size = new System.Drawing.Size(219, 54);
            this.cancelDownloadButton.TabIndex = 25;
            this.cancelDownloadButton.Text = "Cancel Download";
            this.cancelDownloadButton.UseVisualStyleBackColor = true;
            this.cancelDownloadButton.Click += new System.EventHandler(this.cancelDownloadButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(748, 474);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(51, 20);
            this.statusLabel.TabIndex = 26;
            this.statusLabel.Text = "label7";
            // 
            // alreadyDownloadedFilter
            // 
            this.alreadyDownloadedFilter.AutoSize = true;
            this.alreadyDownloadedFilter.Checked = true;
            this.alreadyDownloadedFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alreadyDownloadedFilter.Location = new System.Drawing.Point(752, 37);
            this.alreadyDownloadedFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.alreadyDownloadedFilter.Name = "alreadyDownloadedFilter";
            this.alreadyDownloadedFilter.Size = new System.Drawing.Size(213, 24);
            this.alreadyDownloadedFilter.TabIndex = 27;
            this.alreadyDownloadedFilter.Text = "Hide already downloaded";
            this.alreadyDownloadedFilter.UseVisualStyleBackColor = true;
            this.alreadyDownloadedFilter.CheckedChanged += new System.EventHandler(this.alreadyDownloadedFilter_CheckedChanged);
            // 
            // filterLabel
            // 
            this.filterLabel.AutoSize = true;
            this.filterLabel.Location = new System.Drawing.Point(747, 0);
            this.filterLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.Size = new System.Drawing.Size(56, 20);
            this.filterLabel.TabIndex = 28;
            this.filterLabel.Text = "Filters:";
            // 
            // uniqueFilterBox
            // 
            this.uniqueFilterBox.AutoSize = true;
            this.uniqueFilterBox.Location = new System.Drawing.Point(752, 72);
            this.uniqueFilterBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uniqueFilterBox.Name = "uniqueFilterBox";
            this.uniqueFilterBox.Size = new System.Drawing.Size(159, 24);
            this.uniqueFilterBox.TabIndex = 29;
            this.uniqueFilterBox.Text = "Only show unique";
            this.uniqueFilterBox.UseVisualStyleBackColor = true;
            this.uniqueFilterBox.CheckedChanged += new System.EventHandler(this.uniqueFilterBox_CheckedChanged);
            // 
            // chooseLanguageBox
            // 
            this.chooseLanguageBox.FormattingEnabled = true;
            this.chooseLanguageBox.Location = new System.Drawing.Point(1004, 29);
            this.chooseLanguageBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chooseLanguageBox.Name = "chooseLanguageBox";
            this.chooseLanguageBox.Size = new System.Drawing.Size(180, 28);
            this.chooseLanguageBox.TabIndex = 30;
            this.chooseLanguageBox.SelectedIndexChanged += new System.EventHandler(this.chooseLanguageBox_SelectedIndexChanged);
            // 
            // chooseLanguageLabel
            // 
            this.chooseLanguageLabel.AutoSize = true;
            this.chooseLanguageLabel.Location = new System.Drawing.Point(999, 0);
            this.chooseLanguageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.chooseLanguageLabel.Name = "chooseLanguageLabel";
            this.chooseLanguageLabel.Size = new System.Drawing.Size(140, 20);
            this.chooseLanguageLabel.TabIndex = 31;
            this.chooseLanguageLabel.Text = "Choose Language";
            // 
            // playGamesButton
            // 
            this.playGamesButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.playGamesButton.Location = new System.Drawing.Point(4, 182);
            this.playGamesButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.playGamesButton.Name = "playGamesButton";
            this.playGamesButton.Size = new System.Drawing.Size(120, 332);
            this.playGamesButton.TabIndex = 32;
            this.playGamesButton.Text = "Play";
            this.playGamesButton.UseVisualStyleBackColor = true;
            this.playGamesButton.Click += new System.EventHandler(this.playGamesButton_Click);
            // 
            // downloadGamesButton
            // 
            this.downloadGamesButton.BackColor = System.Drawing.Color.Red;
            this.downloadGamesButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.downloadGamesButton.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.downloadGamesButton.Location = new System.Drawing.Point(4, 532);
            this.downloadGamesButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.downloadGamesButton.Name = "downloadGamesButton";
            this.downloadGamesButton.Size = new System.Drawing.Size(120, 352);
            this.downloadGamesButton.TabIndex = 33;
            this.downloadGamesButton.Text = "Download";
            this.downloadGamesButton.UseVisualStyleBackColor = false;
            this.downloadGamesButton.Click += new System.EventHandler(this.downloadGamesButton_Click);
            // 
            // RomDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1692, 922);
            this.Controls.Add(this.downloadGamesButton);
            this.Controls.Add(this.playGamesButton);
            this.Controls.Add(this.chooseLanguageLabel);
            this.Controls.Add(this.chooseLanguageBox);
            this.Controls.Add(this.uniqueFilterBox);
            this.Controls.Add(this.filterLabel);
            this.Controls.Add(this.alreadyDownloadedFilter);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.cancelDownloadButton);
            this.Controls.Add(this.removeFromDownloadQueueButton);
            this.Controls.Add(this.downloadQueueBox);
            this.Controls.Add(this.downloadNameLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.formBrowser);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.getSystemText);
            this.Controls.Add(this.labelDownloaded);
            this.Controls.Add(this.labelPerc);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gameListBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chooseSystemBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chooseSiteBox);
            this.Controls.Add(this.ResultsPic);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchTermBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RomDownloader";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.RomDownloader_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ResultsPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SearchTermBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.PictureBox ResultsPic;
        private System.Windows.Forms.ComboBox chooseSiteBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox chooseSystemBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox gameListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label labelPerc;
        private System.Windows.Forms.Label labelDownloaded;
        private System.Windows.Forms.TextBox getSystemText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.WebBrowser formBrowser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label downloadNameLabel;
        private System.Windows.Forms.ListBox downloadQueueBox;
        private System.Windows.Forms.Button removeFromDownloadQueueButton;
        private System.Windows.Forms.Button cancelDownloadButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.CheckBox alreadyDownloadedFilter;
        private System.Windows.Forms.Label filterLabel;
        private System.Windows.Forms.CheckBox uniqueFilterBox;
        private System.Windows.Forms.ComboBox chooseLanguageBox;
        private System.Windows.Forms.Label chooseLanguageLabel;
        private System.Windows.Forms.Button playGamesButton;
        private System.Windows.Forms.Button downloadGamesButton;
    }
}

