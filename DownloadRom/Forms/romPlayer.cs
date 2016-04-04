using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadRom
{
    public partial class romPlayer : Form
    { 
        private RomDownloader downloadForm;
        private List<playableRom> recentlyDownloadedList;
        private List<playableRom> recentlyPlayedList;
        private List<pageOfRoms> romPages = new List<pageOfRoms>();
        int currentPage = -1;
        private ControlCollection visibleControl;

        private PictureBox selectedGamePic;
        private Label selectedGameName = new Label();
        private Label selectedGameSystem = new Label();
        private Label selectedGameGenre1 = new Label();
        private Label selectedGameGenre2 = new Label();
        private Label selectedGameGenre3 = new Label();
        private Button playRomButton = new Button();
        

        private playableRom selectedRom;


        public romPlayer(string newGameFolder)
        {
            InitializeComponent();
            setUpSelectedControls();
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            downloadForm = new RomDownloader(newGameFolder, this);
            downloadForm.FormClosed += new FormClosedEventHandler(endApplication);
            this.Width = formSizes.formWidth;
            this.Height = formSizes.formLength;
            recentDownloadLabel.Location = new Point(formSizes.recentlyDownloadedX - 16, formSizes.recentlyDownloadedY - 15);
            recentDownloadLabel.Font = new Font(recentDownloadLabel.Font, FontStyle.Bold);
            recentlyPlayedLabel.Location = new Point(formSizes.recentlyPlayedX, formSizes.recentlyPlayedY - 15);
            recentlyPlayedLabel.Font = new Font(recentDownloadLabel.Font, FontStyle.Bold);
            downloadButton.Width = formSizes.buttonWidth;
            downloadButton.Height = formSizes.buttonLength;
            downloadButton.Location = new Point(formSizes.downloadButtonX, formSizes.downloadButtonY);
            playGamesButton.Width = formSizes.buttonWidth;
            playGamesButton.Height = formSizes.buttonLength;
            playGamesButton.Location = new Point(formSizes.playButtonX, formSizes.playButtonY);
            updateRecentlyDownloadedList();
            this.WindowState = FormWindowState.Maximized;
        }

        public void setUpSelectedControls()
        {
            selectedGamePic = new PictureBox();
            Bitmap resizedImage = new Bitmap(selectedGamePic.ErrorImage, new Size(formSizes.selectedPicLength, formSizes.selectedPicHeight));
            selectedGamePic.Image = resizedImage;
            selectedGamePic.Size = new Size(formSizes.selectedPicLength, formSizes.selectedPicHeight);
            selectedGamePic.Location = new Point(formSizes.selectedPicX, formSizes.selectedPicY);
            selectedGameName.AutoSize = true;
            selectedGameName.Text = "Name: ";
            selectedGameName.Location = new Point(formSizes.selectedLabelsX, formSizes.selectedLabelY);
            selectedGameSystem.AutoSize = true;
            selectedGameSystem.Text = "System: ";
            selectedGameSystem.Location = new Point(formSizes.selectedLabelsX,
                                                    formSizes.selectedLabelY - (formSizes.inbeetweenlabelY));
            selectedGameGenre1.Text = "Genres: ";
            selectedGameGenre1.Location = new Point(formSizes.selectedLabelsX,
                                                    formSizes.selectedLabelY - (formSizes.inbeetweenlabelY) * 2);
            selectedGameGenre2.Text = "";
            selectedGameGenre2.Location = new Point(formSizes.selectedLabelsX,
                                                    formSizes.selectedLabelY - (formSizes.inbeetweenlabelY) * 3);
            selectedGameGenre3.Text = "";
            selectedGameGenre3.Location = new Point(formSizes.selectedLabelsX,
                                                    formSizes.selectedLabelY - (formSizes.inbeetweenlabelY) * 4);
            playRomButton.Text = "Lanuch Game!";
            playRomButton.Size = new Size(formSizes.playSelectedGameLength, formSizes.playSelectedGameHeight);
            playRomButton.Location = new Point(formSizes.playSelectedGameX, formSizes.playSelectedGameY);
            playRomButton.Click += new EventHandler(playRom);
            Controls.Add(playRomButton);
            Controls.Add(selectedGameName);
            Controls.Add(selectedGamePic);
            Controls.Add(selectedGameSystem);
            Controls.Add(selectedGameGenre1);
            Controls.Add(selectedGameGenre2);
            Controls.Add(selectedGameGenre3);
        }

        public void playRom(object sender, EventArgs e)
        {
            if(selectedRom == null)
            {
                return;
            }
            databaseHelper.updateRecentlyPlayed(selectedRom);
            string fileType = getFileType(selectedRom.fileName);
            string pathForFile = FolderNames.emulationPathsFolderPath + "\\" + selectedRom.systemName + ".txt";
            if(File.Exists(pathForFile))
            {
                string[] executions = File.ReadAllLines(pathForFile);
                openRom(executions[0], executions[1], selectedRom);
            }
            else
            {
                setUpNewEmulator(fileType, selectedRom, pathForFile);
            } 
        }

        public void setUpNewEmulator(string fileType, playableRom romToOpen, string pathForText)
        {
            setUpEmulatorForm newForm = new setUpEmulatorForm(fileType, romToOpen, this);
            newForm.ShowDialog();
            if(File.Exists(pathForText))
            {
                string[] executions = File.ReadAllLines(pathForText);
                string newCommandLine = findCommandLineOpen(executions[0], romToOpen);
                List<string> stringToWrite = new List<string>();
                stringToWrite.Add(newCommandLine);
                File.AppendAllLines(pathForText, stringToWrite);
            }
            updateRecentlyPlayedList();
        }

        public string findCommandLineOpen(string pathOfExecution, playableRom romToOpen)
        {
            romStartOkayForm checkForm;
            foreach (String candidateOpen in commandLineHelper.openLines)
            {
                System.Diagnostics.Process newPros = System.Diagnostics.Process.Start(pathOfExecution, candidateOpen + " " + "\"" + romToOpen.fileName + "\"");
                checkForm = new romStartOkayForm(candidateOpen);
                var check = checkForm.ShowDialog();
                if (checkForm.startCorrectly == true)
                {
                    return (candidateOpen);
                }
                else
                {
                    if (newPros.HasExited == false)
                    {
                        newPros.Kill();
                        newPros.Close();
                    }
                    if (checkForm.commandLineToUse != null)
                    {
                        System.Diagnostics.Process userGiven = System.Diagnostics.Process.Start(pathOfExecution, checkForm.commandLineToUse + " " + "\"" + romToOpen.fileName + "\"");
                        romStartOkayForm userGivenForm = new romStartOkayForm(checkForm.commandLineToUse);
                        var specialCheck = userGivenForm.ShowDialog();
                        if (userGivenForm.startCorrectly == true)
                        {
                            return (checkForm.commandLineToUse);
                        }
                        else
                        {
                            if (userGiven.HasExited == false)
                            {
                                userGiven.Kill();
                                userGiven.Close();
                            }
                        }
                    }
                }
            }
            return (null);
        }

        public void openRom(string pathOfExecution, string commandLineToUse, playableRom romToOpen)
        {
            System.Diagnostics.Process newPros = System.Diagnostics.Process.Start(pathOfExecution,commandLineToUse + " " + "\"" + romToOpen.fileName + "\"");
            this.HideForm();
            newPros.WaitForExit();
            updateRecentlyPlayedList();
            this.ShowForm();
        }

        public string getFileType(string stringToParse)
        {
            int startPoint = stringToParse.IndexOf('.') + 1;
            string retString = stringToParse.Substring(startPoint);
            return (retString);
        }

        public void setSelectedGame(playableRom newSelect)
        {
            selectedRom = newSelect;
            Bitmap org = new Bitmap(selectedRom.imageLocation);
            Bitmap resized = new Bitmap(org, new Size(formSizes.selectedPicLength, formSizes.selectedPicHeight));
            selectedGamePic.Image = resized;
            selectedGameName.Text = "Game: " + selectedRom.gameName;
            selectedGameSystem.Text = "System: " + selectedRom.systemName;
        }

        private void endApplication(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void playGamesButton_Click(object sender, EventArgs e)
        {
           
        }
        
        public void updateLocationAndSize(Point newLocation, Size newSize)
        {
            this.Location = newLocation;
            this.Size = newSize;
            
        }

        public void HideForm()
        {
            this.WindowState = FormWindowState.Minimized;
            
        }

        public void ShowForm()
        {
            this.WindowState = FormWindowState.Maximized;
        }

        public void updateRecentlyDownloadedList()
        {
            updateRecentlyPlayedList(); //TAKE OUT ONCE DONE TESTING
            clearRecentDownloaded();
            List<playableRom> recentList = databaseHelper.getRecently(tableNames.recentlyDownloadedTableName);
            int newY = formSizes.recentlyDownloadedY;
            foreach(playableRom rom in recentList)
            {
                rom.addSender(this);
                Point romPoint = new Point(formSizes.recentlyDownloadedX, newY);
                rom.showRom(romPoint, this.Controls);
                newY = newY - formSizes.recentlyDownloadedSpaceBetween;
            }
            recentlyDownloadedList = recentList;
        }

        public void updateRecentlyPlayedList()
        {
            clearRecentlyPlayed();
            //change here when recently played table is working
            List<playableRom> recentList = databaseHelper.getRecently(tableNames.recentlyPlayedTableName);
            int newY = formSizes.recentlyPlayedY;
            foreach (playableRom rom in recentList)
            {
                rom.addSender(this);
                Point romPoint = new Point(formSizes.recentlyPlayedX, newY);
                rom.showRom(romPoint, this.Controls);
                newY = newY - formSizes.recentlyPlayedSpaceBetween;
            }
            recentlyPlayedList = recentList;
        }

        public void clearRecentlyPlayed()
        {
            if (recentlyPlayedList == null)
            {
                return;
            }
            foreach (playableRom rom in recentlyPlayedList)
            {
                rom.hideRom(this.Controls);
                rom.dispose();
            }
        }

        public void clearRecentDownloaded()
        {
            if(recentlyDownloadedList == null)
            {
                return;
            }
            foreach(playableRom rom in recentlyDownloadedList)
            {
                rom.hideRom(this.Controls);
                rom.dispose();
            }
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            downloadForm.updateLocationAndSize(this.Location, this.Size);
            downloadForm.Show();
            HideForm();
            //this.Visible = false;
        }

        private void recentlyPlayedLabel_Click(object sender, EventArgs e)
        {

        }

        private void searchText_TextChanged(object sender, EventArgs e)
        {

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            populatePages();
        }

        private void clearPages()
        {
            foreach(pageOfRoms page in romPages)
            {
                if (page != null)
                {
                    page.dispose();
                }
            }
            currentPage = -1;
        }

        private void populatePages()
        {
            clearPages();
            romPages.Clear();
            List<playableRom> totalList = databaseHelper.queryPlayableRom("");
            if (totalList.Count > 0)
            {
                int spaceLeftInPage = formSizes.lengthOfPage;
                int heightLeftInPage = formSizes.depthOfPage;
                int pageNum = 1;
                //pageOfRoms currentPage = new pageOfRoms(pageNum);
                romPages.Add(null); //get rid of index 0 so it is base 1 for pages
                romPages.Add(new pageOfRoms(pageNum, this));
                foreach(playableRom rom in totalList)
                {
                    rom.addSender(this);
                    if(spaceLeftInPage == 0)
                    {
                        heightLeftInPage--;
                        if(heightLeftInPage == 0)
                        {
                            pageNum++;
                            romPages.Add(new pageOfRoms(pageNum, this));
                            heightLeftInPage = formSizes.depthOfPage;
                        }
                        spaceLeftInPage = formSizes.lengthOfPage;
                    }
                    romPages[pageNum].addPlayableRom(rom); //pagenum - 1 since we need base zero instead of base 1
                    spaceLeftInPage--;
                }
                switchPage(1);
            }
        }

        public void switchPage(int pageNum)
        {
            if(pageNum == currentPage || pageNum < 0 || pageNum > romPages.Count)
            {
                return;
            }
            if (currentPage > 0)
            {
                romPages[currentPage].hidePage();
            }
            currentPage = pageNum;
            romPages[currentPage].showPage();
            //this.Refresh();
        }

        private void resetLabels()
        {
            Bitmap resizedImage = new Bitmap(selectedGamePic.ErrorImage, new Size(formSizes.selectedPicLength, formSizes.selectedPicHeight));
            selectedGamePic.Image = resizedImage;
            selectedGamePic.ImageLocation = null;
            selectedGameName.Text = "";
            selectedGameSystem.Text = "";
            selectedGameGenre1.Text = "";
            selectedGameGenre2.Text = "";
            selectedGameGenre3.Text = "";
        }

        private void deleteGameButton_Click(object sender, EventArgs e)
        {
            if (selectedRom != null)
            {
                string romPath = selectedRom.fileName;
                string picPath = selectedRom.imageLocation;
                databaseHelper.removeRom(selectedRom);
                selectedRom.dispose();
                selectedRom = null;
                updateRecentlyDownloadedList();
                updateRecentlyPlayedList();
                FileInfo romToDelete = new FileInfo(romPath);
                while(IsFileLocked(romToDelete))
                {
                    Application.DoEvents();
                }
                File.Delete(romPath);
                FileInfo picToDelete = new FileInfo(picPath);
                while (IsFileLocked(picToDelete))
                {
                    Application.DoEvents();
                }
                File.Delete(picPath);
            }
        }

        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        private void rebuildDatabaseButton_Click(object sender, EventArgs e)
        {
            databaseHelper.rebuildDatabase();
        }

        private void romPlayer_Load(object sender, EventArgs e)
        {

        }
    }
}

