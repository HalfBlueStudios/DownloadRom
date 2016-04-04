using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadRom
{
    class formSizes
    {
        public static int downloadButtonX = 3;
        public static int downloadButtonY = 346;
        public static int playButtonX = 3;
        public static int playButtonY = 118;
        public static int buttonWidth = 80;
        public static int buttonLength = 229;
        public static int formWidth = 1144;
        public static int formLength = 638;

        public static int recentlyDownloadedX = 1200;
        public static int recentlyDownloadedY = 15;
        public static int recentlyDownloadedSpaceBetween = -150;

        public static int recentlyPlayedX = 1050;
        public static int recentlyPlayedY = 15;
        public static int recentlyPlayedSpaceBetween = -150;

        public static int pictureTextSpaceBetweenY = -100;

        public static int gamePictureWidth = 100;
        public static int gamePictureHeight = 100;

        public static int pageStartX = 100;
        public static int pageStartY = 200;

        public static int pageBetweenX = 120;
        public static int pageBetweenY = 150;

        public static int lengthOfPage = 7;
        public static int depthOfPage = 3;

        public static int pageButtonLength = 100;
        public static int pageButtonHeight = 27;

        public static int betweenButtonX = 100;
        public static int pageButtonX = pageStartX - betweenButtonX;
        public static int pageButtonY = pageStartY - 30;

        public static int selectedPicX = 800;
        public static int selectedPicY = 300;
        public static int selectedPicLength = 150;
        public static int selectedPicHeight = 150;

        public static int selectedLabelsX = selectedPicX + selectedPicLength;
        public static int selectedLabelY = selectedPicY;
        public static int inbeetweenlabelY = -25;

        public static int playSelectedGameX = selectedLabelsX;
        public static int playSelectedGameY = selectedLabelY - (inbeetweenlabelY * 3);
        public static int playSelectedGameLength = 150;
        public static int playSelectedGameHeight = 75;

    }
}
