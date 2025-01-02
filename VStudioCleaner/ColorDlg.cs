using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VStudioCleaner_ns
{
    public partial class ColorDlg : Form
    {
        public ColorDlg()
        {
            InitializeComponent();
        }

        private void acceptBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fgColorBtn_Click(object sender, EventArgs e)
        {
            colorDialog.Color = fgColorBtn.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                fgColorBtn.BackColor = colorDialog.Color;
            }
        }

        private void bgColorBtn_Click_1(object sender, EventArgs e)
        {
            colorDialog.Color = bgColorBtn.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                bgColorBtn.BackColor = colorDialog.Color;
            }
        }

        public Color FgColor
        {
            get { return fgColorBtn.BackColor; }
            set { fgColorBtn.BackColor = value; }
        }

        public Color BgColor
        {
            get { return bgColorBtn.BackColor; }
            set { bgColorBtn.BackColor = value; }
        }

    }
}
