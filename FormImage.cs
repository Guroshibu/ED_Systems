using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ED_Systems
{
    public partial class FormImage : Form
    {
        public List<string> imgFiles;
        public List<string> brokenPaths = new List<string>();
        private Image img;
        int CurrentImgIndex;
        public FormImage()
        {
            InitializeComponent();
        }
        private void ChangeSize()
        {
            if (fullSizeToolStripMenuItem.Checked)
            {
                pbxImg.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                pbxImg.Width = img.Size.Width;
                pbxImg.Height = img.Size.Height;

                pnlImage.AutoScrollMinSize = pbxImg.Size;
            }
            else
            {
                pnlImage.AutoScrollMinSize = new Size();

                pbxImg.Width = pnlImage.Width;
                pbxImg.Height = pnlImage.Height;

                pbxImg.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right);
            }
        }
        private void GetImage(string path)
        {
            try
            {
                img = Image.FromFile(path);
                
            }
            catch (OutOfMemoryException ex)
            {
                img = Image.FromFile(@"images\notfound.png");
                if(brokenPaths.Count == 0 || brokenPaths.Find((x) => x == path) == "")
                {
                    brokenPaths.Add(path);
                }
            }
        }
        private void FormImage_Load(object sender, EventArgs e)
        {
            if (imgFiles.Count() == 0) return;
            CurrentImgIndex = 0;
            GetImage(imgFiles[CurrentImgIndex]);
            pbxImg.Image = img;
            ChangeSize();
        }
        private void fullSizeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ChangeSize();
        }

        private void priveousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(CurrentImgIndex == 0)
            {
                CurrentImgIndex = imgFiles.Count - 1;
            }
            else
            {
                CurrentImgIndex--;
            }
            GetImage(imgFiles[CurrentImgIndex]);
            pbxImg.Image = img;
            ChangeSize();
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentImgIndex == imgFiles.Count - 1)
            {
                CurrentImgIndex = 0;
            }
            else
            {
                CurrentImgIndex++;
            }
            GetImage(imgFiles[CurrentImgIndex]);
            pbxImg.Image = img;
            ChangeSize();
        }

        private void deleteImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Delete image?",
                                                    "Delete",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question,
                                                    MessageBoxDefaultButton.Button2,
                                                    MessageBoxOptions.DefaultDesktopOnly);
            if(result == DialogResult.Yes)
            {
                string path = imgFiles[CurrentImgIndex];
                if (brokenPaths.Count == 0 || brokenPaths.Find((x) => x == path) == "")
                {
                    try
                    {
                        img.Dispose();
                        pbxImg.Image.Dispose();
                        img = Image.FromFile(@"images\notfound.png");
                        pbxImg.Image = img;
                        File.Delete(path);
                        brokenPaths.Add(path);
                        imgFiles.RemoveAt(CurrentImgIndex);     //[CurrentImgIndex] = @"images\notfound.png";
                    }
                    catch(IOException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void FormImage_FormClosed(object sender, FormClosedEventArgs e)
        {
            img.Dispose();
            pbxImg.Image.Dispose();
        }
    }
}
