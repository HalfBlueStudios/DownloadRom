using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DownloadRom
{
    public class googleImageQuery
    {
        private static string firstHalfPic = @"https://www.google.com/search?as_st=y&tbm=isch&hl=en&as_q=";
        private static string secondHalfPic = @"&as_epq=&as_oq=&as_eq=&cr=&as_sitesearch=&safe=images&tbs=isz:l#as_st=y&hl=en&tbs=isz:l&tbm=isch"; //@" & as_epq=&as_oq=&as_eq=&cr=&as_sitesearch=&safe=images&tbs=isz:l#imgrc=jR2HRFZzy1SWfM%3A";

        public static Image getGooglePic(string searchingTerms, bool addBoxArt)
        {
            searchingTerms = textHelper.removeExtraParts(searchingTerms);
            string pictureSearchTerms = searchingTerms.Replace(" ", "+");
            if (addBoxArt == true)
            {
                pictureSearchTerms += "Box+Art";
            }
            string pictureSearch = firstHalfPic + pictureSearchTerms + secondHalfPic;
            string htmlText = htmlHelper.getHtml(pictureSearch);
            List<string> listOfUrls = GetPicFromUrl(htmlText);
            if (listOfUrls.Count == 0 || listOfUrls[0] == null)
            {
                return (null);
            }
            WebClient downloader = new WebClient();
            Stream dataStream = downloader.OpenRead(listOfUrls[0]);
            //downloader.DownloadFile(listOfUrls[0], "temp.jpg");
            /*
            byte[] data = downloader.DownloadData(listOfUrls[0]);
            using (MemoryStream mem = new MemoryStream(data))
            {
                return (Image.FromStream(mem));
            }
            */
            return (Image.FromStream(dataStream));

        }

        private static List<string> getUrlsGoogleUrl(string html)
        {
            var urls = new List<string>();
            int ndx = html.IndexOf("url?q=", 0, StringComparison.Ordinal);

            while (ndx >= 0)
            {
                ndx = ndx + 6;
                int ndx2 = html.IndexOf("&", ndx, StringComparison.Ordinal);
                string url = html.Substring(ndx, ndx2 - ndx);
                urls.Add(url);
                ndx = html.IndexOf("url?q=", ndx, StringComparison.Ordinal);
            }
            return urls;
        }

        private static List<string> GetPicFromUrl(string html)
        {
            File.WriteAllText("imageStuff.txt", html);
            var urls = new List<string>();
            try
            {
                int ndx = html.IndexOf("class=\"images_table\"", StringComparison.Ordinal);
                ndx = html.IndexOf("<img", ndx, StringComparison.Ordinal);

                while (ndx >= 0)
                {
                    ndx = html.IndexOf("src=\"", ndx, StringComparison.Ordinal);
                    ndx = ndx + 5;
                    int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
                    string url = html.Substring(ndx, ndx2 - ndx);
                    urls.Add(url);
                    ndx = html.IndexOf("<img", ndx, StringComparison.Ordinal);
                }
            }
            catch (ArgumentException e)
            {
                urls = new List<string>();
            }
            return urls;
        }
    }
}
