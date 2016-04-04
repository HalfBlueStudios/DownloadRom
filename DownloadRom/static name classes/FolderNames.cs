using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadRom
{
    class FolderNames
    {
        public static string topFolderName = "Rom Downloader Data";
        public static string gameRootFolder = "Rom Downloader Downloaded Games";
        public static string gameImageFolder = "Preview Images";
        public static string downloadedGamesFolder = "Downloaded Games";
        public static string configFolder = "Config";
        public static string databaseFolder = "Database";
        public static string tempDownloads = "Temp Downloads";
        public static string tempExtraction = "Temp Extraction";
        public static string emulatorPaths = "Emulator Paths";
        public static string defaultLocation = Environment.CurrentDirectory;
        public static string databaseFolderPath = FolderNames.defaultLocation + "\\" + FolderNames.topFolderName + "\\" + FolderNames.databaseFolder;
        public static string configFolderPath = FolderNames.defaultLocation + "\\" + FolderNames.topFolderName + "\\" + configFolder;
        public static string gamesfolderPath = FolderNames.gameRootFolder + "\\" + FolderNames.downloadedGamesFolder;
        public static string tempExtractionFolderPath = FolderNames.tempDownloads + "\\" + FolderNames.tempExtraction;
        public static string emulationPathsFolderPath = configFolderPath + "\\" + emulatorPaths; 
    }
}
