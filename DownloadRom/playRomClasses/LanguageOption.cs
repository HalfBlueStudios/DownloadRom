using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadRom
{
    class LanguageOption
    {
        public string languageName;
        public int gameCount;
        
        public LanguageOption(string newName, int initialCount)
        {
            languageName = newName;
            gameCount = initialCount;
        }

        public void addToGameCount()
        {
            gameCount++;
        }

        public void subtractFromGameCount()
        {
            if (gameCount - 1 >= 0)
            {
                gameCount--;
            }
        }

        public override string ToString()
        {
            return (languageName + " (" + gameCount + ")");
        }
    }
}
