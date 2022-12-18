using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        Graphics g;
        SolidBrush b1 = new SolidBrush(Color.Black);   //primary color
        SolidBrush b2 = new SolidBrush(Color.White);   //secondary color
        SolidBrush rubber = new SolidBrush(Color.White); //rubber always white

        Pen p1 = new Pen(Color.Black);
        Pen prubber = new Pen(Color.White);

        //Prim currentPrim;

        List<Point> plist = new List<Point>();

        private int? x0, y0;
        private int? x1, y1;
        private int? xb, yb;

        public void setPosition(int x, int y, Color color)
        {

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
                drawWuLine2(x0.GetValueOrDefault(), y0.GetValueOrDefault(), x1.GetValueOrDefault(), y1.GetValueOrDefault(), color);
                //rotatePixels(90);
                //pointDirection(x0.GetValueOrDefault(), y0.GetValueOrDefault(),x1.GetValueOrDefault(),y1.GetValueOrDefault(), xb.GetValueOrDefault(), yb.GetValueOrDefault());
                return;
            }
            x0 = x;
            y0 = y;
            x1 = null;
            y1 = null;
        }

        void drawPixel(int x, int y, double c, Color color)
        {
            Brush brush = new SolidBrush(Color.FromArgb((int)c, color));
            g.FillRectangle(brush, x, y, 1, 1);
        }

        void rotatePolygon(double alpha)
        {
            double angleRadian = alpha * Math.PI / 180;
            PointF s = new PointF((float)xb, (float)yb);
            PointF[] r = new PointF[plist.Count()];

            for (int j = 0; j < plist.Count; j++)
            {
                float x = (float)((plist[j].X - s.X) * Math.Cos(angleRadian) - (plist[j].Y - s.Y) * Math.Sin(angleRadian) + s.X);
                float y = (float)((plist[j].X - s.X) * Math.Sin(angleRadian) + (plist[j].Y - s.Y) * Math.Cos(angleRadian) + s.Y);
                r[j] = new PointF(x, y);
            }
            //Рисуем повернутый объект
            g.DrawPolygon(Pens.Black, r);

        }
        void resizePolygon(double percent)
        {
            double per = percent / 100.0d;
            PointF s = new PointF((float)xb, (float)yb);
            PointF[] r = new PointF[plist.Count()];
            for (int j = 0; j < plist.Count; j++)
            {
                float x = plist[j].X + Math.Abs(((float)xb - plist[j].X) * (float)per);
                float y = plist[j].Y + Math.Abs(((float)yb - plist[j].Y) * (float)per);
                r[j] = new PointF(x, y);
            }
            g.DrawPolygon(Pens.Black, r);
        }

        void rotatePixels(double alpha)
        {
            var radians = alpha * Math.PI / 180;

            var mid_x = (x0 + x1) / 2;

            var mid_y = (y0 + y1) / 2;

            var a_mid_x = x0 - mid_x;
            var a_mid_y = y0 - mid_y;

            var b_mid_x = x1 - mid_x;
            var b_mid_y = y1 - mid_y;

            var a_rotated_x =
        Math.Cos(radians) * a_mid_x - Math.Sin(radians) * a_mid_y;
            var a_rotated_y =
        Math.Sin(radians) * a_mid_x + Math.Cos(radians) * a_mid_y;

            var b_rotated_x =
        Math.Cos(radians) * b_mid_x - Math.Sin(radians) * b_mid_y;
            var b_rotated_y =
        Math.Sin(radians) * b_mid_x + Math.Cos(radians) * b_mid_y;

            a_rotated_x += mid_x;
            a_rotated_y += mid_y;
            b_rotated_x += mid_x;
            b_rotated_y += mid_y;

            drawWuLine2(int.Parse(Math.Floor(a_rotated_x.GetValueOrDefault()).ToString()), int.Parse(Math.Floor(a_rotated_y.GetValueOrDefault()).ToString()), int.Parse(Math.Floor(b_rotated_x.GetValueOrDefault()).ToString()), int.Parse(Math.Floor(b_rotated_y.GetValueOrDefault()).ToString()), Color.Red);
        }

        void pointDirection(int beginX0, int beginY0, int beginX1, int beginY1, int pointX, int pointY)
        {
            beginX1 -= beginX0;
            beginY1 -= beginY0;
            pointX -= beginX0;
            pointY -= beginY0;

            int cond = beginX1 * pointY - beginY1 * pointX;
            if (cond > 0)
            {
                textBox4.Text = "Point is on the right";
            }
            else if (cond < 0)
            {
                textBox4.Text = "Point is on the left";
            }
        }

        void drawWuLine2(int x0, int y0, int x1, int y1, Color color)
        {

            g.FillEllipse(b1, x0 - 2, y0 - 2, 5, 5);


            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                (x0, y0) = (y0, x0);
                (x1, y1) = (y1, x1);
            }
            if (x0 > x1)
            {
                (x0, x1) = (x1, x0);
                (y0, y1) = (y1, y0);
            }

            double dx = x1 - x0;
            double dy = y1 - y0;
            double gradient = (double)((double)(dy) / (double)(dx));
            if (dx == 0.0)
            {
                gradient = 1;
            }

            int xpxl1 = x0;
            int xpxl2 = x1;
            double intery = y0;

            if (steep)
            {
                for (int x = int.Parse((xpxl1).ToString()); x <= int.Parse((xpxl2).ToString()); x++)
                {
                    drawPixel(int.Parse(iPart(intery).ToString()), int.Parse(x.ToString()), rfPart(intery) * 255, color);
                    drawPixel(int.Parse(iPart(intery).ToString()) + 1, int.Parse(x.ToString()), fPart(intery) * 255, color);
                    intery += gradient;
                }
            }
            else
            {
                for (int x = int.Parse((xpxl1).ToString()); x <= int.Parse((xpxl2).ToString()); x++)
                {
                    drawPixel(int.Parse(x.ToString()), int.Parse(iPart(intery).ToString()), rfPart(intery) * 255, color);
                    drawPixel(int.Parse(x.ToString()), int.Parse(iPart(intery).ToString()) + 1, fPart(intery) * 255, color);
                    intery += gradient;
                }
            }
        }

        int iPart(double d)
        {
            return (int)d;
        }

        int round(double d)
        {
            return (int)(d + 0.50000);
        }

        double fPart(double d)
        {
            return (double)(d - (int)(d));
        }

        double rfPart(double d)
        {
            return (double)(1.00000 - (double)(d - (int)(d)));
        }

        /*
        enum primitiveType
        {
            point, segment, polygon
        }

        struct Prim {
            public Prim(primitiveType t, List<Point> p)
            {
                type = t;
                points = p;
            }

            primitiveType type {get;}
            List<Point> points { get; }
            //int[][] matrix;
        }
        */

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                b1.Color = cd.Color;
                panel3.BackColor = cd.Color;
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                b2.Color = cd.Color;
                panel4.BackColor = cd.Color;
            }
        }

        private void disableRadios()
        {
            radioPoint.Enabled = false;
            radioRectangle.Enabled = false;
            radioSegment.Enabled = false;
        }

        private void enableRadios()
        {
            radioPoint.Enabled = true;
            radioRectangle.Enabled = true;
            radioSegment.Enabled = true;
        }

        private void clearBoxes()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
        }

        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
        }

        //классификация положения точки относительно отрезка
        private void button3_Click(object sender, EventArgs e)
        {
            pointDirection(x0.Value, y0.Value, x1.Value, y1.Value, xb.Value, yb.Value);
        }
        //вращение
        private void button4_Click(object sender, EventArgs e)
        {
            if(radioSegment.Checked)
            rotatePixels(Convert.ToDouble(textBox5.Text));
            else
            if (radioRectangle.Checked )
            {

                rotatePolygon(Convert.ToDouble(textBox5.Text));
            }

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text != "" && !radioPoint.Checked)
            {
                button4.Enabled = true;   
            }
            else { button4.Enabled = false; }
        }

        //shift pos
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Point")
            {
                g.FillEllipse(rubber, xb.Value, yb.Value, 5, 5);
                xb = xb.Value + Convert.ToInt32(textBox2.Text);
                yb = yb.Value + Convert.ToInt32(textBox3.Text);
                g.FillEllipse(b1, xb.Value, yb.Value, 5, 5);
            }

            if (textBox1.Text == "Polygon")
            {
                List<Point> newplist = new List<Point>();

                for (int i = 0; i < plist.Count() - 1; i++)
                {
                    g.DrawLine(prubber, plist[i], plist[i + 1]);
                }
                g.DrawLine(prubber, plist.First(), plist.Last());

                Point temp;

                for (int i = 0; i < plist.Count(); i++)
                {
                    temp = plist[i];
                    temp.X += Convert.ToInt32(textBox2.Text);
                    temp.Y += Convert.ToInt32(textBox3.Text);
                    plist[i] = temp;
                }

                for (int i = 0; i < plist.Count() - 1; i++)
                {
                    g.DrawLine(p1, plist[i], plist[i + 1]);
                }
                g.DrawLine(p1, plist.First(), plist.Last());
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != ""/*&& xb != null && yb != null*/)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "" /*&& xb != null && yb != null*/)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool belongs = false;
            for (int i = 0; i < plist.Count()-1; i++)
            {
                if ((plist[i + 1].X - plist[i].X) * (yb - plist[i].Y) - (plist[i + 1].Y - plist[i].Y) * (xb - plist[i].X) >= 0) {
                    belongs = true;
                }
                else
                { 
                    belongs = false;
                    break;
                }
            }


            if ((plist.First().X - plist.Last().X) * (yb - plist.Last().Y) - (plist.First().Y - plist.Last().Y) * (xb - plist.Last().X) < 0)
            {
                belongs = false;
            }
            

            if (belongs)
            {
                textBox6.Text = "Point does belong";
            }
            else
            {
                textBox6.Text = "Point does NOT belong";
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            resizePolygon(Convert.ToDouble(textBox7.Text));
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                button6.Enabled = true;
            }
            else { button6.Enabled = false; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //TODO: POINT DRAWING
            if (radioPoint.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    g.FillEllipse(b1, e.X, e.Y, 5, 5);
                }
                else
                {
                    g.FillEllipse(b2, e.X, e.Y, 5, 5);
                }

                xb = e.X;
                yb = e.Y;
                if (x0 != null && y0 != null && x1 != null && y1 != null && xb != null && yb != null)
                {
                    button3.Enabled = true;
                }
                //disableRadios();
                //radioPoint.Checked = false;

                //panel5.Enabled = true;
                textBox1.Text = "Point";
            }

            //SEGMENT
            if (radioSegment.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    setPosition(e.X, e.Y, b1.Color);
                }
                else
                {
                    setPosition(e.X, e.Y, b2.Color);
                }
                textBox1.Text = "Segment";
                if (x0 != null && y0 != null && x1 != null && y1 != null && xb != null && yb != null)
                {
                    button3.Enabled = true;
                }
            }

            //POLYGON DRAWING
            if (radioRectangle.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Point newPoint = new Point(e.X, e.Y);
                    if (plist.Count != 0)
                    {
                        g.DrawLine(p1, plist.Last(), newPoint);
                    }
                    plist.Add(newPoint);
                }
                if (e.Button == MouseButtons.Right)
                {
                    if (plist.Count != 0)
                    {
                        Point newPoint = new Point(e.X, e.Y);
                        g.DrawLine(p1, plist.Last(), newPoint);
                        g.DrawLine(p1, plist.First(), newPoint);
                        plist.Add(newPoint);
                    }

                    //disableRadios();
                    //radioRectangle.Checked = false;

                    //panel5.Enabled = true;
                    textBox1.Text = "Polygon";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(panel1.BackColor);
            enableRadios();
            radioPoint.Checked = true;
            //panel5.Enabled = false;

            clearBoxes();
            button3.Enabled = false;

            plist.Clear();

            (x0, y0, x1, y1, xb, yb) = (null, null, null, null, null, null);
        }
    }
}
