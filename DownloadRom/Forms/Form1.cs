using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Data.Sql;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Net.Mime;
using System.Threading;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;

namespace DownloadRom
{
    public partial class RomDownloader : Form
    {
        bool UsbMode = false;
        bool currentlyDownloading = false;
        Queue<downloadableRom> downloadQueue = new Queue<downloadableRom>();
        private string coolRomName = "Coolrom";
        private string emuparadiseName = "Emuparadise";
        private string userAgentChrome = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36";
        private string SearchTerms = "";
        private string currentSelectedLanguage = "";
        private string currentSelectedSite = "";
        private string gameFolderLocation;
        private const string allLanguageName = "All";
        private WebClient downloader;
        private List<romList> masterList;
        private List<romList> currentlyDownloadedList;
        private downloadableRom currentDownloadRom;
        private romPlayer playerForm;

        public RomDownloader(string newGameFolderLocation, romPlayer newPlayer)
        {
            InitializeComponent();
            playerForm = newPlayer;
            this.Width = formSizes.formWidth;
            this.Height = formSizes.formLength;
            downloadGamesButton.Width = formSizes.buttonWidth;
            downloadGamesButton.Height = formSizes.buttonLength;
            downloadGamesButton.Location = new Point(formSizes.downloadButtonX, formSizes.downloadButtonY);
            playGamesButton.Width = formSizes.buttonWidth;
            playGamesButton.Height = formSizes.buttonLength;
            playGamesButton.Location = new Point(formSizes.playButtonX, formSizes.playButtonY);
            LanguageOption initalLanguage = new LanguageOption(allLanguageName, 0);
            chooseLanguageBox.Items.Add(initalLanguage);
            chooseLanguageBox.SelectedItem = initalLanguage;
            SearchTermBox.KeyDown += new KeyEventHandler(tb_KeyDown);
            gameFolderLocation = newGameFolderLocation;
            currentlyDownloadedList = databaseHelper.queryRoms("");
            this.WindowState = FormWindowState.Maximized;
        }
    

        public void updateLocationAndSize(Point newLocation, Size newSize)
        {
            this.Location = newLocation;
            this.Size = newSize;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchButton_Click(sender, null);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            depopulateGameList();
            depopulateSystemList();
            SearchTerms = SearchTermBox.Text;
            //List<string> pictureUrls = getUrls(listOfUrls[0]);
            List<romList> queryResults = queryAllSites();
            populateSystemList(queryResults, null);
        }

        public List<romList> queryAllSites()
        {
            formBrowser.ScriptErrorsSuppressed = true;
            formBrowser.Visible = false;
            List<List<romList>> allQuerys = new List<List<romList>>();
            List<romList> coolRomList = CoolRomQuery.startCoolRomQuery(formBrowser, SearchTerms, chooseSiteBox);
            allQuerys.Add(coolRomList);
            List<romList> emuparadiseRomList = EmuparadiseQuery.startEmpuparadiseQuery(formBrowser, SearchTerms, chooseSiteBox);
            allQuerys.Add(emuparadiseRomList);
            //List<romList> coolRomList = queryCoolRoms();
            masterList = combineLists(allQuerys);
            return (applyFilters(masterList));
        }

        public void HideForm()
        {
            this.WindowState = FormWindowState.Minimized;

        }

        public void ShowForm()
        {
            this.WindowState = FormWindowState.Maximized;
        }


        public void refreshSiteBox(romList newList)
        {
            romSite chosenSite = (romSite)chooseSiteBox.SelectedItem;
            chooseSiteBox.Items.Clear();
            List<romSite> listOfSites = new List<romSite>();
            foreach(downloadableRom rom in newList.listOfRoms)
            {
                bool inserted = false;
                for(int i = 0; i < listOfSites.Count; i++)
                {
                    if(rom.siteFrom == listOfSites[i].siteName)
                    {
                        listOfSites[i].increaseNumRoms();
                        inserted = true;
                    }
                }
                if (inserted == false)
                {
                    romSite newSite = new romSite(rom.siteFrom, 1);
                    listOfSites.Add(newSite);
                }
            }
            romSite newAllSite = new romSite(allLanguageName, newList.listOfRoms.Count);
            listOfSites.Add(newAllSite);
            listOfSites.OrderByDescending(X => X.numRoms);
            foreach(romSite site in listOfSites)
            {
                chooseSiteBox.Items.Add(site);
            }
            if(chosenSite == null)
            {
                chooseSiteBox.SelectedItem = newAllSite;
            }
            else
            {
                foreach(romSite site in listOfSites)
                {
                    if(site.siteName == chosenSite.siteName)
                    {
                        chooseSiteBox.SelectedItem = site;
                    }
                }
            }
        }


