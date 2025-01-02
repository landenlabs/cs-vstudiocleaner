using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace VStudioCleaner_ns
{
    public partial class HelpDialog : Form
    {
        public HelpDialog()
        {
            InitializeComponent();

            // HTML help is embedded, load the embedded html resource
            Assembly a = Assembly.GetExecutingAssembly();
            string[] files = a.GetManifestResourceNames();
            Stream htmlStream = a.GetManifestResourceStream("VStudioCleaner_ns.Resources.help.html");
            this.webBrowser.DocumentStream = htmlStream;
        }
   
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
