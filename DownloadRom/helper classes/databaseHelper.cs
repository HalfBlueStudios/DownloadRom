using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadRom
{
    public class databaseHelper
    {
        public static string instanceName = "MSSQLLocalDB"; //"RomRewindDB";
        private static string dbConnection = @"Data Source=(LocalDB)\" + instanceName + ";AttachDbFilename=" + FolderNames.databaseFolderPath + "\\" + FileNames.fullGameDatabaseName + ";Integrated Security=True";
        private static string initialConnection = @"Data Source=(LocalDB)\" + instanceName + ";" + "Integrated Security=True";
        private static int maxRecent = 10;

        public static void createGameDatabase()
        {
            //dropAll(initialConnection);
            SqlConnection startConnection = new SqlConnection(initialConnection);
            try
            {
                startConnection.Open();
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("timed out!");
                createGameDatabase();
                return;
            }
            //try
            //{
                string databaseFolder = FolderNames.defaultLocation + "\\" + FolderNames.topFolderName + "\\" + FolderNames.databaseFolder;
                string createDatabase = "CREATE DATABASE " + tableNames.gameDatabaseName + " ON PRIMARY " +
                    "(NAME = '" + tableNames.gameDatabaseName + "'," +
                    "FILENAME = '" + FolderNames.databaseFolderPath + "\\" + FileNames.fullGameDatabaseName + "', " +
                    "SIZE = 5MB, MAXSIZE = 1000MB, FILEGROWTH = 10%)" +
                    "LOG ON (NAME = '" + FileNames.gameDatabaseLogName + "'," +
                    "FILENAME = '" + FolderNames.databaseFolderPath + "\\" + FileNames.fullGameDatabseLogName + "', " +
                    "SIZE = 1MB, " +
                    "MAXSIZE = 50MB, " +
                    "FILEGROWTH = 10%)";
                SqlCommand createDatabseCommand = new SqlCommand(createDatabase, startConnection);
                int numRows = createDatabseCommand.ExecuteNonQuery();
                startConnection.Close();
                setupGameDatabase();
                /*
            }
            catch(Exception e) //game database already exists
            {
                MessageBox.Show("erroring out here");
            }
            */
        }

        public static void rebuildDatabase()
        {
            dropAll(initialConnection);
            createGameDatabase();
        }

        private static void dropAll(string connectionToUse)
        {
            try
            {
                SqlConnection connection = new SqlConnection(initialConnection);
                connection.Open();
                //SqlCommand getReadyForDrop = new SqlCommand("USE MASTER alter database " + tableNames.gameDatabaseName  + " set single_user with rollback immediate",connection);
                //getReadyForDrop.ExecuteNonQuery();
                SqlCommand dropGameCommand = new SqlCommand("DROP DATABASE " + tableNames.gameDatabaseName, connection);
                dropGameCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException e)
            {
                //MessageBox.Show(e.Message);
            }
            /*
            SqlCommand dropRecentlyDownloaded = new SqlCommand("DROP DATABASE" + FileNames.recentlyDownloadedDatabaseName, connection);
            dropRecentlyDownloaded.ExecuteNonQuery();
            SqlCommand dropRecentlyPlayed = new SqlCommand("DROP DATABASE " + FileNames.recentlyPlayedDatabaseName, connection);
            dropRecentlyPlayed.ExecuteNonQuery();
            connection.Close();
            */
        }

        private static void setupGameDatabase()
        {
            SqlConnection connection = new SqlConnection(dbConnection);
            connection.Open();
            try
            {
                using (SqlCommand command =
                    new SqlCommand(
                    "Create TABLE " + tableNames.romTableName + "(", connection))
                {
                    command.CommandText +=
                    "SYSTEM_NAME VARCHAR(100) NOT NULL," +
                    "GAME_NAME varchar(300) NOT NULL," +
                    "LANGUAGE varchar(50) NOT NULL," +
                    "GENRE_1 varchar(50)," +
                    "GENRE_2 varchar(50)," +
                    "GENRE_3 varchar(50)," +
                    "URL varchar(200) NOT NULL," +
                    "FAVORITED bit NOT NULL," +
                    "SITE varchar(50) NOT NULL," +
                    "FILE_NAME varchar(200) NOT NULL," +
                    "PICTURE_NAME varchar(200) NOT NULL," +
                    "primary key(URL));";
                    command.ExecuteNonQuery();
                }
                using (SqlCommand downloadableCommand =
                   new SqlCommand(
                    "Create TABLE " + tableNames.recentlyDownloadedTableName + "(", connection))
                {
                    downloadableCommand.CommandText +=
                    "URL varchar(200) NOT NULL," +
                    "DOWNLOAD_NUM INT NOT NULL," +
                    "FOREIGN key(URL) REFERENCES " + tableNames.romTableName + "(URL)" +
                    "ON UPDATE CASCADE " +
                    "ON DELETE CASCADE);"; 
                    downloadableCommand.ExecuteNonQuery();
                }
                using (SqlCommand downloadableCommand =
                   new SqlCommand(
                   "Create TABLE " + tableNames.recentlyPlayedTableName + "(", connection))
                {
                    downloadableCommand.CommandText +=
                    "URL varchar(200) NOT NULL," +
                    "DOWNLOAD_NUM INT NOT NULL," +
                    "FOREIGN key(URL) REFERENCES " + tableNames.romTableName + "(URL)" +
                    "ON UPDATE CASCADE " +
                    "ON DELETE CASCADE);"; 
                    downloadableCommand.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (SqlException e) //table already in database
            {
                MessageBox.Show(e.Message);
            }
            connection.Close();
        }

        public static void insertRom(downloadableRom romToInsert)
        {
            try
            {
                romToInsert = sanatizeRom(romToInsert);
                SqlConnection connection = new SqlConnection(dbConnection);
                connection.Open();
                using (SqlCommand command =
                    new SqlCommand(
                    "insert into " + tableNames.romTableName + " values(", connection))
                {
                    command.CommandText += "'" + romToInsert.systemName + "'" + ",";
                    command.CommandText += "'" + romToInsert.gameName + "'" + ",";
                    command.CommandText += "'" + romToInsert.language + "'" + ",";
                    command.CommandText += "'" + romToInsert.genre1 + "'" + ",";
                    command.CommandText += "'" + romToInsert.genre2 + "'" + ",";
                    command.CommandText += "'" + romToInsert.genre3 + "'" + ",";
                    command.CommandText += "'" + romToInsert.urlToDownload + "'" + ",";
                    command.CommandText += "0" + ",";
                    command.CommandText += "'" + romToInsert.siteFrom + "'" + ",";
                    command.CommandText += "'" + romToInsert.fileName + "'" + ",";
                    command.CommandText += "'" + romToInsert.pictureLocation + "'";
                    command.CommandText += ");";
                    command.CommandText = command.CommandText;
                    command.ExecuteNonQuery();
                }
                SqlCommand upNum = new SqlCommand("", connection);
                upNum.CommandText = "UPDATE " + tableNames.recentlyDownloadedTableName + " SET DOWNLOAD_NUM = DOWNLOAD_NUM + 1;";
                upNum.ExecuteNonQuery();
                SqlCommand deleteLast = new SqlCommand("" + connection);
                upNum.CommandText = "DELETE FROM " + tableNames.recentlyDownloadedTableName + " WHERE DOWNLOAD_NUM >= " + maxRecent + ";";
                upNum.ExecuteNonQuery();
                SqlCommand insertIntoDownloaded =
                new SqlCommand("insert into " + tableNames.recentlyDownloadedTableName + " values(", connection);
                insertIntoDownloaded.CommandText += "'" + romToInsert.urlToDownload + "'" + "," +
                        "0" + ");";
                insertIntoDownloaded.ExecuteNonQuery();
                connection.Close();

            }
            catch (InvalidAsynchronousStateException e) //table already in database
            {
                MessageBox.Show(e.Message);
            }
        }
        
        public static void removeRom(playableRom romToRemove)
        {
            SqlConnection connection = new SqlConnection(dbConnection);
            connection.Open();
            string dropCommand = "DELETE FROM " + tableNames.romTableName;
            dropCommand += " WHERE URL='" + romToRemove.url + "';";
            SqlCommand dropEntry = new SqlCommand(dropCommand, connection);
            dropEntry.ExecuteNonQuery();
            connection.Close();
        }

        public static void updateRecentlyPlayed(playableRom romToInsert)
        {
            List<playableRom> currentRecentlyPlayed = getRecently(tableNames.recentlyPlayedTableName);
            bool inserted = false;
            foreach(playableRom rom in currentRecentlyPlayed)
            {
                if(rom.url == romToInsert.url)
                {
                    updateRecentlyPlayedRom(rom.url, rom.placeInList);
                    inserted = true;
                    break;
                }
            }
            if(inserted == false)
            {
                insertRecentlyPlayedRom(romToInsert);
            }
        }

        public static void updateRecentlyPlayedRom(string urlToUse, int numToUpdate)
        {
            SqlConnection connection = new SqlConnection(dbConnection);
            connection.Open();
            SqlCommand updateHigher = new SqlCommand("", connection);
            updateHigher.CommandText = "UPDATE " + tableNames.recentlyPlayedTableName + 
                                " SET DOWNLOAD_NUM = DOWNLOAD_NUM + 1 WHERE DOWNLOAD_NUM < " + numToUpdate + ";";

            updateHigher.ExecuteNonQuery();
            SqlCommand updateChosen = new SqlCommand("", connection);
            updateChosen.CommandText = "UPDATE " + tableNames.recentlyPlayedTableName +
                                " SET DOWNLOAD_NUM = 0 WHERE URL = " + "'" + urlToUse + "'" + ";";

            updateChosen.ExecuteNonQuery();
            connection.Close();
        }

        public static void insertRecentlyPlayedRom(playableRom romToInsert)
        {
            SqlConnection connection = new SqlConnection(dbConnection);
            connection.Open();
            SqlCommand upNum = new SqlCommand("", connection);
            upNum.CommandText = "UPDATE " + tableNames.recentlyPlayedTableName + " SET DOWNLOAD_NUM = DOWNLOAD_NUM + 1;";
            upNum.ExecuteNonQuery();
            SqlCommand deleteLast = new SqlCommand("" + connection);
            upNum.CommandText = "DELETE FROM " + tableNames.recentlyPlayedTableName + " WHERE DOWNLOAD_NUM >= " + maxRecent + ";";
            upNum.ExecuteNonQuery();
            SqlCommand insertIntoDownloaded =
                new SqlCommand("insert into " + tableNames.recentlyPlayedTableName + " values(", connection);
            insertIntoDownloaded.CommandText += "'" + romToInsert.url + "'" + "," +
                    "0" + ");";
            insertIntoDownloaded.ExecuteNonQuery();
            connection.Close();
        }

        public static List<playableRom> getRecently(string tableToQuery)
        {
            SqlConnection connection = new SqlConnection(dbConnection);
            connection.Open();
            SqlCommand query = new SqlCommand("Select * from " + tableToQuery + " ", connection);
            SqlDataReader reader = query.ExecuteReader();
            List<playableRom> returnList = new List<playableRom>();
            List<int> countList = new List<int>();
            List<SqlCommand> romsToFind = new List<SqlCommand>();
            while (reader.Read())
            {
                string url = reader.GetString(0);
                int num = reader.GetInt32(1);
                countList.Add(num);
                SqlCommand findRom = new SqlCommand("Select * from " + tableNames.romTableName + " " +
                                                     "where URL = '" + url + "';", connection);
                romsToFind.Add(findRom);
            }
            connection.Close();
            for (int i = 0; i < romsToFind.Count; i++)
            {
                connection.Open();
                SqlDataReader romFindRead = romsToFind[i].ExecuteReader();
                if (romFindRead.Read() == true)
                {
                    playableRom newReturnedRom = playableRomFromReader(romFindRead);
                    newReturnedRom.addPlaceInList(countList[i]);
                    returnList.Add(newReturnedRom);
                }
                connection.Close();
            }
            returnList = returnList.OrderBy(x => x.placeInList).ToList<playableRom>();
            return (returnList);
        }

        private static downloadableRom romFromReader(SqlDataReader reader)
        {
            string systemName = reader.GetString(0);
            string gameName = reader.GetString(1);
            string language = reader.GetString(2);
            string genre1 = reader.GetString(3);
            string genre2 = reader.GetString(4);
            string genre3 = reader.GetString(5);
            string url = reader.GetString(6);
            bool favorited = reader.GetBoolean(7);
            string site = reader.GetString(8);
            downloadableRom newReturnedRom = new downloadableRom(systemName, gameName, url, site);
            return (newReturnedRom);
        }
        
        private static playableRom playableRomFromReader(SqlDataReader reader)
        {
            string systemName = reader.GetString(0);
            string gameName = reader.GetString(1);
            string language = reader.GetString(2);
            string genre1 = reader.GetString(3);
            string genre2 = reader.GetString(4);
            string genre3 = reader.GetString(5);
            string url = reader.GetString(6);
            bool favorited = reader.GetBoolean(7);
            string site = reader.GetString(8);
            string fileName = reader.GetString(9);
            string pictureName = reader.GetString(10);
            playableRom newPlayableRom = new playableRom(systemName, gameName, favorited, fileName, pictureName,url);
            return (newPlayableRom);
        }

        public static List<romList> queryRoms(string searchParamaters)
        {
            SqlConnection connection = new SqlConnection(dbConnection);
            connection.Open();
            SqlCommand query = new SqlCommand("Select * from " + tableNames.romTableName + " " + searchParamaters, connection);
            SqlDataReader reader = query.ExecuteReader();
            List<romList> returnList = new List<romList>();
            while (reader.Read())
            {
                downloadableRom newReturnedRom = romFromReader(reader);
                bool added = false;
                foreach (romList listToCheck in returnList)
                {
                    if (listToCheck.systemName == newReturnedRom.systemName)
                    {
                        listToCheck.listOfRoms.Add(newReturnedRom);
                        added = true;
                    }
                }
                if (added == false)
                {
                    romList newRomList = new romList(newReturnedRom.systemName);
                    newRomList.listOfRoms.Add(newReturnedRom);
                    returnList.Add(newRomList);
                }
            }
            connection.Close();
            return (returnList);
        }

        public static List<playableRom> queryPlayableRom(string searchParamaters)
        {
            SqlConnection connection = new SqlConnection(dbConnection);
            connection.Open();
            SqlCommand query = new SqlCommand("Select * from " + tableNames.romTableName + " " + searchParamaters, connection);
            SqlDataReader reader = query.ExecuteReader();
            List<playableRom> returnList = new List<playableRom>();
            while (reader.Read())
            {
                playableRom newReturnedRom = playableRomFromReader(reader);
                returnList.Add(newReturnedRom);
            }
            connection.Close();
            return (returnList);
        }

        public static downloadableRom sanatizeRom(downloadableRom romToSanatize)
        {
            romToSanatize.gameName = sanatizeString(romToSanatize.gameName);
            romToSanatize.urlToDownload = sanatizeString(romToSanatize.urlToDownload);
            romToSanatize.genre1 = sanatizeString(romToSanatize.genre1);
            romToSanatize.genre2 = sanatizeString(romToSanatize.genre2);
            romToSanatize.genre3 = sanatizeString(romToSanatize.genre3);
            return (romToSanatize);
        }

        public static string sanatizeString(string stringToSanatize)
        {
            if (stringToSanatize == null)
            {
                return (null);
            }
            string retString = stringToSanatize.Replace("'", "_");
            retString = retString.Replace(",", "_");
            //retString = retString.Replace("")
            return (retString);
        }
    }
}
