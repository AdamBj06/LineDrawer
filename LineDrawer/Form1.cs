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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("valori mancanti", "Errore", MessageBoxButtons.OK);
                return;
            }

            if (!int.TryParse(textBox1.Text, out x1) || !int.TryParse(textBox2.Text, out y1) || !int.TryParse(textBox3.Text, out x2) || !int.TryParse(textBox4.Text, out y2))
            {
                MessageBox.Show("i valori non sono accettabili (devono essere numeri interi)", "Errore", MessageBoxButtons.OK);
                return;
            }
            else if(x2 <= x1 || y2 <= y1)
            {
                MessageBox.Show("la pendenza deve essere maggiore di 0\n(i pt inziali devono essere minori dei pt finali)", "Errore", MessageBoxButtons.OK);
                return;
            }
            else if (x2 < y2)
            {
                MessageBox.Show("la pendenza deve essere minore di 1\n(y finale deve essere minore di x finale)", "Errore", MessageBoxButtons.OK);
                return;
            }
            else if (x1 < 0 || y1 < 0 || x2 < 0 || y2 < 0)
            {
                MessageBox.Show("i punti inziali e finali devono essere positivi", "Errore", MessageBoxButtons.OK);
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

        private void button2_Click(object sender, EventArgs e)
        {
            g = CreateGraphics();
            g.Clear(Color.White);
            CreateGrid();

            textBox1.Text = "X iniziale";
            textBox2.Text = "Y iniziale";
            textBox3.Text = "X finale";
            textBox4.Text = "Y finale";
        }

        public void DrawPixel(int x, int y)
        {
            g = CreateGraphics();
            g.FillRectangle(Brushes.Black, 10 * x, 740 - 10 * y, 10, 10);
        }

        public void CreateGrid()
        {
            for (int i = 1; i < 80; i ++)
            {
                g.DrawLine(Pens.Black, i*10, 0, i*10, 800);
            }
            for (int i = 1; i < 80; i ++)
            {
                g.DrawLine(Pens.Black, 0, i * 10, 800, i*10);
            }
        }
    }
}