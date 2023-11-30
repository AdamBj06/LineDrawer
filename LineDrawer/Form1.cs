using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineDrawer
{
    public partial class Form1 : Form
    {
        Graphics g;
        public int x1;
        public int y1;
        public int x2;
        public int y2;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            g = CreateGraphics();
            CreateGrid();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out x1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox2.Text, out y1);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox3.Text, out x2);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox4.Text, out y2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("TextBox vuota");
                return;
            }
            else if(x2 <= x1 || y2 <= y1)
            {
                MessageBox.Show("i pt inziali devono essere più piccoli dei pt finali");
                return;
            }
            g = CreateGraphics();
            g.Clear(Color.White);
            CreateGrid();

            float Slope = (float)(y2 - y1) / (float)(x2 - x1);
            float Error = 0;

            DrawPixel(x1, y1);

            int y = y1;
            for (int x = x1 + 1; x <= x2; x++)
            {
                Error += Slope;

                if (Error >= 0.5)
                {
                    y = y + 1;
                    Error = Error - 1;
                }

                DrawPixel(x, y);
            }
        }

        public void DrawPixel(int x, int y)
        {
            g = CreateGraphics();
            g.FillRectangle(Brushes.Black, 20 * x, 740 - 20 * y, 20, 20);
        }

        public void CreateGrid()
        {
            for (int i = 20; i < 800; i += 20)
            {
                g.DrawLine(Pens.Black, i, 0, i, 800);
            }
            for (int i = 20; i < 800; i += 20)
            {
                g.DrawLine(Pens.Black, 0, i, 800, i);
            }
        }
    }
}
