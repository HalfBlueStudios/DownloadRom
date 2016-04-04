using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadRom
{
    class pageOfRoms
    {
        public List<playableRom> pageRoms;
        int pageNum;
        romPlayer playerSender;
        Button pageButton = new Button();
        System.Windows.Forms.Control.ControlCollection controlToUse;

        public pageOfRoms(int newPageNum, romPlayer newSender)
        {
            pageRoms = new List<playableRom>();
            pageNum = newPageNum;
            playerSender = newSender;
            controlToUse = playerSender.Controls;
            pageButton.Click += new EventHandler(pageButton_Click);
            pageButton.Text = "Page " + newPageNum;
            pageButton.Size = new Size(formSizes.pageButtonLength, formSizes.pageButtonHeight);
            pageButton.Location = new Point(formSizes.pageButtonX + (formSizes.betweenButtonX * pageNum), 
                                            formSizes.pageButtonY);
            controlToUse.Add(pageButton);
        }

        public void addPlayableRom(playableRom newRom)
        {
            pageRoms.Add(newRom);
        }

        public void hidePage()
        {
            foreach (playableRom rom in pageRoms)
            {
                rom.hideRom(controlToUse);
            }
        }

        public void showPage()
        {
            //controlToUse = playerSender.Controls;
            int xPos = formSizes.pageStartX;
            int yPos = formSizes.pageStartY;
            int lengthOfPage = formSizes.lengthOfPage;
            foreach(playableRom rom in pageRoms)
            {
                rom.showRom(new Point(xPos, yPos), controlToUse);
                xPos = xPos + formSizes.pageBetweenX;
                lengthOfPage--;
                if(lengthOfPage == 0)
                {
                    xPos = formSizes.pageStartX;
                    yPos = yPos + formSizes.pageBetweenY;
                    lengthOfPage = formSizes.lengthOfPage;
                }
            }
        }

        private void pageButton_Click(object sender, EventArgs e)
        {
            playerSender.switchPage(pageNum);
        }

        public void dispose()
        {
            pageButton.Dispose();
            foreach(playableRom rom in pageRoms)
            {
                rom.dispose();
            }
        }

    }
}
