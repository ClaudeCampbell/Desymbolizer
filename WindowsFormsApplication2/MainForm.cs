using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Desymbolizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {            
            InitializeComponent();
            textBoxMainDir.Text = @Properties.Settings.Default.ReadFolder;
            checkBoxSearchOp.Checked = Properties.Settings.Default.SearchSubfolders;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Properties.Settings.Default.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderData.bar = progressBar;
            FolderData.label = labelBytes;
            FolderData.lockButton1 = button2;
            FolderData.lockButton2 = buttonReplace;
            FolderData.lockButton3 = buttonRevert;
            button2.Enabled = false;
            buttonReplace.Enabled = false;
            buttonRevert.Enabled = false;
            Logic.getLnkInfoThread();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = @Properties.Settings.Default.ReadFolder;
            fbd.ShowDialog();
            textBoxMainDir.Text = fbd.SelectedPath;
            FolderData.folder = textBoxMainDir.Text;
        }

        private void progressBar_Click(object sender, EventArgs e)
        {

        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            FolderData.bar = progressBar;
            FolderData.label = labelHappen;
            FolderData.lockButton1 = button2;
            FolderData.lockButton2 = buttonReplace;
            FolderData.lockButton3 = buttonRevert;
            button2.Enabled = false;
            buttonReplace.Enabled = false;
            buttonRevert.Enabled = false;
            Logic.desymbolizeFilesThread();
        }

        private void buttonRevert_Click(object sender, EventArgs e)
        {
            FolderData.bar = progressBar;
            FolderData.label = labelHappen;
            FolderData.lockButton1 = button2;
            FolderData.lockButton2 = buttonReplace;
            FolderData.lockButton3 = buttonRevert;
            button2.Enabled = false;
            buttonReplace.Enabled = false;
            buttonRevert.Enabled = false;
            Logic.symbolizeFilesThread();
        }

        private void textBoxMainDir_TextChanged(object sender, EventArgs e)
        {
            FolderData.folder = @textBoxMainDir.Text;
            Properties.Settings.Default.ReadFolder = @textBoxMainDir.Text;
        }

        private void checkBoxSearchOp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSearchOp.Checked)
            {
                Properties.Settings.Default.SearchSubfolders = true;
                FolderData.searchLevel = SearchOption.AllDirectories;
            }
            else
            {
                Properties.Settings.Default.SearchSubfolders = false;
                FolderData.searchLevel = SearchOption.TopDirectoryOnly;
            }
        }
    }
}
