using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VStudioCleaner_ns
{
    /// Author: Dennis Lang - 2009
    /// https://landenlabs.com/
    /// 
    /// Used to prompt for input from a tabular list field.
    /// Place box directly over tabular list field and monitor change event.
    /// 
    public partial class FieldBox : Form
    {
        public FieldBox()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
        }

        public string FieldText
        {
            get { return this.textBox.Text; }
            set { this.textBox.Text = value; }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}