using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CGLab5
{
    public partial class MidpointForm : Form
    {
        private Graphics g;

        public MidpointForm()
        {
            InitializeComponent();
            g = CreateGraphics();
        }

        private double R;
        private int x0, y0, x1, y1;

        private void MidpointForm_Shown(object sender, EventArgs e)
        {

            x0 = 0;
            y0 = int.Parse(Math.Floor(this.Height * 0.6).ToString());
            x1 = this.Width;
            y1 = int.Parse(Math.Floor(this.Height * 0.6).ToString());
            R = double.Parse(roughnessBox.Text);
        }

        private void midpointDispacement(int x0,int y0, int x1, int y1)
        {
            double l = Math.Sqrt(Math.Pow(Math.Abs(x1 - x0), 2) + Math.Pow(Math.Abs(y1 - y0), 2));
   
            
            int midX = (x0 + x1) / 2;
            int midY = (y0 + y1) / 2;

            double randomShift = GetRandomNumber(-R * l, R * l);

            midY = int.Parse(Math.Floor(midY - randomShift).ToString());
            
            if (Math.Abs(x1 - x0) <= 1)
            {
                drawWuLine(x0, y0, midX, midY,Color.Red);
                drawWuLine(midX, midY, x1, y1,Color.Red);
                return;
            } else
            {
                drawWuLine(x0, y0, midX, midY, Color.Black);
                drawWuLine(midX, midY, x1, y1, Color.Black);
            }
            

            midpointDispacement(x0, y0, midX, midY);
            midpointDispacement(midX, midY, x1, y1);
        }

        private double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private void drawWuLine(int x0, int y0, int x1, int y1, Color color)
        {

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

        private void drawPixel(int x, int y, double c, Color color)
        {
            
            Brush brush = new SolidBrush(Color.FromArgb(Math.Abs((int)c%256), color));
            g.FillRectangle(brush, x, y, 1, 1);
        }

        private int iPart(double d)
        {
            return (int)d;
        }

        private int round(double d)
        {
            return (int)(d + 0.50000);
        }

        private double fPart(double d)
        {
            return (double)(d - (int)(d));
        }

        private double rfPart(double d)
        {
            return (double)(1.00000 - (double)(d - (int)(d)));
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            midpointDispacement(x0, y0, x1, y1);
        }

        private void roughnessBox_TextChanged(object sender, EventArgs e)
        {
           if (!double.TryParse(roughnessBox.Text,out R))
            {
                roughnessBox.Text = "0,0";
            }
        }
        
    }
}
