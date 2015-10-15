using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;


namespace FYP16_WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Bitmap originalBitmap = null;
        private Bitmap previewBitmap = null;
        private Bitmap resultBitmap = null;

        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfd = new OpenFileDialog();
            opfd.Title = "Select an image file.";
            opfd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg|Tiff images(*.tiff)|*.tiff|Icon images(*.icon)|*.icon";
            opfd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            if (opfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(opfd.FileName);
                originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();

                previewBitmap = originalBitmap.CopyToThisCanvas(pictureBox1.Width);
                pictureBox1.Image = previewBitmap;

                ApplyFilter(true);
            }
        }
        /// <summary>
        /// Save file as
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveImg_Click(object sender, EventArgs e)
        {
            ApplyFilter(false);

            if (resultBitmap != null)
            {
                SaveFileDialog svfd = new SaveFileDialog();
                svfd.Title = "Specify a file name and file path";
                svfd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg|Tiff images(*.tiff)|*.tiff";
                svfd.Filter += "|Bitmap Images(*.bmp)|*.bmp|Svg Images(*.svg)|*.svg";


                if (svfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(svfd.FileName).ToUpper();
                    ImageFormat imgFormat = ImageFormat.Jpeg;

                    if (fileExtension == "BMP")
                    {
                        imgFormat = ImageFormat.Bmp;
                    }
                    else if (fileExtension == "JPG")
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }
                    else if (fileExtension == "PNG") 
                    {
                        imgFormat = ImageFormat.Png;
                    }
                    else if (fileExtension == "TIFF")
                    {
                        imgFormat = ImageFormat.Tiff;
                    }
                    else if (fileExtension == "ICON")
                    {
                        imgFormat = ImageFormat.Icon;
                    }
                    ///else if (fileExtension == "SVG")
                    ///{
                    ///    imgFormat = ImageFormat.Svg;
                    ///}

                    StreamWriter streamWriter = new StreamWriter(svfd.FileName, false);
                    resultBitmap.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();

                    resultBitmap = null;
                }
            }
        }

        /// <summary>
        /// Image Filter Options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter(true);
        }

        private void ApplyFilter(bool preview)
        {
            if (previewBitmap == null || comboBox1.SelectedIndex == -1)
            {
                return;
            }

            Bitmap selectedSource = null;
            Bitmap bitmapResult = null;

            if (preview == true)
            {
                selectedSource = previewBitmap;
            }
            else
            {
                selectedSource = originalBitmap;
            }
            if (selectedSource != null)
            {
                if (comboBox1.SelectedItem.ToString() == "None")
                {
                    bitmapResult = selectedSource;
                }
                else if (comboBox1.SelectedItem.ToString() == "Laplacian 3x3")
                {
                    bitmapResult = selectedSource.Laplacian3x3Filter(false);
                }
                else if (comboBox1.SelectedItem.ToString() == "Laplacian 3x3 Grayscale")
                {
                    bitmapResult = selectedSource.Laplacian3x3Filter(true);
                }
                else if (comboBox1.SelectedItem.ToString() == "Laplacian of Gaussian")
                {
                    bitmapResult = selectedSource.LaplacianOfGaussianFilter();
                }
            }

            if (bitmapResult != null)
            {
                if (preview == true)
                {
                    pictureBox1.Image = bitmapResult;
                }
                else
                {
                    resultBitmap = bitmapResult;
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
