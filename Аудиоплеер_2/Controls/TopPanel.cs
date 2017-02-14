using System;
using System.Drawing;
using System.Windows.Forms;
using Аудиоплеер_2.Interface;
using ButtonBase = Аудиоплеер_2.Classes.ButtonBase;

namespace Аудиоплеер_2.Controls
{
    public partial class TopPanel : UserControl, IButtonParent
    {
        public ButtonBase Min;
        public ButtonBase Max;
        public ButtonBase Close;
        public bool TrueMax = true, TrueMin = true, TrueClose = true;

        private int moveX, moveY;
        private bool mouseDown = false;
        private int xMax = 40, xMin = 60, xClose = 20;

        public TopPanel()
        {
            InitializeComponent();
            Close = new ButtonBase(new Point(this.Width - xClose, 8),
                15, 15,
                Аудиоплеер_2.Properties.Resources.close,
                Аудиоплеер_2.Properties.Resources.closeM,
                null,
                this);
            Max = new ButtonBase(new Point(this.Width - xMax, 8),
                15, 15,
                Аудиоплеер_2.Properties.Resources.max,
                Аудиоплеер_2.Properties.Resources.MaxM,
                null,
                this);
            Min = new ButtonBase(new Point(this.Width - xMin, 18),
                15, 5,
                Аудиоплеер_2.Properties.Resources.min,
                Аудиоплеер_2.Properties.Resources.minM,
                null,
                this);
            Close.Click += new EventHandler(close_Click);
            Min.Click += new EventHandler(min_Click);
            Max.Click += new EventHandler(max_Click);
        }

        public void OffMax(){
            TrueMax = false;
            Max.Work = false;
        }

        public void OffMin()
        {
            TrueMin = false;
            Min.Work = false;
        }

        public void OffClose()
        {
            TrueClose = false;
            Close.Work = false;
        }


        private void max_Click(object sender, EventArgs e)
        {
            if (TrueMax)
            {
                if (this.Parent.FindForm().WindowState == FormWindowState.Normal)
                    this.Parent.FindForm().WindowState = FormWindowState.Maximized;
                else this.Parent.FindForm().WindowState = FormWindowState.Normal;
            }
        }

        private void min_Click(object sender, EventArgs e)
        {
            if (TrueMin)
            {
                this.Parent.FindForm().WindowState = FormWindowState.Minimized;
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            if (TrueClose)
            {
                this.Parent.FindForm().Close();
            }
        }

        private void topPanel_SizeChanged(object sender, EventArgs e)
        {
            if (this != null)
            {
                if(Close.Work)Close.parent_Size(this.Width - xClose, 8);
                if(Max.Work)Max.parent_Size(this.Width - xMax, 8);
                if(Min.Work)Min.parent_Size(this.Width - xMin, 18);
            }

        }

        private void topPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Parent.Location = new Point(
                    this.Parent.Location.X + (e.X - moveX),
                    this.Parent.Location.Y + (e.Y - moveY)
                    );
            }
        }

        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            moveX = e.X;
            moveY = e.Y;
        }

        private void topPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

    }
}
