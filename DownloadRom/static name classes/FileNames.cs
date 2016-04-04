using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadRom
{
    class FileNames
    {
        public static string gamePathName = "GamesPath.txt";
        public static string fullGameDatabaseName = tableNames.gameDatabaseName + ".mdf";
        public static string gameDatabaseLogName = tableNames.gameDatabaseName + "_log";
        public static string fullGameDatabseLogName = gameDatabaseLogName + ".ldf";
    }
}
