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
    public partial class Form1 : Form
    {
        
        PointF a, b, c;
        Color c1, c2, c3;
        

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                c1 = Color.Red;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                c1 = Color.Orange;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                c1 = Color.Yellow;
            }
            if (comboBox1.SelectedIndex == 3)
            {
                c1 = Color.Green;
            }
            if (comboBox1.SelectedIndex == 4)
            {
                c1 = Color.Blue;
            }
            if (comboBox1.SelectedIndex == 5)
            {
                c1 = Color.Indigo;
            }
            if (comboBox1.SelectedIndex == 6)
            {
                c1 = Color.Violet;
            }
            if (c1 == c2 || c2 == c3 || c3 == c1 || c1.IsEmpty || c2.IsEmpty || c3.IsEmpty)
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                c2 = Color.Red;
            }
            if (comboBox2.SelectedIndex == 1)
            {
                c2 = Color.Orange;
            }
            if (comboBox2.SelectedIndex == 2)
            {
                c2 = Color.Yellow;
            }
            if (comboBox2.SelectedIndex == 3)
            {
                c2 = Color.Green;
            }
            if (comboBox2.SelectedIndex == 4)
            {
                c2 = Color.Blue;
            }
            if (comboBox2.SelectedIndex == 5)
            {
                c2 = Color.Indigo;
            }
            if (comboBox2.SelectedIndex == 6)
            {
                c2 = Color.Violet;
            }
            if (c1 == c2 || c2 == c3 || c3 == c1 || c1.IsEmpty || c2.IsEmpty || c3.IsEmpty)
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                c3 = Color.Red;
            }
            if (comboBox3.SelectedIndex == 1)
            {
                c3 = Color.Orange;
            }
            if (comboBox3.SelectedIndex == 2)
            {
                c3 = Color.Yellow;
            }
            if (comboBox3.SelectedIndex == 3)
            {
                c3 = Color.Green;
            }
            if (comboBox3.SelectedIndex == 4)
            {
                c3 = Color.Blue;
            }
            if (comboBox3.SelectedIndex == 5)
            {
                c3 = Color.Indigo;
            }
            if (comboBox3.SelectedIndex == 6)
            {
                c3 = Color.Violet;
            }
            //comboBox1.SelectedIndex == comboBox2.SelectedIndex || comboBox1.SelectedIndex == comboBox3.SelectedIndex || comboBox3.SelectedIndex == comboBox2.SelectedIndex
            if (c1 == c2 || c2 == c3 || c3 == c1 || c1.IsEmpty || c2.IsEmpty || c3.IsEmpty )
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }
        
       private Graphics g { get; set; }

        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            Random rnd = new Random();
            // Bitmap bt = new Bitmap(pictureBox1.Width - 1, pictureBox1.Height - 1);
            //Graphics g = Graphics.FromImage(bt);

            int x1 = rnd.Next(100, 300);
            int y1 = rnd.Next(100, 240);

            int y2 = rnd.Next(100, 240);


            a = new PointF(x: x1, y: y1 - 50);
            b = new PointF(x: x1 + 50, y: y2  );
            c = new PointF(x: x1 - 50, y: y2  );

            PointF[] points = { a, b, c };

            //g.DrawPolygon(Pens.Black, points);

            Gradient();
            
            //pictureBox1.Image = bt;

        }

        //Находим описывающий прямоугольник
        public (PointF,PointF) R()
        {
            var minP = new PointF(Math.Min(a.X, Math.Min(b.X, c.X)), Math.Min(a.Y, Math.Min(b.Y, c.Y)));
            var maxP = new PointF(Math.Max(a.X, Math.Max(b.X, c.X)), Math.Max(a.Y, Math.Max(b.Y, c.Y)));
            return (minP, maxP);
        }
        //Проверяем находится ли точка внутри треугольника
        public bool InTriangle(float x, float y)
        {
            float aSide = (a.Y - b.Y) * x + (b.X - a.X) * y + (a.X * b.Y - b.X * a.Y);
            float bSide = (b.Y - c.Y) * x + (c.X - b.X) * y + (b.X * c.Y - c.X * b.Y);
            float cSide = (c.Y - a.Y) * x + (a.X - c.X) * y + (c.X * a.Y - a.X * c.Y);
            return (aSide >= 0 && bSide >= 0 && cSide >= 0) || (aSide < 0 && bSide < 0 && cSide < 0);
        }

       

        
        //Процедура окраски в градиентный цвет
        public void Gradient()
        {
            Bitmap image = new Bitmap(pictureBox1.Size.Width - 1, pictureBox1.Size.Height - 1);


             
            for (int y = 0; y<= R().Item2.Y; y++)
            {
                for (int x = 0; x <= R().Item2.X; x++)
                {
                    float w1 = (y * b.X - x * b.Y) / (float)(a.Y * b.X - a.X * b.Y);
                    if (w1>=0 && w1<=1)
                    {
                        float w2 = (y - w1 * a.Y) / (float)(b.Y);
                        if (w2 >= 0 && (w1 + w2) <= 1)
                        {
                            var resR =c1.R +  (c2.R - c1.R)* w1 + (c3.R - c1.R) * w2;
                            var resG =c1.G +  (c2.G - c1.G) * w1 + (c3.G - c1.G) * w2;
                            var resB =c1.B + (c2.B - c1.B) * w1 + (c3.B - c1.B) * w2;

                            image.SetPixel(x, y, Color.FromArgb(Convert.ToInt32(resR), Convert.ToInt32(resG), Convert.ToInt32(resB)));
                        }
                    }

                }
            }
              pictureBox1.Image = image;
        }


        private void onCheckedChanged(object sender, EventArgs e)
        {
            lineDrawerIsChecked = checkBoxLineDrawer.Checked;
        }

        private void onMouseClick(object sender, MouseEventArgs e)
        {
            setPosition(e.X, e.Y);
        }




        private int? x0, y0;
        private int? x1, y1;


        private bool lineDrawerIsChecked;

        Brush aBrush = (Brush)Brushes.Black;


        public void setPosition(int x, int y)
        {
            if (!lineDrawerIsChecked)
                return;

            if (x0 == null || y0 == null)
            {
                x0 = x;
                y0 = y;
                return;
            }
            if (x1 == null || y1 == null)
            {
                x1 = x;
                y1 = y;
                drawLine(x0.GetValueOrDefault(), y0.GetValueOrDefault(), x1.GetValueOrDefault(), y1.GetValueOrDefault());
                return;
            }
            x0 = x;
            y0 = y;
            x1 = null;
            y1 = null;
        }

        void drawLineUpper(int x0, int y0, int x1, int y1)
        {
            var deltax = x1 - x0;
            var deltay = y1 - y0;

            var dir = 1;

            if (deltax < 0)
            {
                dir = -1;
                deltax = -deltax;
            }

            var delta = (2 * deltax) - deltay;

            var x = x0;

            for (int y = y0; y <= y1; y++)
            {

                g.FillRectangle(aBrush, x, y, 1, 1);
                

                if (delta > 0)
                {
                    x += dir;
                    delta += (2 * (deltax - deltay));
                }
                else
                {
                    delta += 2 * deltax;
                }

            }
        }

        void drawLineLower(int x0, int y0, int x1, int y1)
        {
            var deltax = x1 - x0;
            var deltay = y1 - y0;

            var dir = 1;

            if (deltay < 0)
            {

                dir = -1;
                deltay = -deltay;

            }
            var delta = (2 * deltay) - deltax;

            var y = y0;

            for (int x = x0; x <= x1; x++)
            {
                g.FillRectangle(aBrush, x, y, 1, 1);

                if (delta > 0)
                {
                    y += dir;
                    delta += (2 * (deltay - deltax));
                }
                else
                {
                    delta += 2 * deltay;
                }
            }
        }

        ///Bresenham's line algorithm
        private void drawLine(int x0, int y0, int x1, int y1)
        {
            g = this.CreateGraphics();
            if (Math.Abs(y1 - y0) < Math.Abs(x1 - x0))
            {
                if (x0 > x1)
                    drawLineLower(x1, y1, x0, y0);
                else
                    drawLineLower(x0, y0, x1, y1);
            }
            else
            {
                if (y0 > y1)
                    drawLineUpper(x1, y1, x0, y0);
                else
                    drawLineUpper(x0, y0, x1, y1);
            }
        }
    }


}
