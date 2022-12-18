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
        Graphics g;

        double negX = 1.0;
        double posX = 10.0;

        double acc = 0.001;


        double fmin = double.MaxValue;
        double fmax = double.MinValue;

        void drawL()
        {
            Pen l = new Pen(Color.Black);

            for (int x = 0; x < 800; x++)
            {
                g.DrawEllipse(l, x, 200, 1, 1);
            }
            for (int y = 0; y < 400; y++)
            {
                g.DrawEllipse(l, 400, y, 1, 1);
            }
        }

        void fExt(Func<double, double> func) 
        {
            for (var x = negX; x <= posX; x += acc)
            {
                fmin = Math.Min(func(x), fmin);
            }
            for (var x = negX; x <= posX; x += acc)
            {
                fmax = Math.Max(func(x), fmax);
            }
        }

        void drawF(Func<double, double> f)
        {
            fExt(f);
            Pen p = new Pen(Color.Red);
            var w = this.ClientSize.Width;
            var h = this.ClientSize.Height;


            for (double i = negX; i <= posX; i += acc)
            {
                float x = (float)((i - negX) / (posX - negX)) * w;
                float y = (float)(this.ClientSize.Height - (((f(i) - fmin) / (fmax - fmin)) * h));
                
                g.DrawEllipse(p, x, y, 1, 1);
            }


            p.Dispose();
        }

        public Form1()
        {
            this.ClientSize = new System.Drawing.Size(800, 400);
            g = this.CreateGraphics();
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //drawL();
            drawF(x => x*x);
        }
    }
}
