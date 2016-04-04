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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DownloadRom
{ 
    public partial class initialSetupForm : Form
    {
        string gamePathConfigFile = FolderNames.defaultLocation + "\\" + FolderNames.topFolderName + "\\" + FolderNames.configFolder
            + "\\" + FileNames.gamePathName;

        public initialSetupForm()
        {
            InitializeComponent();
            setupLocalFiles();
            if (configExists() == true && gamePathExists() == true)
            {
                string gameFolderLocation = retrieveDirectory(gamePathConfigFile);
                startPlayForm(gameFolderLocation);
            }

        }

        public string retrieveDirectory(string gamePathConfigFile)
        {
            string[] configInfo = File.ReadAllLines(gamePathConfigFile);
            for (int i = 0; i < configInfo.Length; i++)
            {
                if (configInfo[i].Contains(configNames.beforeGamesPath))
                {
                    return (configInfo[i + 1]);
                }
            }
            return (null);
        }

        private void startPlayForm(string chosenDirectory)
        {
            romPlayer playForm = new romPlayer(chosenDirectory);
            playForm.FormClosed += new FormClosedEventHandler(endApplication);
            this.Hide();
            this.VisibleChanged += new EventHandler(alwaysHide);
            playForm.ShowDialog();
        }

        private void alwaysHide(object sender, EventArgs e)
        {
            if(this.Visible == true)
            {
                this.Visible = false;
            }
        }

        private void endApplication(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowDialog();
            directoryText.Text = folderBrowser.SelectedPath;
        }

        private void setupLocalFiles()
        {
            string topDirectory = FolderNames.defaultLocation + "/" + FolderNames.topFolderName;
            Directory.CreateDirectory(topDirectory);
            string configFolder = topDirectory + "/" + FolderNames.configFolder;
            Directory.CreateDirectory(configFolder);
            string databaseFolder = topDirectory + "/" + FolderNames.databaseFolder;
            Directory.CreateDirectory(databaseFolder);
            Directory.CreateDirectory(FolderNames.tempDownloads);
            Directory.CreateDirectory(FolderNames.tempExtractionFolderPath);
            Directory.CreateDirectory(FolderNames.emulationPathsFolderPath);
        }

        private void setupGamePathConfig(string directoryToUse)
        {
            StreamWriter writer = File.CreateText(gamePathConfigFile);
            writer.Close();
            StreamWriter gameWriter = File.AppendText(gamePathConfigFile);
            gameWriter.WriteLine(configNames.beforeGamesPath);
            gameWriter.WriteLine(directoryToUse);
            gameWriter.Close();
        }

        private bool configExists()
        {
            if (File.Exists(gamePathConfigFile) == false)
            {
                return (false);
            }
            else
            {
                return (true);
            }
        }
        
        private bool gamePathExists()
        {
            string[] configInfo = File.ReadAllLines(gamePathConfigFile);
            for(int i = 0; i < configInfo.Length; i++)
            {
                if(configInfo[i].Contains(configNames.beforeGamesPath))
                {
                    if(Directory.Exists(configInfo[i + 1] + "\\" + FolderNames.gameRootFolder))
                    {
                        return (true);
                    }
                    else
                    {
                        return (false);
                    }
                }
            }
            return (false);
        }

        private void setupDatabase()
        {
            try
            {
                databaseHelper.createGameDatabase();
            }catch(Exception e)
            {
                Process start = Process.Start("cmd.exe", "/C SqlLocalDB c "  + databaseHelper.instanceName + " 11.0");
                start.WaitForExit();
                databaseHelper.rebuildDatabase();
            }
        }

        private void setupGamesFolder(string directoryToUse)
        {
            string rootGamesFolder = directoryToUse + "/" + FolderNames.gameRootFolder;
            Directory.CreateDirectory(rootGamesFolder);
            string pictureFolder = rootGamesFolder + "/" + FolderNames.gameImageFolder;
            Directory.CreateDirectory(pictureFolder);
            string gamesFolder = rootGamesFolder + "/" + FolderNames.downloadedGamesFolder;
            Directory.CreateDirectory(gamesFolder);
        }

        private void customDirectoryButton_Click(object sender, EventArgs e)
        {
            string directoryToUse = directoryText.Text;
            if(Directory.Exists(directoryToUse) == false)
            {
                MessageBox.Show("Chosen custom path does not exist!");
            }
            else
            {
                doInitialSetUp(directoryToUse);
            }
        }

        private void doInitialSetUp(string directoryToUse)
        {
            setupGamesFolder(directoryToUse);
            setupDatabase();
            setupConfigs(directoryToUse);
            startPlayForm(directoryToUse);
        }

        private void setupConfigs(string directoryToUse)
        {
            setupGamePathConfig(directoryToUse);
        }

        private void defaultButton_Click(object sender, EventArgs e)
        {
            doInitialSetUp(FolderNames.defaultLocation);
        }
    }
}
