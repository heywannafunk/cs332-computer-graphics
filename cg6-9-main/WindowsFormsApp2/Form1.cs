using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = "PNG files|*.png| jpg files(*.jpg)|*.jpg| All files(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pictureBox1.LoadAsync(dialog.FileName);
                    //pictureBox1.ImageLocation = dialog.FileName;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        enum ColorChannel
        {
            R,G,B,
        }

        private Bitmap highlightChannel(ColorChannel channel, Image image)
        {
            Bitmap bitmap_image = new Bitmap(image);
            for (int i = 0; i < bitmap_image.Width; i++)
            {
                for (int j = 0; j < bitmap_image.Height; j++)
                {
                    //get the pixel from the scrBitmap image
                    var actualColor = bitmap_image.GetPixel(i, j);
                    var valueAlpha = actualColor.A;
                    switch (channel)
                    {
                        case ColorChannel.R:
                            {
                                var value = actualColor.R;
                                var newColor = Color.FromArgb(valueAlpha, value, 0, 0);
                                bitmap_image.SetPixel(i, j, newColor);
                                break;
                            }
                        case ColorChannel.G:
                            {
                                var value = actualColor.G;
                                var newColor = Color.FromArgb(valueAlpha, 0, value, 0);
                                bitmap_image.SetPixel(i, j, newColor);
                                break;
                            }
                        case ColorChannel.B:
                            {
                                var value = actualColor.B;
                                var newColor = Color.FromArgb(valueAlpha, 0, 0, value);
                                bitmap_image.SetPixel(i, j, newColor);
                                break;
                            }
                        default: break;
                    }
                }
            }
            return bitmap_image;

        }

        private int[] createDataForChart(ColorChannel channel, Image image)
        {
            int[] color = new int[256];
            Bitmap bitmap_image = new Bitmap(image);
            for (int i = 0; i < bitmap_image.Width; i++)
            {
                for (int j = 0; j < bitmap_image.Height; j++)
                {
                    //get the pixel from the scrBitmap image
                    var actualColor = bitmap_image.GetPixel(i, j);
                    switch (channel)
                    {
                        case ColorChannel.R:
                            {
                                var value = actualColor.R;
                                color[value]++;
                                break;
                            }
                        case ColorChannel.G:
                            {
                                var value = actualColor.G;
                                color[value]++;
                                break;
                            }
                        case ColorChannel.B:
                            {
                                var value = actualColor.B;
                                color[value]++;
                                break;
                            }
                        default: break;
                    }
                }
            }
            return color;
        }
        

        private void PictureBox1_LoadCompleted(Object sender, AsyncCompletedEventArgs e)
        {
            var redImage = highlightChannel(ColorChannel.R, pictureBox1.Image);
            var greenImage = highlightChannel(ColorChannel.G, pictureBox1.Image);
            var blueImage = highlightChannel(ColorChannel.B, pictureBox1.Image);

            pictureBoxRed.Image = redImage;
            pictureBoxGreen.Image = greenImage;
            pictureBoxBlue.Image = blueImage;

            var valuesRed = createDataForChart(ColorChannel.R, pictureBox1.Image);
            var valuesGreen = createDataForChart(ColorChannel.G, pictureBox1.Image);
            var valuesBlue = createDataForChart(ColorChannel.B, pictureBox1.Image);

            this.chartR.Series.Clear();
            this.chartG.Series.Clear();
            this.chartB.Series.Clear();

            for (int i = 0; i < 256; i++)
            {
                Series series = this.chartR.Series.Add(i.ToString());
                series.Points.Add(valuesRed[i]);
                series = this.chartG.Series.Add(i.ToString());
                series.Points.Add(valuesGreen[i]);
                series = this.chartB.Series.Add(i.ToString());
                series.Points.Add(valuesBlue[i]);
            }
            
        }


    }
}
