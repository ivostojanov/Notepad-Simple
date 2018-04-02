using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad___Simple
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Content will be lost! Are you sure?", "Exit", MessageBoxButtons.YesNo);

            if(dr == DialogResult.Yes)
                Application.Exit();
                
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileText.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileText.LoadFile(op.FileName, RichTextBoxStreamType.PlainText);
                }catch(Exception ex)
                {
                    
                }
                
                this.Text = op.FileName;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|";
            if (sf.ShowDialog() == DialogResult.OK)
                fileText.SaveFile(sf.FileName, RichTextBoxStreamType.PlainText);
            this.Text = sf.FileName;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileText.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileText.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileText.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileText.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileText.Redo();
        }

        private void fontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = fileText.SelectionFont;
            if(fd.ShowDialog() == DialogResult.OK)
            {
                fileText.SelectionFont = fd.Font;
            }
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                fileText.SelectionBackColor = colorDialog.Color;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by Ivan Stojanov", "About", MessageBoxButtons.OK);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|";
            if (sf.ShowDialog() == DialogResult.OK)
                fileText.SaveFile(sf.FileName, RichTextBoxStreamType.PlainText);
            this.Text = sf.FileName;
        }
    }
}
