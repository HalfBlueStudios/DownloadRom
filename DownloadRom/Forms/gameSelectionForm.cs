using DownloadRom.helper_classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadRom.Forms
{
    public partial class gameSelectionForm : Form
    {
        List<playableRom> listOfGames;
        int currentlySelectedGame = 0;
        playableRom selectedRom;

        private PictureBox selectedGamePic;
        private Label selectedGameName = new Label();
        private Label selectedGameSystem = new Label();
        private Label selectedGameGenre1 = new Label();
        private Label selectedGameGenre2 = new Label();
        private Label selectedGameGenre3 = new Label();

        public gameSelectionForm(List<playableRom> listToUse)
        {
            InitializeComponent();
            listOfGames = listToUse;
            setUpSelectedControls();
            populateList();
            updateSelection(0);
        }

        public void setSelectedGame(playableRom newSelect)
        {
            selectedRom = newSelect;
            Bitmap org = new Bitmap(selectedRom.imageLocation);
            Bitmap resized = new Bitmap(org, new Size(formSizes.selectedPicLength, formSizes.selectedPicHeight));
            selectedGamePic.Image = resized;
            selectedGameName.Text = "Game: " + selectedRom.gameName;
            selectedGameSystem.Text = "System: " + selectedRom.systemName;
        }

        public void setUpSelectedControls()
        {
            Label selectedBox = new Label();
            selectedBox.BackColor = Color.FromArgb(UISizes.selectBoxOpacity,UISizes.selectBoxColor);
            selectedBox.Location = new Point(UISizes.selectBoxX, UISizes.selectBoxY);
            selectedBox.Size = new Size(UISizes.selectBoxWidth, UISizes.selectBoxHeight);
            selectedBox.SendToBack();

            selectedGamePic = new PictureBox();
            Bitmap resizedImage = new Bitmap(selectedGamePic.ErrorImage, new Size(formSizes.selectedPicLength, formSizes.selectedPicHeight));
            selectedGamePic.Image = resizedImage;
            selectedGamePic.Size = new Size(formSizes.selectedPicLength, formSizes.selectedPicHeight);
            selectedGamePic.Location = new Point(formSizes.selectedPicX, formSizes.selectedPicY - 30);
            selectedGameName.AutoSize = true;
            selectedGameName.Font = new Font(selectedGameName.Font.FontFamily, UISizes.selectedGameTextSize);
            selectedGameName.Text = "Name: ";
            selectedGameName.Location = new Point(formSizes.selectedLabelsX, formSizes.selectedLabelY);
            selectedGameSystem.AutoSize = true;
            selectedGameSystem.Font = new Font(selectedGameSystem.Font.FontFamily, UISizes.selectedGameTextSize);
            selectedGameSystem.Text = "System: ";
            selectedGameSystem.Location = new Point(formSizes.selectedLabelsX,
                                                    formSizes.selectedLabelY - (formSizes.inbeetweenlabelY));
            selectedGameGenre1.Text = "Genres: ";
            selectedGameGenre1.Location = new Point(formSizes.selectedLabelsX,
                                                    formSizes.selectedLabelY - (formSizes.inbeetweenlabelY) * 2);
            selectedGameGenre2.Text = "";
            selectedGameGenre2.Location = new Point(formSizes.selectedLabelsX,
                                                    formSizes.selectedLabelY - (formSizes.inbeetweenlabelY) * 3);
            selectedGameGenre3.Text = "";
            selectedGameGenre3.Location = new Point(formSizes.selectedLabelsX,
                                                    formSizes.selectedLabelY - (formSizes.inbeetweenlabelY) * 4);
            Controls.Add(selectedGameName);
            Controls.Add(selectedGamePic);
            Controls.Add(selectedGameSystem);
            Controls.Add(selectedGameGenre1);
            Controls.Add(selectedGameGenre2);
            Controls.Add(selectedGameGenre3);
            Controls.Add(selectedBox);
        }

        //populates the list of games to select from
        private void populateList()
        {
            int xpos = UISizes.playablePictureX;
            int ypos = UISizes.playablePictureY;
            foreach(playableRom rom in listOfGames)
            {
                rom.addGameSelectorSender(this);
                rom.showRom(new Point(xpos, ypos), this.Controls);
                ypos += UISizes.spaceBetweenPicturesY;
                xpos += UISizes.spaceBetweenPicturesX;
            }
        }

        private void keyPressed(object sender, KeyEventArgs args)
        {
            switch(args.KeyCode)
            {
                case Keys.Down:
                    updateSelection(1);
                    break;
                case Keys.Up:
                    updateSelection(-1);
                    break;
                case Keys.Enter:
                    playSelectedRom();
                    break;
            }
        }

        private void playSelectedRom()
        {
            if(selectedRom != null)
            {
                romPlayHelper.playRom(selectedRom, this);
            }
        }

        private void updateSelection(int numToAdd)
        {
            if(currentlySelectedGame + numToAdd < 0 || currentlySelectedGame + numToAdd > listOfGames.Count - 1)
            {
                return;
            }
            foreach(playableRom rom in listOfGames)
            {
                Point movePoint = new Point(rom.getCurrentLocation().X + (UISizes.spaceBetweenPicturesX * -numToAdd),
                                            rom.getCurrentLocation().Y + (UISizes.spaceBetweenPicturesY * -numToAdd));
                rom.moveRom(movePoint);
            }
            currentlySelectedGame += numToAdd;
            setSelectedGame(listOfGames[currentlySelectedGame]);
            //todo: move pictures
        }

        private void gameSelectionForm_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(UIFileNames.backgroundImage);
            this.WindowState = FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.gameSelectionForm_FormClosed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(keyPressed);
        }

        private void gameSelectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
