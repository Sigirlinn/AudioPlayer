using System;
using Аудиоплеер_2.Interface;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using Аудиоплеер_2.Forms;

namespace Аудиоплеер_2.Classes
{
    class NameAudio
    {
        public int Number;
        public Point ХУ;
        public event EventHandler DoubleClick;
        public bool ThisClick = false;
        public int NameLenght;

        private int width;
        private int height;
        static Image _background = Аудиоплеер_2.Properties.Resources.Фон;
        static Image _move = Аудиоплеер_2.Properties.Resources.Панель; 
        static Image _select = Аудиоплеер_2.Properties.Resources.minM;
        private IButtonParent parent;
        private PlayList form;

        private bool enterMouse = false;
        private string text = "";
        private string buftext = "";
        private bool workThread = false;
        private int step = 2;

        private Font font = new Font("Courier New", 8, FontStyle.Bold);
        private Brush brush = new SolidBrush(Color.Black);

        public NameAudio(
            Point location, 
            int width, 
            int height, 
            IButtonParent parent, 
            string time, 
            string name)
        {
            parent.Paint += new PaintEventHandler(parent_Paint);
            parent.MouseMove += new MouseEventHandler(parent_MouseMove);
            parent.DoubleClick += new EventHandler(parent_DoubleClick);
            ХУ = location;
            this.width = width;
            this.height = height;
            this.parent = parent;
            this.form = ((PlayList)parent);
            this.text = name + '\n' + time;
            this.buftext = text;
            NameLenght = buftext.Length;
        }

        private void parent_DoubleClick(object sender, EventArgs e)
        {
            if (DoubleClick != null && enterMouse)
                DoubleClick(this, new EventArgs());
        }

        //Если название файла не помещается в плейлисте, то строка сдвигается
        //в бок, через некотрое время, если на неё наведен курсор
        public void GoStr()
        {
            int i = 0;
            while (i < 15)
            {
                Thread.Sleep(100);
                if (!workThread) return;
                i++;
            }
            while (text.Length > step + 9)       //time 00:00:00
            {

                string str = text.Substring(step, text.Length - step);
                if (!workThread) return;

                text = str;
                parent.Invalidate();

               
                Thread.Sleep(100);
                if (!workThread) return;
            }

            Thread.Sleep(100);
            if (!workThread) return;

            text = buftext; 
            parent.Invalidate(new Rectangle(ХУ.X, ХУ.Y, width, height));
        }

        private void parent_MouseMove(object sender, MouseEventArgs e)
        {
            int x1 = ХУ.X;
            int x2 = x1 + width;
            int y1 = ХУ.Y;
            int y2 = y1 + height;

            if ((e.Location.X >= x1 && e.Location.X <= x2) && 
                (e.Location.Y >= y1 && e.Location.Y <= y2))
            {
                if (!enterMouse)
                {
                    enterMouse = true;
                    if (!workThread)
                    {
                        workThread = true;
                        form.BeginRunStr(Number);                       
                    }
                }
                else return;
            }
            else
            {
                if (enterMouse)
                {
                    enterMouse = false;
                    workThread = false;
                    //_form.EndRunStr();
                    text = buftext;
                }
                else return;
            }
            parent.Invalidate(new Rectangle(x1, y1, width, height));
        }

        private void parent_Paint(object sender, PaintEventArgs e)
        {
            if (ThisClick)
            {
                e.Graphics.DrawImage(
                        _select,
                        ХУ.X,
                        ХУ.Y,
                        width,
                        height
                    );
            }
            else
            {
                if (enterMouse)
                    e.Graphics.DrawImage(
                        _move,
                        ХУ.X,
                        ХУ.Y,
                        width,
                        height
                    );
                else e.Graphics.DrawImage(
                        _background,
                        ХУ.X,
                        ХУ.Y,
                        width,
                        height
                    );
            }
            e.Graphics.DrawString(
                text,
                font,
                brush,
                ХУ.X + 5, ХУ.Y + 5
            );
        }

        public void Parent_Size(int x, int y)
        {
            ХУ.X += x;
            ХУ.Y += y;
            parent.Invalidate(new Rectangle(ХУ.X, ХУ.Y, width, height));
        }

        public void Dispose()
        {
            parent.Paint -= parent_Paint;
            parent.MouseMove -= parent_MouseMove;

        }
    }
}
