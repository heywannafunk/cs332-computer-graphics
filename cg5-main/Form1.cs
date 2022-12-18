using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGLab5
{
    public partial class Form1 : Form
    {
        private LsystemsForm lSystemsForm;
        private MidpointForm midpointForm;
        private BezierForm bezierForm;
        public Form1()
        {
            InitializeComponent();
        }

        private void lSystemsButton_Click(object sender, EventArgs e)
        {
            lSystemsForm = new LsystemsForm();
            lSystemsForm.ShowDialog();
        }

        private void midpointButton_Click(object sender, EventArgs e)
        {
            midpointForm = new MidpointForm();
            midpointForm.ShowDialog();
        }

        private void bezierButton_Click(object sender, EventArgs e)
        {
            bezierForm = new BezierForm();
            bezierForm.ShowDialog();
        }
    }
}
