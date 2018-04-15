using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Cellular_Automaton
{
    public partial class Form1 : Form
    {
        private int length = 0;
        private int height = 0;
        private double u = 10;
        private Graphics g;
        private int[,] cell1;
        private int[,] cell2;
        private int time = 1;
        public Form1()
        {
            InitializeComponent();
            textBox_x.Enabled = false;
            textBox_y.Enabled = false;
            button_add.Enabled = false;
            button_start.Enabled = false;
            g = pictureBox1.CreateGraphics();
            t = new Thread(new ThreadStart(ThreadProc));
            t.IsBackground = true;
            pictureBox1.Enabled = false;
            button_stop.Enabled = false;
        }

        private void Button_set_Click(object sender, EventArgs e)
        {
            if(textBox_length.Text != null && textBox_height.Text != null)
            {
                if(int.TryParse(textBox_length.Text, out length) == true && int.TryParse(textBox_height.Text, out height) == true
                    && length <= Size.Width / u && height <= Size.Height / u)
                {
                    textBox_length.Enabled = false;
                    textBox_height.Enabled = false;
                    textBox_x.Enabled = true;
                    textBox_y.Enabled = true;
                    button_add.Enabled = true;
                    button_start.Enabled = true;
                    cell1 = new int[length, height];
                    cell2 = new int[length, height];
                    pictureBox1.Enabled = true;
                    button_set.Enabled = false;
                    for(int i = 0; i < length; i++)
                    {
                        for(int j = 0; j < height; j++)
                        {
                            repaint(i, j);
                        }
                    }
                }
                else
                {
                    textBox_length.Text = "";
                    textBox_height.Text = "";
                }
            }
            else
            {
                textBox_length.Text = "";
                textBox_height.Text = "";
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            if (textBox_x.Text != null && textBox_y.Text != null)
            {
                if (int.TryParse(textBox_x.Text, out x) == true && int.TryParse(textBox_y.Text, out y) == true)
                {
                    if(x <= 0 || y <= 0 || x > length || y > length)
                    {
                        textBox_x.Text = "";
                        textBox_y.Text = "";
                        return;
                    }
                    cell1[x-1, y-1] = 1;
                    paint(x-1, y-1);
                }
            }
        }
        public static Thread t;
        private void button_start_Click(object sender, EventArgs e)
        {
            textBox_x.Enabled = false;
            textBox_y.Enabled = false;
            button_add.Enabled = false;
            
            try
            {
                t.Start();
                Console.WriteLine(t.ThreadState.ToString());
            }
            catch
            {
#pragma warning disable CS0618 // 类型或成员已过时
                t.Resume();
#pragma warning restore CS0618 // 类型或成员已过时
                Console.WriteLine(t.ThreadState.ToString());
            }
            button_stop.Enabled = true;
        }

        private void ThreadProc()
        {
            while (true)
            {
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (time == 1)
                        {
                            int count = 0;
                            if (i + 1 < length && j + 1 < height)
                            {
                                if (cell1[i + 1, j + 1] == 1)
                                {
                                    count++;
                                }
                            }
                            if (i + 1 < length)
                            {
                                if (cell1[i + 1, j] == 1)
                                {
                                    count++;
                                }
                            }
                            if (i + 1 < length && j - 1 > 0)
                            {
                                if (cell1[i + 1, j - 1] == 1)
                                {
                                    count++;
                                }
                            }
                            if (j - 1 > 0)
                            {
                                if (cell1[i, j - 1] == 1)
                                {
                                    count++;
                                }
                            }
                            if (i - 1 > 0 && j - 1 > 0)
                            {
                                if (cell1[i - 1, j - 1] == 1)
                                {
                                    count++;
                                }
                            }
                            if (i - 1 > 0)
                            {
                                if (cell1[i - 1, j] == 1)
                                {
                                    count++;
                                }
                            }
                            if (i - 1 > 0 && j + 1 < height)
                            {
                                if (cell1[i - 1, j + 1] == 1)
                                {
                                    count++;
                                }
                            }
                            if (j + 1 < height)
                            {
                                if (cell1[i, j + 1] == 1)
                                {
                                    count++;
                                }
                            }
                            if (cell1[i, j] == 1)
                            {
                                if (count == 2 || count == 3)
                                {
                                    cell2[i, j] = 1;
                                }
                                else
                                {
                                    cell2[i, j] = 0;
                                }
                            }
                            else
                            {
                                if (count == 3)
                                {
                                    cell2[i, j] = 1;
                                }
                                else
                                {
                                    cell2[i, j] = 0;
                                }
                            }
                        }
                        if (time == 2)
                        {
                            int count = 0;
                            if (i + 1 < length && j + 1 < height)
                            {
                                if (cell2[i + 1, j + 1] == 1)
                                {
                                    count++;
                                }
                            }
                            if (i + 1 < length)
                            {
                                if (cell2[i + 1, j] == 1)
                                {
                                    count++;
                                }
                            }
                            if (i + 1 < length && j - 1 > 0)
                            {
                                if (cell2[i + 1, j - 1] == 1)
                                {
                                    count++;
                                }
                            }
                            if (j - 1 > 0)
                            {
                                if (cell2[i, j - 1] == 1)
                                {
                                    count++;
                                }
                            }
                            if (i - 1 > 0 && j - 1 > 0)
                            {
                                if (cell2[i - 1, j - 1] == 1)
                                {
                                    count++;
                                }
                            }
                            if (i - 1 > 0)
                            {
                                if (cell2[i - 1, j] == 1)
                                {
                                    count++;
                                }
                            }
                            if (i - 1 > 0 && j + 1 < height)
                            {
                                if (cell2[i - 1, j + 1] == 1)
                                {
                                    count++;
                                }
                            }
                            if (j + 1 < height)
                            {
                                if (cell2[i, j + 1] == 1)
                                {
                                    count++;
                                }
                            }
                            if (cell2[i, j] == 1)
                            {
                                if (count == 2 || count == 3)
                                {
                                    cell1[i, j] = 1;
                                }
                                else
                                {
                                    cell1[i, j] = 0;
                                }
                            }
                            else
                            {
                                if (count == 3)
                                {
                                    cell1[i, j] = 1;
                                }
                                else
                                {
                                    cell1[i, j] = 0;
                                }
                            }
                        }
                    }
                }
                if (time == 1)
                {
                    paint1();
                    time = 2;
                }
                else
                {
                    paint2();
                    time = 1;
                }
                Thread.Sleep(1000);
            }
        }

        private void paint2()
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (cell2[i, j] == 1)
                    {
                        paint(i, j);
                    }
                    else
                    {
                        repaint(i, j);
                    }
                }
            }
        }

        private void paint1()
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (cell1[i, j] == 1)
                    {
                        paint(i, j);
                    }
                    else
                    {
                        repaint(i, j);
                    }
                }
            }
        }

        private void paint(int x, int y)
        {
            Rectangle rect = new Rectangle((int)(u * x), (int)(u * y), (int)u, (int)u);
            SolidBrush b1 = new SolidBrush(Color.Red);       
            g.FillRectangle(b1, rect);
        }
        private void repaint(int x, int y)
        {
            Rectangle rect = new Rectangle((int)(u * x), (int)(u * y), (int)u, (int)u);
            SolidBrush b1 = new SolidBrush(Color.White);
            g.FillRectangle(b1, rect);
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
#pragma warning disable CS0618 // 类型或成员已过时
            t.Suspend();
#pragma warning restore CS0618 // 类型或成员已过时
            textBox_x.Enabled = true;
            textBox_y.Enabled = true;
            button_add.Enabled = true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            x = x / (int)u;
            y = y / (int)u;
            if(x >= length || y >= height)
            {
                return;
            }
            if (time == 1)
            {
                cell1[x, y] = 1;
                paint(x, y);
            }
            else
            {
                cell2[x, y] = 1;
                paint(x, y);
            }
        }

        private void button_resetup_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
            Application.Restart();
            Process.GetCurrentProcess().Kill();
        }
    }
}
