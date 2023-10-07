using System;
using System.Drawing;
using System.Windows.Forms;

namespace app_vertical_calender
{
    public partial class Form2 : Form
    {
        public bool IsOK;
        Point mousePoint;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IsOK = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IsOK = false;
            Close();
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                mousePoint = new Point(e.X, e.Y);
            }
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }
    }
}
