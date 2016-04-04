using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadRom
{
    public partial class setUpEmulatorForm : Form
    {
        playableRom selectedRom;
        Form playSender;

        public setUpEmulatorForm(string fileType, playableRom romToUse, Form newSender)
        {
            InitializeComponent();
            playSender = newSender;
            selectedRom = romToUse;
            labelForFileType.Font = new Font(labelForFileType.Font, FontStyle.Bold);
            labelForFileType.Text = "." + fileType;
            systemNameLabel.Font = new Font(labelForFileType.Font, FontStyle.Bold);
            systemNameLabel.Text = romToUse.systemName;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog newDialog = new OpenFileDialog();
            newDialog.Filter = "Executables (*.exe) | *.exe";
            newDialog.ShowDialog();
            PathText.Text = newDialog.FileName;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            string newTextLocation = FolderNames.emulationPathsFolderPath + "\\" + selectedRom.systemName  + ".txt";
            StreamWriter writer = File.CreateText(newTextLocation);
            writer.WriteLine(PathText.Text);
            writer.Close();
            this.Close();
        }

        private void setUpEmulatorForm_Load(object sender, EventArgs e)
        {

        }
    }
}
