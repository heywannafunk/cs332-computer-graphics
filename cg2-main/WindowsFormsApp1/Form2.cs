using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        Color p = Color.Black;
        Color border = Color.Red;
        Bitmap canvas;
        Bitmap image;

        void FloodFill4(Bitmap bmp, int x, int y, Color fill, Color old)
        {
            if (x >= 0 && x < pictureBox1.Width && y >= 0 && y < pictureBox1.Height && bmp.GetPixel(x, y) == old && bmp.GetPixel(x, y) != fill)
            {
                bmp.SetPixel(x, y, fill);
                FloodFill4(bmp, x + 1, y, fill, old);
                FloodFill4(bmp, x - 1, y, fill, old);
                FloodFill4(bmp, x, y + 1, fill, old);
                FloodFill4(bmp, x, y - 1, fill, old);
            }
            pictureBox1.Image = bmp;
        }

        void PicFill4(Bitmap bmp, int x, int y, Color fill, Color old)
        {
            if (x >= 0 && x < pictureBox1.Width && y >= 0 && y < pictureBox1.Height && bmp.GetPixel(x, y) == old && bmp.GetPixel(x, y) != fill)
            {
                bmp.SetPixel(x, y, fill);
                PicFill4(bmp, x + 1, y, fill, old);
                PicFill4(bmp, x - 1, y, fill, old);
                PicFill4(bmp, x, y + 1, fill, old);
                PicFill4(bmp, x, y - 1, fill, old);
            }
            pictureBox1.Image = bmp;
        }

        void PicFill4Centered(Bitmap bmp, int x, int y, int px, int py, Color old)
        {
            if (x >= 0 && x < pictureBox1.Width && y >= 0 && y < pictureBox1.Height && bmp.GetPixel(x, y) == old && bmp.GetPixel(x, y) != image.GetPixel(px,py))
            {
                bmp.SetPixel(x, y, image.GetPixel(px, py));
                PicFill4Centered(bmp, x + 1, y, px + 1, py, old);
                PicFill4Centered(bmp, x - 1, y, px - 1, py, old);
                PicFill4Centered(bmp, x, y + 1, px, py + 1, old);
                PicFill4Centered(bmp, x, y - 1, px, py - 1, old);
            }
            pictureBox1.Image = bmp;
        }

        void BorderTrav4(Bitmap bmp, int x, int y, Color fill, Color old)
        {
            if (x >= 0 && x < pictureBox1.Width && y >= 0 && y < pictureBox1.Height && bmp.GetPixel(x, y) == old && bmp.GetPixel(x, y) != fill)
            {
                bmp.SetPixel(x, y, fill);
                BorderTrav4(bmp, x + 1, y, fill, old);
                BorderTrav4(bmp, x - 1, y, fill, old);
                BorderTrav4(bmp, x, y + 1, fill, old);
                BorderTrav4(bmp, x, y - 1, fill, old);
            }
            pictureBox1.Image = bmp;
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