        public List<romList> applyFilters(List<romList> preFilteredList)
        {
            List<romList> retList = deepCopyList(preFilteredList);
            if (chooseSiteBox.SelectedItem != null &&
                ((romSite)chooseSiteBox.SelectedItem).siteName != allLanguageName)
            {
                retList = applySiteFilter(retList);
            }
            if (chooseLanguageBox.SelectedItem != null && 
                ((LanguageOption)chooseLanguageBox.SelectedItem).languageName != allLanguageName)
            {
                retList = applyLanguageFilter(retList);
            }
            if(uniqueFilterBox.Checked == true)
            {
                retList = applyUniqueFilter(retList);
            }
            if (alreadyDownloadedFilter.Checked == true)
            {
                retList = applyAlraeadyDownloadedFilter(retList);
            }

            return (retList);
        }

        public List<romList> applyUniqueFilter(List<romList> initalList)
        {
            List<romList> uniqueRetList = deepCopyList(initalList);
            foreach(romList list in initalList)
            {
                foreach(downloadableRom rom in list.listOfRoms)
                {
                    for(int i = 0; i < uniqueRetList.Count; i++)
                    {
                        for(int k = 0; k < uniqueRetList[i].listOfRoms.Count; k++)
                        {
                            if (rom.urlToDownload != uniqueRetList[i].listOfRoms[k].urlToDownload && 
                                isNotUnique(rom, uniqueRetList[i].listOfRoms[k]))
                            {
                                uniqueRetList[i].listOfRoms.Remove(rom);
                            }
                        }
                    }
                }
            }
            return (uniqueRetList);
        }

        public bool isNotUnique(downloadableRom firstRom, downloadableRom secondRom)
        {
            string firstRomName =  textHelper.removeExtraParts(firstRom.gameName);
            string secondRomName = textHelper.removeExtraParts(secondRom.gameName);
            if(firstRomName == secondRomName)
            {
                //MessageBox.Show(firstRomName + " is equal to " + secondRomName);
                return (true);
            }
            else
            {
                return (false);
            }

        }

        public List<romList> applyLanguageFilter(List<romList> initalList)
        {
            List<romList> languageRetList = deepCopyList(initalList);
            if (initalList == null || chooseLanguageBox.SelectedItem == null)
            {
                return (initalList);
            }
            string chosenLanguage = ((LanguageOption)chooseLanguageBox.SelectedItem).languageName;
            for(int i = 0; i < initalList.Count; i++)
            {
                for(int k = 0; k < initalList[i].listOfRoms.Count; k++)
                {
                    if(initalList[i].listOfRoms[k].language != chosenLanguage)
                    {
                        languageRetList[i].listOfRoms.Remove(initalList[i].listOfRoms[k]);
                    }
                }
            }
            return (languageRetList);
        }

        public List<romList> applyAlraeadyDownloadedFilter(List<romList> initalList)
        {
            if(currentlyDownloadedList == null)
            {
                return (initalList);
            }
            List<romList> retList = deepCopyList(initalList);
            for(int i = 0; i < initalList.Count; i++)
            {
                for(int k = 0; k < initalList[i].listOfRoms.Count; k++)
                {
                    for(int j = 0; j < currentlyDownloadedList.Count; j++)
                    {
                        for(int f = 0; f < currentlyDownloadedList[j].listOfRoms.Count; f++)
                        {
                            if(currentlyDownloadedList[j].listOfRoms[f].Equals(initalList[i].listOfRoms[k]))
                            {
                                retList[i].listOfRoms.Remove(initalList[i].listOfRoms[k]);
                            }
                               
                        }
                    }
                }
            }
            /*
            foreach(romList listOfRoms in retList)
            {
                foreach(downloadableRom romToCheck in listOfRoms.listOfRoms)
                {
                    foreach(romList downloadedList in downloadedRoms)
                    {
                        if(downloadedList.listOfRoms.Contains(romToCheck))
                        {
                            listOfRoms.listOfRoms.Remove(romToCheck);
                            MessageBox.Show("REMOVING " + romToCheck.gameName);
                        }
                    }
                }
            }
            */
            return (retList);
        }

