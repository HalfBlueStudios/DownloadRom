using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadRom
{
    public partial class romStartOkayForm : Form
    {
        public bool startCorrectly = false;
        public string commandLineToUse = null;

        public romStartOkayForm(string commandLineGiven)
        {
            InitializeComponent();
            commandLabel.Text = commandLineGiven;
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            startCorrectly = false;
            if(textForCommand.Text != "")
            {
                commandLineToUse = textForCommand.Text;
            }
            this.Close();
        }

        private void commandText_TextChanged(object sender, EventArgs e)
        {
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            startCorrectly = true;
            this.Close();
        }
    }
}
