using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace DownloadRom
{
    public partial class WebsiteSetUpForm : Form
    {

        const int startingX = 1000;
        const int startingY = 100;

        const int spaceBetweenX = 0;
        const int spaceBetweenY = -100;

        string searchString = "";
        Queue<configStep> stepsToTake = new Queue<configStep>();
        List<configStep> allSteps = new List<configStep>();

        string websiteName = "";
        string queryString = "";
        string firstSelection = "";
        string secondSelection = "";
        string lastSelection = "";
        string htmlOfSelections = "";

        Uri currentUri = null;
        int numTimes = 0;

        bool findingQueryString = false;
        bool inbetweenLoads = false;
        bool navigationStarted = false;

        public WebsiteSetUpForm()
        {
            InitializeComponent();
            doSetUp();
        }

        private void handleKeys(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Enter && browserOfWeb.Focused == false)
            {
                changePage();
            }
        }

        private void handleWebKeys(object sender, PreviewKeyDownEventArgs args)
        {
            if (findingQueryString == false)
            {
                return;
            }
            if (args.KeyCode == Keys.Enter)
            {
                inbetweenLoads = false;
                return;
            }
            if (args.KeyCode == Keys.Back)
            {
                if (searchString.Length > 0)
                {
                    searchString = searchString.Substring(0, searchString.Length - 1);
                }
            }
            else
            {
                if (numTimes % 2 == 0)
                {
                    numTimes++;
                    return;
                }
                string charToAdd = "" + (char)args.KeyValue;
                if (args.Shift == false)
                {
                    charToAdd = charToAdd.ToLower();
                }
                //have to changes spaces into what they would be in url
                if (charToAdd == " ")
                {
                    charToAdd = "+";
                }
                searchString += charToAdd;
                numTimes++;
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsolutePath == (sender as WebBrowser).Url.AbsolutePath && e.Url.AbsoluteUri.Contains(websiteName) == true)
            {
                inbetweenLoads = false;
            }
        }

        private void doSetUp()
        {
            KeyDown += new KeyEventHandler(handleKeys);
            browserOfWeb.PreviewKeyDown += new PreviewKeyDownEventHandler(handleWebKeys);
            this.KeyPreview = true;
            browserOfWeb.Url = new Uri("https://ign.com");
            browserOfWeb.Visible = true;
            browserOfWeb.AllowNavigation = true;
            browserOfWeb.ScriptErrorsSuppressed = true;
            browserOfWeb.Navigating += new WebBrowserNavigatingEventHandler(handleNavigation);
            setUpSteps();
            addStepsToQueue(0);
        }

        private void setUpSteps()
        {
            configStep websiteNameStep = new configStep(getWebsiteName, 1, "Get website name");
            configStep queryStep = new configStep(getQuery, 2, "Search for something");
            configStep firstSelectionStep = new configStep(getFirstResult, 3, "Click on first result");
            configStep nextSelectionStep = new configStep(getNextResult, 5, "Click on second result");
            configStep secondSelectionStep = new configStep(getLastResult, 5, "Click on last result");
            configStep getDownloadLinkStep = new configStep(getDownloadLink, 6, "Click on download link!");
            allSteps.Add(websiteNameStep);
            allSteps.Add(queryStep);
            allSteps.Add(firstSelectionStep);
            allSteps.Add(nextSelectionStep);
            allSteps.Add(secondSelectionStep);
            allSteps.Add(getDownloadLinkStep);
            placeAllStepLabels();
        }

        private void placeAllStepLabels()
        {
            int xVal = startingX;
            int yVal = startingY;
            foreach(configStep step in allSteps)
            {
                step.setStepNotCompleted();
                step.showStep(new Point(xVal, yVal), this.Controls);
                xVal -= spaceBetweenX;
                yVal -= spaceBetweenY;
            }
        }

        private void addStepsToQueue(int startNum)
        {
            for (int i = startNum; i < allSteps.Count; i++)
            {
                stepsToTake.Enqueue(allSteps[i]);
            }
        }

        private void dequeueAllSteps()
        {
            foreach (configStep action in stepsToTake)
            {
                stepsToTake.Dequeue();
            }
        }

        private void handleNavigation(object sender, WebBrowserNavigatingEventArgs args)
        {
            if (navigationStarted == true)
            {
                inbetweenLoads = true;
                //MessageBox.Show("lets try and go to: " + args.Url);
                currentUri = args.Url;
                int currentSize = stepsToTake.Count;
                configStep currentStep = stepsToTake.Peek();
                bool successful = currentStep.invokeStep(args);
                if(successful)
                {
                    currentStep.setStepCompleted();
                    stepsToTake.Dequeue();
                }
            }
            if (NavigationBox.Focused == false)
            {
                NavigationBox.Text = args.Url.AbsoluteUri;
            }
        }

        private void navButton_Click(object sender, EventArgs e)
        {
            changePage();
        }

        private void changePage()
        {
            string newUrl = NavigationBox.Text;

            if (newUrl.Contains("www.") == false)
            {
                newUrl = "www." + newUrl;
            }
            if (newUrl.Contains("http://") == false)
            {
                newUrl = "http://" + newUrl;
            }
            navigationStarted = true;
            browserOfWeb.Url = new Uri(newUrl);
        }


        private string getStartingPoint(string htmlText)
        {
            string searchString = "<div class=";
            int startOfSelections = htmlText.IndexOf(firstSelection);
            int currentFound = 0; //set to something less than what your looking for
            int previousFound = -1; //set to something unfindable
            while(currentFound < startOfSelections)
            {
                previousFound = currentFound;
                currentFound = htmlText.IndexOf(searchString, currentFound + 1);
            }
            int getEndOfClassName = htmlText.IndexOf('"', previousFound);//gets first quotation
            getEndOfClassName = htmlText.IndexOf('"', getEndOfClassName + 1);//gets ending quotation
            string classFound = htmlText.Substring(previousFound, getEndOfClassName - previousFound + 1);
            if(htmlText.IndexOf(classFound) == previousFound)
            {
                MessageBox.Show("good candidate!");
            }
            return (classFound);

        }

        public string getEndingPoint(string htmlText)
        {
            string searchString = "<div class=";
            int endOfSelections = htmlText.IndexOf(lastSelection);
            int foundClassSpot = endOfSelections;
            string classFound = "";
            bool found = false;
            while (found == false)
            {
                foundClassSpot = htmlText.IndexOf(searchString, foundClassSpot + 1);
                int getEndOfClassName = htmlText.IndexOf('"', foundClassSpot);//gets first quotation
                getEndOfClassName = htmlText.IndexOf('"', getEndOfClassName + 1);//gets ending quotation
                classFound = htmlText.Substring(foundClassSpot, getEndOfClassName - foundClassSpot + 1);
                if (htmlText.IndexOf(classFound) == foundClassSpot)
                {
                    found = true;
                }
            }
            return (classFound);
        }

        private void getAllNames()
        {
            string htmlText = htmlHelper.getHtml(queryString);
            List<string> allnames = getAllSelectionNames(htmlText.IndexOf(getStartingPoint(htmlText)), 
                                                   htmlText.IndexOf(getEndingPoint(htmlText)),
                                                   htmlText, getNumRefBetween(htmlText));
            allnames = getAllSelections(getStartingPoint(htmlText), htmlText);
            foreach(string str in allnames)
            {
                comboBox1.Items.Add(str);
            }
                                                    
        }

        //gets the number of hrefs between each selection
        private int getNumRefBetween(string htmlText)
        {
            string searchString = "href=";
            int numRefs = 0;
            int currentPosition = htmlText.IndexOf(firstSelection);
            int endingPosition = htmlText.IndexOf(secondSelection);
            currentPosition = htmlText.IndexOf(searchString, currentPosition + 1);
            while(currentPosition < endingPosition)
            {
                currentPosition = htmlText.IndexOf(searchString, currentPosition + 1);
                numRefs++;
            }
            return (numRefs - 1); //gets rid of the last href that goes with the next selection;
        }

        private List<string> getAllSelectionNames(int startingSection, int endingSection, string htmlText, int hrefBetween)
        {
            List<string> retList = new List<string>();
            int currentPosition = startingSection;
            string searchString = "href";
            int numHref = hrefBetween;
            currentPosition = htmlText.IndexOf(searchString, currentPosition + 1);
            while (currentPosition < endingSection && currentPosition != -1)
            {
                int closeBracket = htmlText.IndexOf('>', currentPosition) + 1;
                int openBracket = htmlText.IndexOf('<', closeBracket) + 1;
                if (hrefBetween - numHref == 0)
                {
                    numHref = 0;
                    string newOption = htmlText.Substring(closeBracket, openBracket - closeBracket);
                    retList.Add(newOption);
                }
                else
                {
                    numHref++;
                }
                currentPosition = htmlText.IndexOf(searchString, openBracket); 

            }
            return (retList);
        }

        private List<string> getAllSelections(string className, string htmlText)
        {
            List<string> retList = new List<string>();
            int currentPosition = htmlText.IndexOf(className);
            while (currentPosition != -1)
            {
                int hrefPosition = htmlText.IndexOf("href=", currentPosition);
                int closeBracket = htmlText.IndexOf('>', hrefPosition) + 1;
                int openBracket = htmlText.IndexOf('<', closeBracket);
                string newOption = htmlText.Substring(closeBracket, openBracket - closeBracket);
                retList.Add(newOption);
                currentPosition = htmlText.IndexOf(className, currentPosition + 1);

            }
            return (retList);
        }

        /// <summary>
        /// PAST HERE ALL FUNCTIONS ARE FOR STEPS
        /// </summary>

        private string getWebsiteName(WebBrowserNavigatingEventArgs args)
        {
            websiteName = currentUri.AbsoluteUri;
            int wwwIndex = websiteName.IndexOf("www.") + 4;
            //if no www then not a valid website name
            if (wwwIndex == -1)
            {
                return null;
            }
            websiteName = websiteName.Substring(wwwIndex, (websiteName.Length) - wwwIndex);
            findingQueryString = true;
            return("website name is set to " + websiteName);
        }

        private string getQuery(WebBrowserNavigatingEventArgs args)
        {
            if (searchString == "")
            {
                return null;
            }
            if (currentUri.AbsoluteUri.Contains(websiteName) == false ||
               currentUri.AbsoluteUri.Contains(searchString) == false)
            {
                return null;
            }
            queryString = currentUri.AbsoluteUri;
            htmlOfSelections = htmlHelper.getHtml(queryString);
            return ("We detected you searched for: " + searchString);
            //MessageBox.Show("web name is : " + websiteName);
            //MessageBox.Show("query page is : " + queryString);
        }

        private string getFirstResult(WebBrowserNavigatingEventArgs args)
        {
            if(currentUri.AbsoluteUri.Contains(websiteName) == false || 
                //htmlOfSelections.Contains(currentUri.AbsoluteUri) == false ||
               currentUri.AbsoluteUri.Contains(".html") == true) 
            {
                return null;
            }
            firstSelection = currentUri.AbsolutePath;
            //MessageBox.Show("set first selection as " + firstSelection);
            args.Cancel = true;
            return ("set first selection as \n" + firstSelection);

        }

        private string getNextResult(WebBrowserNavigatingEventArgs args)
        {
            if (currentUri.AbsoluteUri.Contains(websiteName) == false ||
               //htmlOfSelections.Contains(currentUri.AbsoluteUri) == false ||
               currentUri.AbsoluteUri.Contains(".html") == true)
            {
                return null;
            }
            secondSelection = currentUri.AbsolutePath;
            //MessageBox.Show("set first selection as " + firstSelection);
            args.Cancel = true;
            return ("set second selection as \n" + firstSelection);

        }

        private string getLastResult(WebBrowserNavigatingEventArgs args)
        {
            if (currentUri.AbsoluteUri.Contains(websiteName) == false ||
            //htmlOfSelections.Contains(currentUri.AbsoluteUri) == false ||
            currentUri.AbsoluteUri.Contains(".html") == true)
            {
                return null;
            }
            lastSelection = currentUri.AbsolutePath;
            //MessageBox.Show("set last selection as " + lastSelection);
            getAllNames();
            return ("set last selection as /n" + lastSelection);
        }

        private string getDownloadLink(WebBrowserNavigatingEventArgs args)
        {
            //todo: implement
            return (null);
        }
    }

    class configStep
    {
        const int textSize = 20;
        const int resultsSize = 15;

        const int spaceBetweenX = 0;
        const int spaceBetweenY = -(textSize + 20);

        Func<WebBrowserNavigatingEventArgs, string> actionToTake;
        string nameOfStep;
        string output;
        int stepNum;
        Label stepDescription;
        Label stepResults;

        public configStep(Func<WebBrowserNavigatingEventArgs, string> newAction, int newStepNum, string newNameOfStep)
        {
            actionToTake = newAction;
            nameOfStep = newNameOfStep;
            stepNum = newStepNum;
            stepDescription = new Label();
            stepDescription.AutoSize = true;
            stepDescription.Font = new Font(stepDescription.Font.FontFamily, textSize);
            stepDescription.Text = "Step " + stepNum + ": " + nameOfStep;
            stepResults = new Label();
            stepResults.AutoSize = true;
            stepResults.Font = new Font(stepDescription.Font.FontFamily, resultsSize);
        }

        public void setStepNotCompleted()
        {
            stepDescription.ForeColor = Color.Red;
        }

        public void setStepCompleted()
        {
            stepDescription.ForeColor = Color.Green;
        }

        public void showStep(Point newPoint, System.Windows.Forms.Control.ControlCollection controlToAdd)
        {
            stepDescription.Location = newPoint;
            stepResults.Location = new Point(newPoint.X - spaceBetweenX, newPoint.Y - spaceBetweenY);
            stepDescription.Visible = true;
            stepResults.Visible = true;
            controlToAdd.Add(stepDescription);
            controlToAdd.Add(stepResults);
        }

        public bool invokeStep(WebBrowserNavigatingEventArgs args)
        {
            string stepOutput = actionToTake.Invoke(args);
            if (stepOutput == null)
            {
                return (false);
            }
            else
            {
                //MessageBox.Show(nameOfStep + " returning true!");
                stepResults.Text = stepOutput;
                return (true);
            }
        }
    }
}