        public List<romList> applySiteFilter(List<romList> initalList)
        {
            if (initalList == null || chooseSiteBox.SelectedItem == null)
            {
                return (initalList);
            }
            string siteToUse = ((romSite)chooseSiteBox.SelectedItem).siteName;
            List<romList> retList = deepCopyList(initalList);
            for (int i = 0; i < initalList.Count; i++)
            {
                for (int k = 0; k < initalList[i].listOfRoms.Count; k++)
                {
                    if (initalList[i].listOfRoms[k].siteFrom != siteToUse)
                    {
                        retList[i].listOfRoms.Remove(initalList[i].listOfRoms[k]);
                    }
                }
            }
            return (retList);
        }


        public List<romList> deepCopyList(List<romList> otherList)
        {
            List<romList> retList = new List<romList>();
            foreach(romList listToAdd in otherList)
            {
                romList newList = new romList(listToAdd);
                retList.Add(newList);                
            }
            return (retList);
        }

        private List<romList> combineLists(List<List<romList>> allQuerys)
        {
            List<romList> retList = new List<romList>();
            foreach (List<romList> queryList in allQuerys)
            {
                foreach (romList insertList in queryList)
                {
                    bool inserted = false;
                    foreach (romList retListOfRoms in retList)
                    {
                        if (retListOfRoms.systemName == insertList.systemName)
                        {
                            foreach (downloadableRom romToAdd in insertList.listOfRoms)
                            {
                                retListOfRoms.listOfRoms.Add(romToAdd);
                                inserted = true;
                            }
                        }
                    }
                    if (inserted == false)
                    {
                        retList.Add(insertList);
                    }
                }
            }
            return (retList);
        }

        private void insertRomIntoCurrentlyDownloaded(downloadableRom romToInsert)
        {
            bool inserted = false;
            foreach(romList listToCheck in currentlyDownloadedList)
            {
                if(listToCheck.systemName == romToInsert.systemName)
                {
                    listToCheck.listOfRoms.Add(romToInsert);
                    inserted = true;
                }
            }
            if(inserted == false)
            {
                romList newList = new romList(romToInsert.systemName);
                newList.listOfRoms.Add(romToInsert);
                currentlyDownloadedList.Add(newList);
            }
        }

        private void resultsBox_TextChanged(object sender, EventArgs e)
        {


        }

        private void SearchTermBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void populateSystemList(List<romList> listOfRoms, string nameBefore)
        {
            if (listOfRoms.Count > 0)
            {
                var sortedList = listOfRoms.OrderByDescending(x => x.listOfRoms.Count);
                foreach (romList roms in sortedList)
                {
                    chooseSystemBox.Items.Add(roms);
                }
                if (nameBefore == null)
                {
                    chooseSystemBox.SelectedIndex = 0;
                }
                else
                {
                    foreach (romList list in sortedList)
                    {
                        if (list.systemName == nameBefore)
                        {
                            chooseSystemBox.SelectedItem = list;
                        }
                    }
                }
            }
        }
        
        private void depopulateSystemList()
        {
            chooseSystemBox.Items.Clear();
        }

        private void populateGameList(romList listToUse)
        {
            foreach (downloadableRom rom in listToUse.listOfRoms)
            {
                gameListBox.Items.Add(rom);
            }
        }

        private void depopulateGameList()
        {
            gameListBox.Items.Clear();
        }


        private void depopulateLanguageList()
        {
            chooseLanguageBox.Items.Clear();
        }

        private void populateLanguageList(romList listOfRoms)
        {
            List<LanguageOption> languages = new List<LanguageOption>();
            int totalGames = 0;
            foreach (downloadableRom rom in listOfRoms.listOfRoms)
            {
                bool inserted = false;
                foreach (LanguageOption option in languages)
                {
                    if (option.languageName == rom.language)
                    {
                        option.addToGameCount();
                        inserted = true;
                        break;
                    }
                }
                if (inserted == false)
                {
                    LanguageOption newOption = new LanguageOption(rom.language, 1);
                    languages.Add(newOption);
                }
                totalGames++;
            }
            LanguageOption allOption = new LanguageOption(allLanguageName, totalGames);
            languages.Add(allOption);
            var sortedList = languages.OrderByDescending(X => X.gameCount);
            foreach(LanguageOption option in sortedList)
            {
                chooseLanguageBox.Items.Add(option);
            }
        }

