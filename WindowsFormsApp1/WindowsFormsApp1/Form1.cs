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
        Timer t = new Timer();

        int Width = 300, Height = 300, secondHand = 140, minuteHand = 110, hourHand = 80;

        int x, y;        

        Bitmap btm1, btm2;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btm1 = new Bitmap(Width + 1, Height + 1);
            btm2 = new Bitmap(Width, Height);
            x = Width / 2;
            y = Height / 2;
            t.Interval = 1000;
            t.Tick += new EventHandler(this.timer1_Tick);
            t.Start();
            g = this.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            
           
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(btm2);


            int seconds = DateTime.Now.Second;
            int minutes = DateTime.Now.Minute;
            int hours = DateTime.Now.Hour;

            int[] handCoordinate = new int[2];
            g.Clear(Color.White);

            g.DrawEllipse(new Pen(Color.Black, 1f), 0, 0, Width, Height);
            g.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(140, 2));
            g.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(286, 140));
            g.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(142, 282));
            g.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, 140));

            handCoordinate = minutesCoordinate(seconds, secondHand);
            g.DrawLine(new Pen(Color.Red, 1f), new Point(x, y), new Point(handCoordinate[0], handCoordinate[1]));


            handCoordinate = minutesCoordinate(minutes, minuteHand);
            g.DrawLine(new Pen(Color.Black, 2f), new Point(x, y), new Point(handCoordinate[0], handCoordinate[1]));


            handCoordinate = hourCoordinate(hours % 12, minutes, hourHand);
            g.DrawLine(new Pen(Color.Gray, 3f), new Point(x, y), new Point(handCoordinate[0], handCoordinate[1]));


            pictureBox1.Image = btm2;


            this.Text = "Analog Clock -  " + hours + ":" + minutes + ":" + seconds;


            g.Dispose();


        }
        private int[] minutesCoordinate(int val, int hlen)
        {
            int[] coordinate = new int[2];
            val *= 6;

            if (val >= 0 && val <= 180)
            {
                coordinate[0] = x + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coordinate[1] = y - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coordinate[0] = x - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coordinate[1] = y - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coordinate;
        }


        private int[] hourCoordinate(int hval, int mval, int hlen)
        {
            int[] coordinate = new int[2];


            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 180)
            {
                coordinate[0] = x + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coordinate[1] = y - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coordinate[0] = x - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coordinate[1] = y - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coordinate;
        }
    }
}
