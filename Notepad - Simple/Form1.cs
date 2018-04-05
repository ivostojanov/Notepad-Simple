using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.IO;

namespace Notepad___Simple
{
    public partial class Form1 : Form
    {
        string openFilePath = null;
        bool justSaved = false;

        public Form1(string openWithPath)
        {
            InitializeComponent();
                        
            if (openWithPath != string.Empty)
            {
                openFilePath = openWithPath;
                //Open the file
                try
                {
                    fileText.LoadFile(openFilePath, RichTextBoxStreamType.RichText);
                }
                catch (Exception ex)
                {
                    fileText.LoadFile(openFilePath, RichTextBoxStreamType.PlainText);
                }
                
                this.Text = openFilePath;
            }

            justSaved = true;
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dr=DialogResult.No;
            if (!justSaved)
            {
                dr = MessageBox.Show("Content will be lost!\nAre you sure?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }           

            if(justSaved || dr == DialogResult.Yes)
                Application.Exit();
                
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileText.Clear();
            this.Text = "Notepad - Simple";
            openFilePath = null;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|";
            if (op.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileText.LoadFile(op.FileName, RichTextBoxStreamType.RichText);
                }catch(Exception ex)
                {
                    fileText.LoadFile(op.FileName, RichTextBoxStreamType.PlainText);
                }

                justSaved = true;
                openFilePath = op.FileName;
                this.Text = op.FileName;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFilePath == null)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                try
                {
                    fileText.SaveFile(openFilePath, RichTextBoxStreamType.RichText);
                }
                catch (Exception ex)
                {
                    fileText.SaveFile(openFilePath, RichTextBoxStreamType.PlainText);
                }
                justSaved = true;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileText.SaveFile(sf.FileName, RichTextBoxStreamType.RichText);
                }
                catch (Exception ex)
                {
                    fileText.SaveFile(sf.FileName, RichTextBoxStreamType.PlainText);
                }
                justSaved = true;
                openFilePath = sf.FileName;
                this.Text = sf.FileName;
            }            
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

        private void backgroundColorToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                fileText.SelectionBackColor = colorDialog.Color;
            }
        }                     

        private void colorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                fileText.SelectionColor = colorDialog.Color;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by Ivan Stojanov", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {                
                printDocument1.Print();
            }            
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            char[] param = { '\n' };

            if (printDialog1.PrinterSettings.PrintRange == PrintRange.Selection)
            {
                lines = fileText.SelectedText.Split(param);
            }
            else
            {
                lines = fileText.Text.Split(param);
            }

            int i = 0;
            char[] trimParam = { '\r' };
            foreach (string s in lines)
            {
                lines[i++] = s.TrimEnd(trimParam);
            }
        }

        private int linesPrinted;
        private string[] lines;

        private void OnPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Brush brush = new SolidBrush(fileText.ForeColor);

            while (linesPrinted < lines.Length)
            {
                e.Graphics.DrawString(lines[linesPrinted++],
                    fileText.Font, brush, x, y);
                y += 15;
                if (y >= e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            linesPrinted = 0;
            e.HasMorePages = false;
        }

        private void fileText_TextChanged(object sender, EventArgs e)
        {
            justSaved = false;
        }

        private void gitHubToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ivostojanov/Notepad-Simple");
        }
        
    }
}
