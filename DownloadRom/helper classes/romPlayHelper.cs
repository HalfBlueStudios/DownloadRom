using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadRom.helper_classes
{
    static class romPlayHelper
    {


        public static void playRom(playableRom selectedRom,Form Newsender )
        {
            if (selectedRom == null)
            {
                return;
            }
            databaseHelper.updateRecentlyPlayed(selectedRom);
            string fileType = getFileType(selectedRom.fileName);
            string pathForFile = FolderNames.emulationPathsFolderPath + "\\" + selectedRom.systemName + ".txt";
            if (File.Exists(pathForFile))
            {
                string[] executions = File.ReadAllLines(pathForFile);
                openRom(executions[0], executions[1], selectedRom, Newsender);
            }
            else
            {
                setUpNewEmulator(fileType, selectedRom, pathForFile, Newsender);
            }
        }

        private static void openRom(string pathOfExecution, string commandLineToUse, playableRom romToOpen, Form sender)
        {
            System.Diagnostics.ProcessStartInfo newInfo = new System.Diagnostics.ProcessStartInfo(pathOfExecution, commandLineToUse + " " + "\"" + romToOpen.fileName + "\"");
            newInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
            System.Diagnostics.Process newPros = System.Diagnostics.Process.Start(newInfo); //System.Diagnostics.Process.Start(pathOfExecution,commandLineToUse + " " + "\"" + romToOpen.fileName + "\"");
            sender.Hide();
            newPros.WaitForExit();
            sender.Show();
        }

        private static void setUpNewEmulator(string fileType, playableRom romToOpen, string pathForText, Form sender)
        {
            setUpEmulatorForm newForm = new setUpEmulatorForm(fileType, romToOpen, sender);
            newForm.ShowDialog();
            if (File.Exists(pathForText))
            {
                string[] executions = File.ReadAllLines(pathForText);
                string newCommandLine = findCommandLineOpen(executions[0], romToOpen);
                List<string> stringToWrite = new List<string>();
                stringToWrite.Add(newCommandLine);
                File.AppendAllLines(pathForText, stringToWrite);
            }
        }

        private static string findCommandLineOpen(string pathOfExecution, playableRom romToOpen)
        {
            romStartOkayForm checkForm;
            foreach (String candidateOpen in commandLineHelper.openLines)
            {
                System.Diagnostics.Process newPros = System.Diagnostics.Process.Start(pathOfExecution, candidateOpen + " " + "\"" + romToOpen.fileName + "\"");
                checkForm = new romStartOkayForm(candidateOpen);
                var check = checkForm.ShowDialog();
                if (checkForm.startCorrectly == true)
                {
                    return (candidateOpen);
                }
                else
                {
                    if (newPros.HasExited == false)
                    {
                        newPros.Kill();
                        newPros.Close();
                    }
                    if (checkForm.commandLineToUse != null)
                    {
                        System.Diagnostics.Process userGiven = System.Diagnostics.Process.Start(pathOfExecution, checkForm.commandLineToUse + " " + "\"" + romToOpen.fileName + "\"");
                        romStartOkayForm userGivenForm = new romStartOkayForm(checkForm.commandLineToUse);
                        var specialCheck = userGivenForm.ShowDialog();
                        if (userGivenForm.startCorrectly == true)
                        {
                            return (checkForm.commandLineToUse);
                        }
                        else
                        {
                            if (userGiven.HasExited == false)
                            {
                                userGiven.Kill();
                                userGiven.Close();
                            }
                        }
                    }
                }
            }
            return (null);
        }

        private static string getFileType(string stringToParse)
        {
            int startPoint = stringToParse.IndexOf('.') + 1;
            string retString = stringToParse.Substring(startPoint);
            return (retString);
        }
    }
}
