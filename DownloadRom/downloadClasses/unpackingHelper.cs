using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadRom
{
    public class unpackingHelper
    {
        public static void unpackRom(downloadableRom romToUnpack, string rootFolder)
        {
            string unpackFolder = rootFolder + "\\" + FolderNames.gamesfolderPath + "\\" + romToUnpack.systemName;
            if (Directory.Exists(unpackFolder) == false)
            {

                DirectoryInfo info = Directory.CreateDirectory(unpackFolder);
                Console.WriteLine(info.CreationTime);
            }
            string zippedFile = FolderNames.tempDownloads + "\\" + romToUnpack.fileName;
            if (zippedFile.Contains(".zip"))
            {
                unpackZip(zippedFile);
            }
            else if (zippedFile.Contains(".7z"))
            {
                unpackZip(zippedFile);
            }
            extractRomFile(unpackFolder, romToUnpack);
            deleteTemps();
            savePreviewImage(romToUnpack, rootFolder);
        }

        private static void extractRomFile(string unpackLocation, downloadableRom romToUnpack)
        {
            string[] allCandidates = Directory.GetFiles(FolderNames.tempExtractionFolderPath);
            if (allCandidates.Length == 1)
            {
                moveFile(allCandidates[0], unpackLocation, romToUnpack);
            }
            else
            {
                int maxMatched = 0;
                string matchedCandidate = null;
                string[] splitName = romToUnpack.gameName.Split(' ');
                foreach (string candidate in allCandidates)
                {
                    int numMatched = 0;
                    string[] splitCandidate = candidate.Split(' ');
                    foreach (string partToCompare in splitName)
                    {
                        foreach (string word in splitCandidate)
                        {
                            if (word.Contains(partToCompare))
                            {
                                numMatched++;
                            }
                        }
                    }
                    if (numMatched > maxMatched)
                    {
                        maxMatched = numMatched;
                        matchedCandidate = candidate;
                    }
                }
                moveFile(matchedCandidate, unpackLocation, romToUnpack);
            }
        }

        private static void moveFile(string originalFile, string destinationFolder, downloadableRom romToUnpack)
        {
            string[] options = originalFile.Split('.');
            string extractionType = "." +  options[options.Length - 1];
            string destination = destinationFolder + "\\" + romToUnpack.gameName + extractionType;
            File.Move(originalFile, destination);
            romToUnpack.fileName = destination; ;
        }


        private static void savePreviewImage(downloadableRom romToSave, string rootFolder)
        {
            string unpackImageFolder = rootFolder + "\\" + FolderNames.gameRootFolder + "\\" + FolderNames.gameImageFolder + "\\" + romToSave.systemName;
            if (Directory.Exists(unpackImageFolder) == false)
            {

                DirectoryInfo info = Directory.CreateDirectory(unpackImageFolder);
                Console.WriteLine(info.CreationTime);
            }
            Image imageToSave = googleImageQuery.getGooglePic(romToSave.systemName + " " + romToSave.gameName, true);
            if (imageToSave != null)
            {
                Bitmap bitImage = new Bitmap(imageToSave);
                string saveSpot = unpackImageFolder + "\\" + romToSave.gameName + ".jpg";
                bitImage.Save(saveSpot, ImageFormat.Jpeg);
                romToSave.setPicture(saveSpot);
            }
        }

        private static void unpackZip(string zippedFile)
        {
            try
            {
                /*
                ZipArchive archive = ZipFile.Open(zippedFile, ZipArchiveMode.Read);
                foreach (ZipArchiveEntry partArchive in archive.Entries)
                {
                    if (partArchive.FullName.Contains(romToUnpack.gameName))
                    {
                        partArchive.ExtractToFile(unpackFolder);
                    }
                }
                */
                ZipFile.ExtractToDirectory(zippedFile, FolderNames.tempExtractionFolderPath);
            }
            catch (InvalidDataException e)
            {
                string sevenZipFile = zippedFile.Replace(".zip", ".7z");
                File.Move(zippedFile, sevenZipFile);
                unpack7zip(sevenZipFile);
            }
        }

        private static void unpack7zip(string zippedFile)
        {
            string zPath = @"C:\Program Files\7-Zip\7zG.exe";// change the path and give yours 
            try
            {
                ProcessStartInfo pro = new ProcessStartInfo();
                //pro.WindowStyle = ProcessWindowStyle.Hidden;
                pro.FileName = zPath;
                pro.Arguments = "x \"" + zippedFile + "\" -o\"" + FolderNames.tempExtractionFolderPath;
                Process x = Process.Start(pro);
                x.WaitForExit();
            }
            catch (Exception e)
            {
                e.ToString();

            }
        }

        private static void deleteTemps()
        {
            foreach (string temp in Directory.GetDirectories(FolderNames.tempExtractionFolderPath))
            {
                foreach (string tempFile in Directory.GetFiles(temp))
                {
                    File.Delete(tempFile);
                }
                Directory.Delete(temp);
            }
            foreach (string temp in Directory.GetFiles(FolderNames.tempExtractionFolderPath))
            {
                File.Delete(temp);
            }
            foreach (string temp in Directory.GetFiles(FolderNames.tempDownloads))
            {
                File.Delete(temp);
            }
        }
    }
}