        private void refreshLanguageList(romList listOfRoms)
        {
            if (listOfRoms != null)
            {
                string languageBefore = "";
                if (chooseLanguageBox.SelectedItem != null)
                {
                    languageBefore = ((LanguageOption)chooseLanguageBox.SelectedItem).languageName;
                }
                depopulateLanguageList();
                populateLanguageList(listOfRoms);
                bool foundLanguage = false;
                foreach(LanguageOption option in chooseLanguageBox.Items)
                {
                    if(option.languageName == languageBefore)
                    {
                        chooseLanguageBox.SelectedItem = option;
                        foundLanguage = true;
                        break;
                    }
                }
                if(foundLanguage == false)
                {
                    chooseLanguageBox.SelectedIndex = 0;
                }
            }
        }


        private void chooseSystemBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            depopulateGameList();
            romList selectedSystem = (romList)chooseSystemBox.SelectedItem;
            populateGameList(selectedSystem);
            foreach(romList list in masterList)
            {
                if(list.systemName == selectedSystem.systemName)
                {
                    refreshLanguageList(list);
                    refreshSiteBox(list);
                }
            }
            //refreshLanguageList(selectedSystem);

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void gameListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            downloadableRom temp = (downloadableRom)gameListBox.SelectedItem;
            Image newGameImage = googleImageQuery.getGooglePic(temp.gameName + " " + temp.systemName + " ", false);
            if (newGameImage != null)
            {
                ResultsPic.Image = newGameImage;
            }
        }

