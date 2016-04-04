namespace DownloadRom
{
    partial class romStartOkayForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.yesButton = new System.Windows.Forms.Button();
            this.noButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.commandLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textForCommand = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.Location = new System.Drawing.Point(221, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(511, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Did the rom start correctly? ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(905, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "You can either enter a custom command line argument in the textbox, or leave it b" +
    "lank, and press no to try another configuration";
            // 
            // yesButton
            // 
            this.yesButton.Location = new System.Drawing.Point(174, 154);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(193, 68);
            this.yesButton.TabIndex = 2;
            this.yesButton.Text = "Yes";
            this.yesButton.UseVisualStyleBackColor = true;
            this.yesButton.Click += new System.EventHandler(this.yesButton_Click);
            // 
            // noButton
            // 
            this.noButton.Location = new System.Drawing.Point(483, 154);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(201, 68);
            this.noButton.TabIndex = 3;
            this.noButton.Text = "No";
            this.noButton.UseVisualStyleBackColor = true;
            this.noButton.Click += new System.EventHandler(this.noButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(310, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "the command line prompt that was tried is: ";
            // 
            // commandLabel
            // 
            this.commandLabel.AutoSize = true;
            this.commandLabel.Location = new System.Drawing.Point(505, 91);
            this.commandLabel.Name = "commandLabel";
            this.commandLabel.Size = new System.Drawing.Size(51, 20);
            this.commandLabel.TabIndex = 5;
            this.commandLabel.Text = "label4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(259, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "The command line prompt to use is:";
            // 
            // textForCommand
            // 
            this.textForCommand.Location = new System.Drawing.Point(509, 122);
            this.textForCommand.Name = "textForCommand";
            this.textForCommand.Size = new System.Drawing.Size(100, 26);
            this.textForCommand.TabIndex = 8;
            // 
            // romStartOkayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 252);
            this.Controls.Add(this.textForCommand);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.commandLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.yesButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "romStartOkayForm";
            this.Text = "romStartOkayForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label commandLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textForCommand;
    }
}