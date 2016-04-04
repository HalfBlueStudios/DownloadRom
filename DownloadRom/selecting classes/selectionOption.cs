using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadRom
{
    class selectionOption
    {
        PictureBox animationBox;
        Label optionText;
        bool currentlySelected;
        string inAnimationPicture;
        string nonAnimationPicture;
        optionList listIfSelected; //the new options that are presented if this option is chosen
        Form formToLoad; //if selected, tells what form to load, null if no form to load

        public selectionOption(string nameOfSelection, string newInAnim, 
                               string newOutAnim, optionList newSelectedList, Form newForm)
        {
            animationBox = new PictureBox();
            animationBox.Height = UISizes.animationHeight;
            animationBox.Width = UISizes.animationWidth;
            animationBox.BackColor = Color.Transparent;

            optionText = new Label();
            optionText.AutoSize = true;
            optionText.Font = new Font(optionText.Font.FontFamily, UISizes.fontSize);
            optionText.Text = nameOfSelection;
            optionText.ForeColor = Color.White;
            optionText.BackColor = Color.Black;

            inAnimationPicture = newInAnim;
            nonAnimationPicture = newOutAnim;
            animationBox.ImageLocation = newOutAnim;
            currentlySelected = false;
            listIfSelected = newSelectedList;
            formToLoad = newForm;
        } 

        public void selectOption()
        {
            currentlySelected = true;
            animationBox.ImageLocation = inAnimationPicture;
            animationBox.Refresh();
        }

        public void unselectOption()
        {
            currentlySelected = false;
            animationBox.ImageLocation = nonAnimationPicture;
            animationBox.Refresh();
        }

        public bool isSelected()
        {
            return (currentlySelected);
        }

        public optionList chooseOption()
        {
            return (listIfSelected);
        }

        public Form getForm()
        {
            return (formToLoad);
        }

        // <summary>
        // preforms the transition animation between menus
        // </summary>
        public void preformTransition()
        {
            //PUT TRANSITION HERE

            /*
            int newWidth = animationBox.Size.Width;
            int decrementAmmount = (int)(newWidth * UISizes.percentAnimationTransition);
            while(newWidth > 0)
            {
                newWidth -= decrementAmmount;
                animationBox.Size = new Size(newWidth, animationBox.Size.Height);
                Thread.Sleep(UISizes.waitTimeAnimationTransition);
            }
            */
        }

        public void showOption(Point newPoint, System.Windows.Forms.Control.ControlCollection controlToAdd)
        {
            //still have to implement position stuff here
            animationBox.Location = newPoint;
            optionText.Location = new Point(newPoint.X + UISizes.textXPos, newPoint.Y + UISizes.textYPos);
            animationBox.Visible = true;
            optionText.Visible = true;
            controlToAdd.Add(optionText);
            controlToAdd.Add(animationBox);
        }

        public void hideOption(System.Windows.Forms.Control.ControlCollection controlToRemove)
        {
            animationBox.Visible = false;
            optionText.Visible = false;
            controlToRemove.Remove(animationBox);
            controlToRemove.Remove(optionText);
        }

    }
}
