using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadRom
{

    [Serializable()]
    public class romList
    {
        public string systemName;
        public List<downloadableRom> listOfRoms;

        public romList(string newSystemName)
        {
            listOfRoms = new List<downloadableRom>();
            systemName = newSystemName;
        }

        public romList(romList romListToCopy)
        {
            systemName = romListToCopy.systemName;
            listOfRoms = new List<downloadableRom>();
            /*
            for(int i = 0; i < romListToCopy.listOfRoms.Count; i++)
            {
                romListToCopy.listOfRoms[i].GetType() newRom;

            }
            */
            foreach(var romToCopy in romListToCopy.listOfRoms)
            {
                var newRom = romToCopy.getCopyOfRom();
                listOfRoms.Add(newRom);
            }
        }

        public override string ToString()
        {
            return (systemName + "(" + listOfRoms.Count + ")");
        }
    }

    [Serializable()]
    public class downloadableRom : IEquatable<downloadableRom>
    {
        public string systemName;
        public string gameName;
        public string urlToDownload;
        public string extractionType;
        public string fileName;
        public string pictureLocation;
        public string genre1;
        public string genre2;
        public string genre3;
        public string language;
        public string siteFrom;
        public bool favorited = false;

        public downloadableRom(string newSystem, string newGame, string newUrlToDownload, string newSite)
        {
            systemName = convertNameHelper.convertName(newSystem);
            gameName = newGame;
            urlToDownload = newUrlToDownload;
            siteFrom = newSite;
            if (newGame.Contains("(Japan)") || newGame.Contains("Japanese"))
            {
                language = "Japanese";
            }
            else if (newGame.Contains("(France)") || newGame.Contains("(French)"))
            {
                language = "French";
            }
            else if (newGame.Contains("(Italy)") || newGame.Contains("(Italian)"))
            {
                language = "Itilian";
            }
            else if(newGame.Contains("(German)") || newGame.Contains("(Germany)"))
            {
                language = "German";
            }
            else if(newGame.Contains("(Spanish)") || newGame.Contains("(Spain)") || newGame.Contains("(Mexico)"))
            {
                language = "Spanish";
            }
            else
            {
                language = "English";
            }
        }

        public override bool Equals(object obj)
        {
            downloadableRom checkRom = obj as downloadableRom;
            if (checkRom == null)
            {
                return (false);
            }
            else
            {
                if (this.urlToDownload == checkRom.urlToDownload)
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
        }

        public void setExtractionType(string newExtraction)
        {
            extractionType = newExtraction;
        }

        public void setGenre1(string newGenre)
        {
            genre1 = newGenre;
        }

        public void setGenre2(string newGenre)
        {
            genre2 = newGenre;
        }

        public override string ToString()
        {
            return (systemName + " " + gameName);
        }

        public void setFile(string newFile)
        {
            fileName = newFile;
        }

        public void setPicture(string newPic)
        {
            pictureLocation = newPic;
        }

        public virtual string getDownloadLink()
        {
            return (null);
        }

        internal void setGenre3(string genreToAdd)
        {
            genre3 = genreToAdd;
        }

        public bool Equals(downloadableRom other)
        {
            if (other == null)
            {
                return (false);
            }
            if (databaseHelper.sanatizeString(other.urlToDownload) == databaseHelper.sanatizeString(this.urlToDownload))
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public virtual downloadableRom getCopyOfRom()
        {
            return (null);
        }

    }

    [Serializable()]
    public class coolRom : downloadableRom
    {
        public coolRom(string newSystem, string newGame, string newUrl, string newSite) : base(newSystem, newGame, newUrl, newSite)
        {

        }

        public coolRom(coolRom romToCopy) : 
            base(romToCopy.systemName,romToCopy.gameName,romToCopy.urlToDownload,romToCopy.siteFrom)
        {
            extractionType = romToCopy.extractionType;
            fileName = romToCopy.fileName;
            genre1 = romToCopy.genre1;
            genre2 = romToCopy.genre2;
            genre3 = romToCopy.genre3;
            language = romToCopy.language;
            siteFrom = romToCopy.siteFrom;
            favorited = romToCopy.favorited;
        }

        public override string getDownloadLink()
        {
            return (CoolRomQuery.getCoolRomDownloadLink(urlToDownload, this));
        }

        
        public override downloadableRom getCopyOfRom()
        {
            return(new coolRom(this)); 
        }
        
    }

    [Serializable()]
    public class EmuparadiseRom : downloadableRom
    {
        public EmuparadiseRom(string newSystem, string newGame, string newUrl, string newSite) : base(newSystem, newGame, newUrl, newSite)
        {

        }

        public EmuparadiseRom(EmuparadiseRom romToCopy) : 
            base(romToCopy.systemName,romToCopy.gameName,romToCopy.urlToDownload,romToCopy.siteFrom)
        {
            extractionType = romToCopy.extractionType;
            fileName = romToCopy.fileName;
            genre1 = romToCopy.genre1;
            genre2 = romToCopy.genre2;
            genre3 = romToCopy.genre3;
            language = romToCopy.language;
            siteFrom = romToCopy.siteFrom;
            favorited = romToCopy.favorited;
        }

        public override string getDownloadLink()
        {
            return (EmuparadiseQuery.getEmuparadiseDownloadLink(urlToDownload, this));
        }

        public override downloadableRom getCopyOfRom()
        {
            return (new EmuparadiseRom(this));
        }
    }
}
