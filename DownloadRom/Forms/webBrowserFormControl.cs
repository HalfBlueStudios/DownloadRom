using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadRom
{
    public partial class webBrowserFormControl : Form
    {
        public webBrowserFormControl(WebBrowser newBrowser)
        {
            InitializeComponent();
            browserToShow.Url = newBrowser.Url;
            browserToShow.Visible = true;
            browserToShow.ScriptErrorsSuppressed = true;
        }

        private void browserToShow_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }
    }
}
