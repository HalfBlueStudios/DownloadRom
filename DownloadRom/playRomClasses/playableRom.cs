using DownloadRom.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadRom
{
    public class playableRom
    {
        public string systemName;
        public string gameName;
        public bool favorited;
        public string fileName;
        public string imageLocation;
        public string url;
        public int placeInList;
        public Label gameText = new Label();
        public PictureBox gamePic = new PictureBox();
        romPlayer superSender;
        gameSelectionForm otherSender;
        

        public playableRom(string newSystemName, string newGameName, bool newFavorited, string newFileName,
                           string newImage, string newUrl)
        {
            url = newUrl;
            systemName = newSystemName;
            gameName = newGameName;
            favorited = newFavorited;
            fileName = newFileName;
            imageLocation = newImage;
            gameText.Text = textHelper.removeExtraParts(gameName);
            gameText.MaximumSize = new Size(100, 0);
            gameText.AutoSize = true;
            if(newImage != null)
            {
                Size picSize = new Size(formSizes.gamePictureWidth, formSizes.gamePictureHeight);
                gamePic = new PictureBox();
                gamePic.Click += new EventHandler(setAsSelected);
                Image org = new Bitmap(newImage);
                gamePic.Image = new Bitmap(org, picSize);
                gamePic.Size = picSize;
            }
        }

        public void addSender(romPlayer newSender)
        {
            superSender = newSender;
        }

        public void addPlaceInList(int newNum)
        {
            placeInList = newNum;
        }

        public void showRom(Point newPoint, System.Windows.Forms.Control.ControlCollection controlToAdd)
        {
            gamePic.Location = newPoint;
            gameText.Location = new Point(newPoint.X, newPoint.Y - formSizes.pictureTextSpaceBetweenY);
            gamePic.Name = gameName + " PIC";
            gameText.Name = gameName + " TEXT";
            gamePic.Visible = true;
            gameText.Visible = true;
            controlToAdd.Add(gamePic);
            controlToAdd.Add(gameText);
            gamePic.BringToFront();
            gameText.BringToFront();
        }

        public void hideRom(System.Windows.Forms.Control.ControlCollection controlToRemove)
        {
            gamePic.Visible = false;
            gameText.Visible = false;
            controlToRemove.Remove(gamePic);
            controlToRemove.Remove(gameText);
        }

        public void hideText(System.Windows.Forms.Control.ControlCollection controlToRemove)
        {
            gameText.Visible = false;
            controlToRemove.Remove(gameText);
        }

        public void hidePic(System.Windows.Forms.Control.ControlCollection controlToRemove)
        {
            gamePic.Visible = false;
            controlToRemove.Remove(gamePic);
        }

        public void showText(Point pointToShow, System.Windows.Forms.Control.ControlCollection controlToAdd)
        {
            gameText.Visible = true;
            gameText.Location = pointToShow;
            controlToAdd.Add(gameText);
        }

        public void showPicture(Point pointToShow, System.Windows.Forms.Control.ControlCollection controlToAdd)
        {
            gamePic.Visible = true;
            gamePic.Location = pointToShow;
            controlToAdd.Add(gamePic);
        }

        public void moveRom(Point newPoint)
        {
            gamePic.Location = newPoint;
            gameText.Location = new Point(newPoint.X, newPoint.Y - formSizes.pictureTextSpaceBetweenY);
        }

        public Point getCurrentLocation()
        {
            return (gamePic.Location);
        }

        public void dispose()
        {
            gamePic.Dispose();
            gameText.Dispose();
        }

        public void addGameSelectorSender(gameSelectionForm newSender)
        {
            otherSender = newSender;
        }

        public void setAsSelected(object sender, EventArgs e)
        {
            if (superSender != null)
            {
                superSender.setSelectedGame(this);
            }
            else
            {
                otherSender.setSelectedGame(this);
            }
        }

    }
}
