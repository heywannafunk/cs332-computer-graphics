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
    public partial class Form222 : Form
    {
        Color p = Color.Black;  //pen
        Color b = Color.Red;    //border
        Bitmap canvas;
        Bitmap image;
        List<Point> border;

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
                bmp.SetPixel(x, y, image.GetPixel(x,y));
                PicFill4(bmp, x + 1, y, image.GetPixel( x + 1, y), old);
                PicFill4(bmp, x - 1, y, image.GetPixel( x - 1, y), old);
                PicFill4(bmp, x, y + 1, image.GetPixel( x, y + 1), old);
                PicFill4(bmp, x, y - 1, image.GetPixel( x, y - 1), old);
            }
            pictureBox1.Image = bmp;
        }

        void PicFill4Centered(Bitmap bmp, int x, int y, int px, int py, Color old)
        {
            if (x >= 0 && x < pictureBox1.Width && y >= 0 && y < pictureBox1.Height && bmp.GetPixel(x, y) == old && bmp.GetPixel(x, y) != image.GetPixel(x,y))
            {
                bmp.SetPixel(x, y, image.GetPixel(px, py));
                PicFill4Centered(bmp, x + 1, y, px + 1, py, old);
                PicFill4Centered(bmp, x - 1, y, px - 1, py, old);
                PicFill4Centered(bmp, x, y + 1, px, py + 1, old);
                PicFill4Centered(bmp, x, y - 1, px, py - 1, old);
            }
            pictureBox1.Image = bmp;
        }

        void BorderTraverse4(Bitmap bmp, int x, int y, Color fill, Color old)
        {
            if (x >= 0 && x < pictureBox1.Width && y >= 0 && y < pictureBox1.Height && bmp.GetPixel(x, y) == old && bmp.GetPixel(x, y) != fill)
            {
                bmp.SetPixel(x, y, fill);
                BorderTraverse4(bmp, x + 1, y, fill, old);
                BorderTraverse4(bmp, x - 1, y, fill, old);
                BorderTraverse4(bmp, x, y + 1, fill, old);
                BorderTraverse4(bmp, x, y - 1, fill, old);
            }
            pictureBox1.Image = bmp;
        }

        public Form222()
        {
            InitializeComponent();
        }

        private void Form222_Load(object sender, EventArgs e)
        {
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = canvas;
            for (int x = 0; x < canvas.Width; x++)
            {
                canvas.SetPixel(x, 0, Color.Black);
                canvas.SetPixel(x, canvas.Height-1, Color.Black);
            }
            for (int y = 0; y < canvas.Height; y++)
            {
                canvas.SetPixel(0,y,Color.Black);
                canvas.SetPixel(canvas.Width-1, y, Color.Black);
            }
            panel2.BackColor = p;
            panel3.BackColor = b;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton2.Checked) {
                if (e.Button == MouseButtons.Left) {
                    FloodFill4(canvas, e.X, e.Y, p, canvas.GetPixel(e.X,e.Y));
                }
            }
            if (radioButton3.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (e.X < image.Width && e.Y < image.Height)
                    {
                        PicFill4(canvas, e.X, e.Y, image.GetPixel(e.X, e.Y), canvas.GetPixel(e.X, e.Y));
                    }
                    else {
                        MessageBox.Show("You are too far from the coordinates origin!");
                    }
                }
            }
            if (radioButton4.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    PicFill4Centered(canvas, e.X, e.Y, image.Width/2, image.Height/2, canvas.GetPixel(e.X, e.Y));
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    canvas.SetPixel(e.X, e.Y, p);
                    pictureBox1.Image = canvas;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                p = cd.Color;
                panel2.BackColor = cd.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files|*.png| All files(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    image = new Bitmap(dialog.FileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                b = cd.Color;
                panel3.BackColor = cd.Color;
            }
        }
    }
}
