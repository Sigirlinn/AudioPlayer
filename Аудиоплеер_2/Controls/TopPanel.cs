using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
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

        int _moveX, _moveY;
        bool _MouseDown = false;
        int xMax = 40, xMin = 60, xClose = 20;
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
            Close.Click += new EventHandler(Close_Click);
            Min.Click += new EventHandler(Min_Click);
            Max.Click += new EventHandler(Max_Click);
        }

        public void OffMax(){
            TrueMax = false;
            Max.work = false;
        }

        public void OffMin()
        {
            TrueMin = false;
            Min.work = false;
        }

        public void OffClose()
        {
            TrueClose = false;
            Close.work = false;
        }


        void Max_Click(object sender, EventArgs e)
        {
            if (TrueMax)
            {
                if (this.Parent.FindForm().WindowState == FormWindowState.Normal)
                    this.Parent.FindForm().WindowState = FormWindowState.Maximized;
                else this.Parent.FindForm().WindowState = FormWindowState.Normal;
            }
        }

        void Min_Click(object sender, EventArgs e)
        {
            if (TrueMin)
            {
                this.Parent.FindForm().WindowState = FormWindowState.Minimized;
            }
        }

        void Close_Click(object sender, EventArgs e)
        {
            if (TrueClose)
            {
                this.Parent.FindForm().Close();
            }
        }

        private void TopPanel_SizeChanged(object sender, EventArgs e)
        {
            if (this != null)
            {
                if(Close.work)Close.parent_Size(this.Width - xClose, 8);
                if(Max.work)Max.parent_Size(this.Width - xMax, 8);
                if(Min.work)Min.parent_Size(this.Width - xMin, 18);
            }

        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_MouseDown)
            {
                this.Parent.Location = new Point(
                    this.Parent.Location.X + (e.X - _moveX),
                    this.Parent.Location.Y + (e.Y - _moveY)
                    );
            }
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            _MouseDown = true;
            _moveX = e.X;
            _moveY = e.Y;
        }

        private void TopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            _MouseDown = false;
        }

    }
}
