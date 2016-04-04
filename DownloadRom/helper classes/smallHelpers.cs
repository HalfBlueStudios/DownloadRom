using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadRom
{
    public class commandLineHelper
    {
        public static string[] openLines = { "", "-o", "-g" };
    }

    public class htmlHelper
    {
        public static string getHtml(string websiteToGet)
        {
            WebRequest requestOfWeb = WebRequest.CreateHttp(websiteToGet);
            //((HttpWebRequest)requestOfWeb).UserAgent = userAgentChrome;
            WebResponse responseOfWeb = requestOfWeb.GetResponse();
            Stream resultsStream = ((HttpWebResponse)responseOfWeb).GetResponseStream();
            StreamReader streamRead = new StreamReader(resultsStream);
            string htmlText = streamRead.ReadToEnd();
            return (htmlText);
        }
    }

    public class browserHelper
    {
        public static void waitForBrowser(WebBrowser browserToWait)
        {
            while (browserToWait.IsBusy)
                System.Windows.Forms.Application.DoEvents();
            for (int i = 0; i < 500; i++)
                if (browserToWait.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
                {
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                }
                else
                    break;
        }
    }

    public class convertNameHelper
    {
        public static string convertName(string sys)
        {
            if (sys[sys.Length - 1] == ' ')
            {
                sys = sys.Substring(0, sys.Length - 1);
            }
            switch (sys)
            {
                case "Nintendo Game Cube":
                    return ("GCN");

                case "Nintendo 64":
                    return ("N64");

                case "Nintendo Entertainment System":
                    return ("NES");

                case "Super Nintendo Entertainment System (SNES)":
                    return ("SNES");

                case "Nintendo Gameboy Advance":
                    return ("GBA");

                case "M.A.M.E. - Multiple Arcade Machine Emulator":
                case "mame":
                    return ("M.A.M.E");

                case "dc":
                case "Sega Dreamcast":
                    return ("Dreamcast");

                case "Sony Playstation - Old":
                    return ("psx");

                case "Nintendo Game Boy Color":
                    return ("Game Boy Color");

                case "Sony Playstation 2":
                    return ("ps2");
            }
            return (sys);
        }
    }

    public class searchOkayHelper
    {
        public static bool searchOkay(string searchTerms, string gameName)
        {
            string[] searchParts = searchTerms.Split(' ');
            foreach (string part in searchParts)
            {
                if (gameName.ToUpper().Contains(part.ToUpper()) == false)
                {
                    return (false);
                }
            }
            return (true);
        }
    }

    public class textHelper
    {
        public static string removeExtraParts(string retName)
        {
            int partToCut = retName.IndexOf("(");
            if (partToCut > 0)
            {
                while (retName[partToCut - 1] == ' ')
                {
                    partToCut--;
                }
                retName = retName.Substring(0, partToCut);
            }
            return (retName);
        }
    }
}
