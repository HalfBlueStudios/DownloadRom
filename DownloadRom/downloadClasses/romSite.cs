using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadRom
{
    class romSite
    {
        public string siteName;
        public int numRoms;

        public romSite(string newName, int newNum)
        {
            siteName = newName;
            numRoms = newNum;
        }

        public override string ToString()
        {
            return (siteName + " (" + numRoms + ")");
        }

        public void increaseNumRoms()
        {
            numRoms++;
        }
    }
}
