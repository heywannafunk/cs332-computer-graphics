using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grayscale
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files|*.png| All files(*.*)|*.*";
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

        private void pictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //get image
            Bitmap ntsc = new Bitmap(pictureBox1.Image);
            Bitmap srgb = new Bitmap(pictureBox1.Image);//HDTV

            //get image dimensions
            int width = ntsc.Width;
            int height = ntsc.Height;

            //pixel color
            Color p;

            //grayscale
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //get pixel value 
                    p = ntsc.GetPixel(x, y);//

                    //extract pixel ARGB components
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    int Yntsc = (int)Math.Truncate(0.299 * r + 0.587 * g + 0.114 * b);
                    int Ysrgb = (int)Math.Truncate(0.2126 * r + 0.7152 * g + 0.0722 * b);

                    //set new ARGB value
                    ntsc.SetPixel(x, y, Color.FromArgb(a, Yntsc, Yntsc, Yntsc));
                    srgb.SetPixel(x, y, Color.FromArgb(a, Ysrgb, Ysrgb, Ysrgb));
                }
            }
            //load result
            pictureBox2.Image = ntsc;
            pictureBox3.Image = srgb;




            Bitmap diff = new Bitmap(pictureBox1.Image);
            //pixel colors
            Color p1;
            Color p2;

            Bitmap minuend = new Bitmap(pictureBox2.Image);
            Bitmap subtrahend = new Bitmap(pictureBox3.Image);

            //difference
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //get pixel value 
                    p1 = minuend.GetPixel(x, y);//
                    p2 = subtrahend.GetPixel(x, y);//

                    //extract pixel ARGB components
                    int a1 = p1.A;
                    int r1 = p1.R;
                    int g1 = p1.G;
                    int b1 = p1.B;

                    int a2 = p2.A;
                    int r2 = p2.R;
                    int g2 = p2.G;
                    int b2 = p2.B;

                    int Ydiff = (int)Math.Truncate(Math.Sqrt(Math.Pow(r2 - r1,2) + Math.Pow(g2 - g1, 2) + Math.Pow(b2 - b1, 2)));

                    //set new ARGB value
                    diff.SetPixel(x, y, Color.FromArgb(a1, Ydiff, Ydiff, Ydiff));
                }
            }
            //load result
            pictureBox4.Image = diff;
        }
    }
}
