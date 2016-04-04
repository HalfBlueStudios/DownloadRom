using DownloadRom.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DownloadRom
{                                                                                                                                                                 
    public partial class selectionForm : Form
    {
        private int currentSelection;
        optionList currentList;
        optionList previousList;
        bool inAnimation = false;
        bool activeCheck = true;

        public selectionForm()
        {
            InitializeComponent();
        }

        private void FirstAnimationBox_Click(object sender, EventArgs e)
        {
             
        }

        private void keyPressed(object sender, KeyEventArgs args)
        {
            //if currently in animation, dont handle key presses
            if(inAnimation == true)
            {
                return;
            }
            switch(args.KeyCode)
            {
                case System.Windows.Forms.Keys.Up:
                    moveSelection(-1);
                    break;

                case System.Windows.Forms.Keys.Down:
                    moveSelection(1);
                    break;

                case System.Windows.Forms.Keys.Back:
                    replaceOptions(previousList);
                    previousList = null;
                    break;
                case System.Windows.Forms.Keys.Enter:
                    handleSelection();
                    break;
            }
        }


        //checks the controller input
        private void controllerCheck()
        {
            while (activeCheck == true)
            {
                GamePadState currentState = GamePad.GetState(PlayerIndex.One);
                if (currentState.IsConnected == true)
                {
                    MessageBox.Show("connected!");
                    if (currentState.DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        moveSelection(-1);
                    }
                    if (currentState.DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        moveSelection(-1);
                    }
                    if (currentState.Buttons.B == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        replaceOptions(previousList);
                        previousList = null;
                    }
                    if (currentState.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        handleSelection();
                    }
                }
                else
                {
                    MessageBox.Show("controller not connected");
                }
                Thread.Sleep(UIconfig.controllerThreadSleep);
            }
        }

        //---------------------------------------------------
        //handles what to do once a user makes a selection
        //---------------------------------------------------
        private void handleSelection()
        {
            Form newForm = currentList.getSelectionList()[currentSelection].getForm();
            //if null then there is another list to load instead of a new form
            if (newForm == null)
            {
                optionList newList = currentList.getSelectionList()[currentSelection].chooseOption();
                if(newList == null)
                {
                    throw new ArgumentNullException();
                }
                replaceOptions(newList);
            }
            else
            {
                activeCheck = false;
                newForm.Visible = true;
                this.Visible = false;
            }
        }

        private void moveSelection(int numToMove)
        {
            currentList.getSelectionList()[currentSelection].unselectOption();
            currentSelection += numToMove;
            if(currentSelection < 0)
            {
                currentSelection = currentList.getSelectionList().Count - 1;
            }
            if(currentSelection > currentList.getSelectionList().Count - 1)
            {
                currentSelection = 0;
            }
            currentList.getSelectionList()[currentSelection].selectOption();
        }

        private void doSetUp()
        {
            optionList initalList = setUpLists();
            replaceOptions(initalList);
        }

        private void replaceOptions(optionList newList)
        {
            inAnimation = true;
            if (newList == null)
            {
                return;
            }
            if (currentList != null)
            {
                Thread[] transitionThreads = new Thread[currentList.getSelectionList().Count];
                for(int i = 0; i < transitionThreads.Length; i++)
                {
                    transitionThreads[i] = new Thread(new ThreadStart(currentList.getSelectionList()[i].preformTransition));
                    transitionThreads[i].Start();
                }
                while(transitionThreads[transitionThreads.Length - 1].ThreadState != ThreadState.Stopped)
                {
                    Application.DoEvents();
                }
                foreach(selectionOption option in currentList.getSelectionList())
                {
                    option.hideOption(this.Controls);
                }
            }
            int xPostion = UISizes.animationXPos;
            int yPosition = UISizes.animationYPos;
            previousList = currentList;
            currentList = newList;
            currentList.getSelectionList()[0].selectOption();
            foreach (selectionOption option in currentList.getSelectionList())
            {
                option.showOption(new System.Drawing.Point(xPostion, yPosition), this.Controls);
                xPostion += UISizes.betweenAnimationX;
                yPosition += UISizes.betweenAnimationY;
            }
            currentSelection = 0;
            inAnimation = false;

        }

        //--------------------------------------------------
        //Sets up all options and returns the inital options
        //--------------------------------------------------
        private optionList setUpLists()
        {
            //need to add forms to these
            List<selectionOption> playOptions = new List<selectionOption>();
            selectionOption recPlayedOption = new selectionOption("recently played", UIFileNames.circleAnimationGif, UIFileNames.circleNonSelected, null,
                                                                  new gameSelectionForm(databaseHelper.getRecently(tableNames.recentlyPlayedTableName)));
            playOptions.Add(recPlayedOption);
            selectionOption recDownloadOption = new selectionOption("recently downloaded", UIFileNames.circleAnimationGif, UIFileNames.circleNonSelected, null,
                                                                    new gameSelectionForm(databaseHelper.getRecently(tableNames.recentlyDownloadedTableName)));
            playOptions.Add(recDownloadOption);
            selectionOption searchAllOption = new selectionOption("Search all", UIFileNames.circleAnimationGif, UIFileNames.circleNonSelected, null, null);
            playOptions.Add(searchAllOption);
            optionList playList = new optionList(playOptions);

            List<selectionOption> initalOptionsList = new List<selectionOption>();
            selectionOption playOption = new selectionOption("Play", UIFileNames.circleAnimationGif, UIFileNames.circleNonSelected, playList, null);
            initalOptionsList.Add(playOption);
            //need to add forms here
            selectionOption downloadOption = new selectionOption("Download", UIFileNames.circleAnimationGif, UIFileNames.circleNonSelected, null, null);
            initalOptionsList.Add(downloadOption);
            selectionOption optionsOption = new selectionOption("Options", UIFileNames.circleAnimationGif, UIFileNames.circleNonSelected, null, null);
            initalOptionsList.Add(optionsOption);
            optionList returnList = new optionList(initalOptionsList);

            return (returnList);



            //selectionOption downloadOption = new selectionOption("")
            //optionList startList = new optionList();
        }

        private void selectionForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            //this.DoubleBuffered = true;
            Thread gameCheckThread = new Thread(new ThreadStart(controllerCheck));
            //gameCheckThread.Start();
            this.BackgroundImage = Image.FromFile(UIFileNames.backgroundImage);
            currentSelection = 0;
            this.KeyPreview = true;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(keyPressed);
            doSetUp();
        }
    }
}