        private void downloadFile(string whereToDownload, string fileName, downloadableRom chosenRom)
        {
            statusLabel.Text = "Downloading...";
            labelDownloaded.Text = "0";
            progressBar.Value = 0;
            labelPerc.Text = "0%";
            downloader = new WebClient();
            //downloader.Headers.Add("Referer", ((downloadableRom)gameListBox.SelectedItem).urlToDownload + "-download");
            downloader.Headers.Add("Referer", chosenRom.urlToDownload + "-download");
            downloader.Headers.Add("Cookie: " + GetCookies());
            //(downloader).UserAgent = userAgentChrome;
            downloader.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadCompleted);
            downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloadProgressChanged);
            //string downloadFileName = FolderNames.tempDownloads + "\\" + getFileDownloadName(whereToDownload);
            downloader.DownloadFileTaskAsync(whereToDownload, fileName);
        }

        private string getFileDownloadName(string urlToDownload)
        {
            var webClient = new WebClient();

            //get file name
            string debugString = webClient.DownloadString(urlToDownload);
            var request = WebRequest.Create(urlToDownload);
            var response = request.GetResponse();
            var contentDisposition = response.Headers;
            const string contentFileNamePortion = "filename=";
            return (null);
            /*
            var fileNameStartIndex = contentDisposition.IndexOf(contentFileNamePortion, StringComparison.InvariantCulture) + contentFileNamePortion.Length;
            var originalFileNameLength = contentDisposition.Length - fileNameStartIndex;
            var originalFileName = contentDisposition.Substring(fileNameStartIndex, originalFileNameLength);
            return (originalFileName);
            */
        }

        // The event that will fire whenever the progress of the WebClient is changed
        private void downloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Calculate download speed and output it to labelSpeed.
            labelSpeed.Text = string.Format("unkown");

            // Update the progressbar percentage only when the value is not the same.
            progressBar.Value = e.ProgressPercentage;

            // Show the percentage on our label.
            labelPerc.Text = e.ProgressPercentage.ToString() + "%";

            // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
            labelDownloaded.Text = string.Format("{0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        }

        private void downloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            labelSpeed.Text = "0 MBps / 0 MBps";
            labelDownloaded.Text = "0%";
            progressBar.Value = 0;
            if (e.Cancelled == true)
            {
                statusLabel.ForeColor = Color.Red;
                statusLabel.Text = "Cancelled!";
            }
            else
            {
                statusLabel.Text = "Unpacking...";
                unpackingHelper.unpackRom(currentDownloadRom, gameFolderLocation);
                databaseHelper.insertRom(currentDownloadRom);
                insertRomIntoCurrentlyDownloaded(currentDownloadRom);
                refreshListWithFilters();
                statusLabel.ForeColor = Color.Green;
                statusLabel.Text = "Downloaded!";
            }
            if(downloadQueue.Count > 0)
            {
                downloadableRom nextDownload = downloadQueue.Dequeue();
                downloadQueueBox.Items.Remove(nextDownload);
                startDownload(nextDownload);
            }
            else
            {
                currentlyDownloading = false;
            }
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            //browserOfWeb.Navigate(((downloadableRom)gameListBox.SelectedItem).urlToDownload);
            //File.WriteAllText("tester.txt", getHtml(@"http://www.emuparadise.me/roms/get-download.php?gid=39952&token=2793bb7f5fac95bea6253d5e135adf41&mirror_available=true"));
            downloadableRom chosenRom = (downloadableRom)gameListBox.SelectedItem;
            if (currentlyDownloading == true)
            {
                if (downloadQueue.Contains(chosenRom) == false)
                {
                    downloadQueue.Enqueue(chosenRom);
                    downloadQueueBox.Items.Add(chosenRom);
                }
            }
            else
            {
                currentlyDownloading = true;
                startDownload(chosenRom);
            }
        }

        private void startDownload(downloadableRom chosenRom)
        {
            chosenRom.gameName = databaseHelper.sanatizeString(chosenRom.gameName);
            downloadNameLabel.Text = chosenRom.gameName;
            statusLabel.ForeColor = Color.Black;
            statusLabel.Text = "Retrieving download link...";
            currentDownloadRom = chosenRom;
            string downloadLink = chosenRom.getDownloadLink();
            if (downloadLink == null)
            {
                return;
            }
            string fileName = chosenRom.gameName + chosenRom.extractionType;
            chosenRom.fileName = fileName;
            string tempFileName = FolderNames.tempDownloads + "\\" + fileName;
            downloadFile(downloadLink, tempFileName, chosenRom);
        }

        private string GetCookies()
        {
            if (formBrowser.InvokeRequired)
            {
                return (string)formBrowser.Invoke(new Func<string>(() => GetCookies()));
            }
            else
            {
                return formBrowser.Document.Cookie;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (romList list in masterList)
            {
                string compare = convertNameHelper.convertName(list.systemName);
                if (compare == list.systemName)
                {
                    getSystemText.Text += list.systemName + Environment.NewLine;
                }
            }
        }

        private void removeFromDownloadQueueButton_Click(object sender, EventArgs e)
        {
            int selectCount = downloadQueueBox.SelectedItems.Count;
            for (int i = 0; i < selectCount; i++)
            {
                downloadQueueBox.Items.Remove(downloadQueueBox.SelectedItems[0]);
            }
            
        }

        private void cancelDownloadButton_Click(object sender, EventArgs e)
        {
            if(currentlyDownloading == true && downloader != null)
            {
                downloader.CancelAsync();
            }
        }

        private void refreshListWithFilters()
        {
            if(chooseSystemBox.SelectedItem == null)
            {
                return;
            }
            string nameBefore = ((romList)chooseSystemBox.SelectedItem).systemName;
            string languageBefore = ((LanguageOption)chooseLanguageBox.SelectedItem).languageName;
            depopulateSystemList();
            List<romList> filterdList = applyFilters(masterList);
            populateSystemList(filterdList, nameBefore);
            depopulateGameList();
            romList chosenList = (romList)chooseSystemBox.SelectedItem;
            populateGameList(chosenList);
            //refreshLanguageList((romList)chooseSystemBox.SelectedItem);
        }
        
        private void alreadyDownloadedFilter_CheckedChanged(object sender, EventArgs e)
        {
            refreshListWithFilters();
        }

        private void uniqueFilterBox_CheckedChanged(object sender, EventArgs e)
        {
            refreshListWithFilters();
        }

        private void chooseLanguageBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            string newLanguage = ((LanguageOption)chooseLanguageBox.SelectedItem).languageName;
            if (newLanguage == currentSelectedLanguage)
            {
                return;
            }
            currentSelectedLanguage = newLanguage;
            refreshListWithFilters();
            /*
            string newLanguage = ((LanguageOption)chooseLanguageBox.SelectedItem).languageName;
            if (newLanguage != currentSelectedLanguage)
            {
                MessageBox.Show(newLanguage + " is not " + currentSelectedLanguage);
                refreshListWithFilters();
                currentSelectedLanguage = newLanguage;
            }
            */
        }

        private void chooseSiteBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string newSite = ((romSite)chooseSiteBox.SelectedItem).siteName;
            if (newSite == currentSelectedSite)
            {
                return;
            }
            currentSelectedSite = newSite;
            refreshListWithFilters();
        }

        private void downloadGamesButton_Click(object sender, EventArgs e)
        {

        }

        private void playGamesButton_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            playerForm.updateLocationAndSize(this.Location, this.Size);
            playerForm.updateRecentlyDownloadedList();
            playerForm.ShowForm();
        }

        private void RomDownloader_Load(object sender, EventArgs e)
        {

        }
    }

}

