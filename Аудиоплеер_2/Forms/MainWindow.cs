using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Аудиоплеер_2.Forms
{
    public partial class MainWindow : BaseWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            OGL.InitializeContexts();
            Thread.Sleep(4000);
            label3.TextAlign = ContentAlignment.MiddleRight;
            label2.TextAlign = ContentAlignment.MiddleLeft;
        }

        Brush _g = new SolidBrush(Color.DarkSlateGray);

        public float volume = 50;
        public float wind = 0;

        private void topPanel1_SizeChanged(object sender, EventArgs e)
        {
            OGL.Location = new Point(0, 50);
            OGL.Width = this.Width;
            OGL.Height = this.Height - 110;

            label1.Location = new Point(5, this.Height - 57);

            label3.Location = new Point(5, 32);
            label2.Location = new Point(this.Width - 70, 32);

            panel2.Location = new Point(75, 36);
            panel2.Height = 7;
            panel2.Width = this.Width - 150;

            panel1.Location = new Point(85, this.Height - 140);
            this.Invalidate();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (panel1.Visible)
            {
                volume = panel1.Height - e.Y;
                panel1.Invalidate();
            }
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(_g, 0, panel1.Height - volume, panel1.Width, volume);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(_g, 0, 0, wind, this.Height);
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            wind = e.X;
            panel2.Invalidate();
        }
    }
}
