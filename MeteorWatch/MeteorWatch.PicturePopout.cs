using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MeteorWatch
{
    public partial class Scatterthon : Form
    {
        // Screenshots within Current Interval...
        private static List<string> thumbnailsWithinInterval = new List<string>();
        private static string imageDirectory = string.Empty;
        private static int thumbnailIndex = -1;

        private void picBoxHighRes1_DoubleClick(object sender, EventArgs e)
        {
            // Thumbnail 1...
            ShowPicturePopup(picBoxHighRes1, config.HighResolutionScreenshotsDirectory);
        }

        private void picBoxHighRes2_DoubleClick(object sender, EventArgs e)
        {
            // Thumbnail 2...
            ShowPicturePopup(picBoxHighRes2, config.HighResolutionScreenshotsDirectory);
        }

        private void picBoxHighRes3_DoubleClick(object sender, EventArgs e)
        {
            // Thumbnail 3...
            ShowPicturePopup(picBoxHighRes3, config.HighResolutionScreenshotsDirectory);
        }

        private void picBoxScreenshot_DoubleClick(object sender, EventArgs e)
        {
            // Main screenshot...
            ShowPicturePopup(picBoxScreenshot, config.OriginalScreenshotsDirectory);
        }

        private static void ShowPicturePopup(PictureBox picBox, string imageLocation) 
        {
            Image imageToUse = picBox.Image;
            string imageHome = picBox.ImageLocation;

            if (imageToUse != null)
            {
                // First find the index for this image...
                imageDirectory = imageLocation;
                string[] captures = Directory.GetFiles(imageDirectory, string.Format("*.jpg"));

                for (int i = 0; i < captures.Length; i++ )
                {
                    if (captures[i] == imageHome)
                    {
                        thumbnailIndex = i;
                        break;
                    }
                }

                using (Form form = new Form())
                {
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.Size = new Size(1000, 700);

                    PictureBox pb = new PictureBox();
                    pb.Dock = DockStyle.Fill;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.Image = imageToUse;

                    form.KeyDown += form_KeyDown;
                    form.DoubleClick += form_DoubleClick;

                    form.Controls.Add(pb);
                    form.Text = imageHome;
                    form.ShowDialog();
                }
            }
        }

        static void form_DoubleClick(object sender, EventArgs e)
        {
            (sender as Form).WindowState = FormWindowState.Maximized;
        }

        static void form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    (sender as Form).Close();
                    break;
                case Keys.Right:
                    MoveToNextImage(sender, true);
                    break;
                case Keys.Left:
                    MoveToNextImage(sender, false);
                    break;
                default:
                    break;
            }
        }

        static void MoveToNextImage(object sender, bool goRight)
        {            
            string[] captures = Directory.GetFiles(imageDirectory, string.Format("*.jpg"));

            if (goRight)
            {
                if (thumbnailIndex < (captures.Length - 1))
                {
                    thumbnailIndex++;
                }
                else
                {
                    thumbnailIndex = 0;
                }
            }
            else // going left...
            {
                if (thumbnailIndex == 0)
                {
                    thumbnailIndex = captures.Length - 1;
                }
                else
                {
                    thumbnailIndex--;
                }
            }

            Form popoutForm = sender as Form;

            (popoutForm.Controls[0] as PictureBox).Image = CreateBitmap(captures[thumbnailIndex]);
            popoutForm.Text = captures[thumbnailIndex];
        }

        private static Bitmap CreateBitmap(string pathToScreenshot)
        {
            // Get the content of the screenshot file.
            Bitmap srcBitmap = (Bitmap)Image.FromFile(pathToScreenshot);
            Rectangle rec = new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height);

            // Create the new bitmap and associated graphics object
            Bitmap bmp = new Bitmap(rec.Width, rec.Height);
            Graphics g = Graphics.FromImage(bmp);

            // Draw the specified section of the source bitmap to the new one
            g.DrawImage(srcBitmap, 0, 0, rec, GraphicsUnit.Pixel);
            g.Dispose();
            srcBitmap.Dispose();
            return bmp;
        }
    }
}
