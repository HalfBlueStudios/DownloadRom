﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadRom
{
    static class UISizes
    {
        public static int animationXPos = 0;
        public static int animationYPos = 0;
        public static int animationWidth = Image.FromFile(UIFileNames.circleAnimationGif).Width;
        public static int animationHeight = Image.FromFile(UIFileNames.circleAnimationGif).Height;
        public static int betweenAnimationY = animationHeight + 100;
        public static int betweenAnimationX = 0;

        public static int textWidth = 100;
        public static int textHeight = 100;
        public static int fontSize = 50;
        public static int textXPos = 150;
        public static int textYPos = 40;

        public static double percentAnimationTransition = .1;
        public static int waitTimeAnimationTransition = 100;

        public static int pictureWidth = 100;
        public static int pictureHeight = 100;

        public static int playablePictureX = 100;
        public static int playablePictureY = 300;
        public static int spaceBetweenPicturesY = pictureHeight + 200;
        public static int spaceBetweenPicturesX = 0;

        public static int gameDescriptionX = 300;
        public static int gameDescriptionY = 200;

        public static int selectedGameTextSize = 20;


        public static int selectBoxOpacity = 200;
        public static Color selectBoxColor = Color.White;
        public static int selectBoxX = 0;
        public static int selectBoxY = formSizes.selectedPicY - 65;
        public static int selectBoxWidth = 6000;
        public static int selectBoxHeight = (int)(formSizes.selectedPicHeight * 1.5);
    }
}
