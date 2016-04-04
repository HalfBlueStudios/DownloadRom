using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadRom
{
    public class CoolRomQuery
    {
        private static string coolRomUrl = @"http://coolrom.com";
        private static string coolRomsSearch = @"http://coolrom.com/search?q=";
        private static string coolRomsStartTop25List = @"/roms/psx/39719/Tekken_3.php";
        private static string coolRomName = "Coolrom";
        private static string userSearchTerms;

        public static List<romList> startCoolRomQuery(WebBrowser browserOfWeb, string searchTerms, ComboBox siteBox)
        {
            userSearchTerms = searchTerms;
            string htmlText = htmlHelper.getHtml(coolRomsSearch + searchTerms);
            browserOfWeb.Navigate(coolRomsSearch + searchTerms);
            List<string> allOptions = getUrlsCoolRom(htmlText, searchTerms);
            List<romList> listOfRoms = parseCoolRomUrl(allOptions);
            return (listOfRoms);
        }

        private static List<string> getUrlsCoolRom(string html, string searchTerms)
        {
            var urls = new List<string>();
            int ndx = html.IndexOf(searchTerms, StringComparison.Ordinal);
            if (ndx == -1)
            {
                return (urls);
            }
            ndx = html.IndexOf("<a", ndx, StringComparison.Ordinal);

            while (ndx >= 0)
            {
                ndx = html.IndexOf("href=", ndx, StringComparison.Ordinal);
                ndx = ndx + 6;
                int ndx2 = html.IndexOf(".php", ndx, StringComparison.Ordinal) + 5;
                string url = html.Substring(ndx, ndx2 - ndx - 1);
                int countOfSlashes = url.Count(x => x == '/');
                if (url == coolRomsStartTop25List)
                {
                    break;
                }
                if (url.Contains(".php") && url.Contains("roms") && countOfSlashes == 4)
                {
                    urls.Add(url);
                }
                ndx = html.IndexOf("href=", ndx, StringComparison.Ordinal);
            }
            return urls;
        }

        private static List<romList> parseCoolRomUrl(List<string> listOfurl)
        {
            List<romList> retList = new List<romList>();
            foreach (string url in listOfurl)
            {
                /*
                0 - nothing
                1- roms 
                2 - system
                3- number
                4 - game name
                */
                string[] partsOfUrl = url.Split('/');
                string gameName = partsOfUrl[4].Replace('_', ' ');
                if (searchOkayHelper.searchOkay(userSearchTerms, gameName) == true)
                {
                    coolRom newRom = new coolRom(partsOfUrl[2], partsOfUrl[4].Substring(0, partsOfUrl[4].Length - 4).Replace('_', ' '), coolRomUrl + url, coolRomName);
                    bool added = false;
                    foreach (romList listToCheck in retList)
                    {
                        if (listToCheck.systemName == newRom.systemName)
                        {
                            listToCheck.listOfRoms.Add(newRom);
                            added = true;
                        }
                    }
                    if (added == false)
                    {
                        romList newRomList = new romList(newRom.systemName);
                        newRomList.listOfRoms.Add(newRom);
                        retList.Add(newRomList);
                    }
                }
            }
            return (retList);
        }

        public static string getCoolRomDownloadLink(string urlToStart, downloadableRom romDownload)
        {
            string htmlText = htmlHelper.getHtml(urlToStart);
            int startPosition = htmlText.IndexOf("window.open") + 13;
            int endpositon = htmlText.IndexOf(',', startPosition) - 1;
            string nextUrl = coolRomUrl + htmlText.Substring(startPosition, endpositon - startPosition);
            string downloadHtml = htmlHelper.getHtml(nextUrl);
            romDownload.extractionType = getCoolRomExtractionType(downloadHtml);
            startPosition = downloadHtml.IndexOf("POST") + 14;
            endpositon = downloadHtml.IndexOf('>', startPosition) - 1;
            string retString = downloadHtml.Substring(startPosition, endpositon - startPosition);
            return (retString);
        }

        private static string getCoolRomExtractionType(string htmlText)
        {
            int intialSearch = htmlText.IndexOf("<b>") + 3;
            int startPosition = htmlText.IndexOf('.', intialSearch);
            int endpositon = htmlText.IndexOf('<', startPosition);
            return (htmlText.Substring(startPosition, endpositon - startPosition));
        }
    }

    public class EmuparadiseQuery
    {
        private static string emuparadiseUrl = @"http://www.emuparadise.me";
        private static string emuparadiseSearch = @"http://www.emuparadise.me/roms/search.php?query=";
        private static string emuparadiseName = "Emuparadise";
        private static string userSearchTerms;
        private static WebBrowser broswerOfWeb;

        public static List<romList> startEmpuparadiseQuery(WebBrowser newBrowser, string searchTerms, ComboBox siteBox)
        {
            broswerOfWeb = newBrowser;
            userSearchTerms = searchTerms;
            string htmlText = htmlHelper.getHtml(emuparadiseSearch + searchTerms);
            //browserOfWeb.Navigate(emuparadiseSearch + searchTerms);
            List<string> allOptions = getUrlsEmuparadise(htmlText);
            List<romList> listOfRoms = parseEmuparadiseUrl(allOptions);
            return (listOfRoms);
        }

        private static List<string> getUrlsEmuparadise(string html)
        {
            var urls = new List<string>();
            int ndx = html.IndexOf("Results Found", StringComparison.Ordinal);
            //int endPosition = html.IndexOf("community", ndx, StringComparison.Ordinal);
            // ndx = html.IndexOf("font-face: Verdana", ndx, StringComparison.Ordinal);
            ndx = html.IndexOf("class=\"roms\"", ndx, StringComparison.Ordinal);
            while (ndx >= 0)
            {
                ndx = html.IndexOf("href=", ndx, StringComparison.Ordinal);
                ndx = ndx + 6;
                int ndx2 = html.IndexOf(">", ndx, StringComparison.Ordinal);
                string url = html.Substring(ndx, ndx2 - ndx - 1);
                urls.Add(url);
                ndx = html.IndexOf("class=\"roms\"", ndx, StringComparison.Ordinal);
            }
            string results = "";
            foreach (string str in urls)
            {
                results += str + Environment.NewLine;
            }
            return urls;
        }

        private static List<romList> parseEmuparadiseUrl(List<string> listOfurl)
        {
            List<romList> retList = new List<romList>();
            foreach (string url in listOfurl)
            {
                /*
                0 - nothing
                1- system 
                2 - game name
                3- number
                */
                string[] partsOfUrl = url.Split('/');
                string systemName = "";
                string[] partsOfSystemName = partsOfUrl[1].Split('_');
                for (int i = 0; i < partsOfSystemName.Length - 1; i++)
                {
                    systemName += partsOfSystemName[i] + " ";
                }
                string gameName = partsOfUrl[2];
                gameName = gameName.Replace('_', ' ');
                if (searchOkayHelper.searchOkay(userSearchTerms, gameName) == true)
                {
                    EmuparadiseRom newRom = new EmuparadiseRom(systemName, gameName, emuparadiseUrl + url, emuparadiseName);
                    bool added = false;
                    foreach (romList listToCheck in retList)
                    {
                        if (listToCheck.systemName == newRom.systemName)
                        {
                            listToCheck.listOfRoms.Add(newRom);
                            added = true;
                        }
                    }
                    if (added == false)
                    {
                        romList newRomList = new romList(newRom.systemName);
                        newRomList.listOfRoms.Add(newRom);
                        retList.Add(newRomList);
                    }
                }
            }
            return (retList);
        }

        public static string getEmuparadiseDownloadLink(string urlToStart, downloadableRom gameRom)
        {
            setGenresEmuparadise(gameRom, htmlHelper.getHtml(gameRom.urlToDownload));
            string urlDownloadPage = urlToStart + "-download";
            string htmlText = htmlHelper.getHtml(urlDownloadPage);
            if (htmlText.Contains("CAPTCHA"))
            {
                Uri startUrl = broswerOfWeb.Url;
                broswerOfWeb.Navigate(urlDownloadPage);
                browserHelper.waitForBrowser(broswerOfWeb);
                if (broswerOfWeb.DocumentText.Contains("CAPTCHA") == false)
                {
                    htmlText = broswerOfWeb.DocumentText;
                }
                else
                {
                    webBrowserFormControl webControl = new webBrowserFormControl(broswerOfWeb);
                    webControl.ShowDialog();
                    return (getEmuparadiseDownloadLink(urlToStart, gameRom));
                }
            }
            if (htmlText.Contains("7-Zip"))
            {
                gameRom.extractionType = ".7z";
            }
            else
            {
                gameRom.extractionType = ".zip";
            }
            int initialPosition = htmlText.IndexOf("Direct Download:");
            int startPosition = htmlText.IndexOf("href=", initialPosition) + 6;
            int endpositon = htmlText.IndexOf('"', startPosition);
            string retString = emuparadiseUrl + htmlText.Substring(startPosition, endpositon - startPosition);
            retString = retString.Replace("&amp;", "&");
            return (retString);
        }

        public static void setGenresEmuparadise(downloadableRom romGame, string htmlText)
        {
            int nx = htmlText.IndexOf("Genre/");
            int genreToSet = 1;
            while (nx > 0)
            {
                int startPosition = htmlText.IndexOf('>', nx) + 1;
                int endPosition = htmlText.IndexOf('<', nx);
                string genreToAdd = htmlText.Substring(startPosition, endPosition - startPosition);
                if (genreToSet == 1)
                {
                    romGame.setGenre1(genreToAdd);
                }
                else if (genreToSet == 2)
                {
                    romGame.setGenre2(genreToAdd);
                }
                else if (genreToSet == 3)
                {
                    romGame.setGenre3(genreToAdd);
                }
                nx = htmlText.IndexOf("Genre/", endPosition);
            }
        }

        private static string getEmuparadiseExtractionType(string htmlText)
        {
            int intialSearch = htmlText.IndexOf("<b>") + 3;
            int startPosition = htmlText.IndexOf('.', intialSearch);
            int endpositon = htmlText.IndexOf('<', startPosition);
            return (htmlText.Substring(startPosition, endpositon - startPosition));
        }
    }
}
