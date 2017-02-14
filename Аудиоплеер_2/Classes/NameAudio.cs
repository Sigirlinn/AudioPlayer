using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Аудиоплеер_2.Interface;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using Аудиоплеер_2.Forms;

namespace Аудиоплеер_2.Classes
{
    class NameAudio
    {
        public int number;
        public Point ХУ;
        public event EventHandler DoubleClick;
        public bool thisClick = false;
        public int NameLenght;

        int _width;
        int _height;
        static Image _background = Аудиоплеер_2.Properties.Resources.Фон;
        static Image _move = Аудиоплеер_2.Properties.Resources.Панель; 
        static Image _select = Аудиоплеер_2.Properties.Resources.minM;
        IButtonParent _parent;
        PlayList _form;
        
        bool _enterMouse = false;
        string _text = "";
        string _buftext = "";
        bool _workThread = false;
        int _step = 2;

        Font F = new Font("Courier New", 8, FontStyle.Bold);
        Brush B = new SolidBrush(Color.Black);

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
            _width = width;
            _height = height;
            _parent = parent;
            _form = ((PlayList)parent);
            _text = name + '\n' + time;
            _buftext = _text;
            NameLenght = _buftext.Length;
        }

        void parent_DoubleClick(object sender, EventArgs e)
        {
            if (DoubleClick != null && _enterMouse)
                DoubleClick(this, new EventArgs());
        }

        public void goStr()
        {
            int i = 0;
            while (i < 15)
            {
                Thread.Sleep(100);
                if (!_workThread) return;
                i++;
            }
            while (_text.Length > _step + 9)       //time 00:00:00
            {

                string str = _text.Substring(_step, _text.Length - _step);
                if (!_workThread) return;

                _text = str;
                _parent.Invalidate();

               
                Thread.Sleep(100);
                if (!_workThread) return;
            }

            Thread.Sleep(100);
            if (!_workThread) return;

            _text = _buftext; 
            _parent.Invalidate(new Rectangle(ХУ.X, ХУ.Y, _width, _height));
        }

        void parent_MouseMove(object sender, MouseEventArgs e)
        {
            int x1 = ХУ.X;
            int x2 = x1 + _width;
            int y1 = ХУ.Y;
            int y2 = y1 + _height;

            if ((e.Location.X >= x1 && e.Location.X <= x2) && 
                (e.Location.Y >= y1 && e.Location.Y <= y2))
            {
                if (!_enterMouse)
                {
                    _enterMouse = true;
                    if (!_workThread)
                    {
                        _workThread = true;
                        _form.BeginRunStr(number);                       
                    }
                }
                else return;
            }
            else
            {
                if (_enterMouse)
                {
                    _enterMouse = false;
                    _workThread = false;
                    //_form.EndRunStr();
                    _text = _buftext;
                }
                else return;
            }
            _parent.Invalidate(new Rectangle(x1, y1, _width, _height));
        }

        void parent_Paint(object sender, PaintEventArgs e)
        {
            if (thisClick)
            {
                e.Graphics.DrawImage(
                        _select,
                        ХУ.X,
                        ХУ.Y,
                        _width,
                        _height
                    );
            }
            else
            {
                if (_enterMouse)
                    e.Graphics.DrawImage(
                        _move,
                        ХУ.X,
                        ХУ.Y,
                        _width,
                        _height
                    );
                else e.Graphics.DrawImage(
                        _background,
                        ХУ.X,
                        ХУ.Y,
                        _width,
                        _height
                    );
            }
            e.Graphics.DrawString(
                _text,
                F,
                B,
                ХУ.X + 5, ХУ.Y + 5
            );
        }

        public void parent_Size(int X, int Y)
        {
            ХУ.X += X;
            ХУ.Y += Y;
            _parent.Invalidate(new Rectangle(ХУ.X, ХУ.Y, _width, _height));
        }

        public void dispose()
        {
            _parent.Paint -= parent_Paint;
            _parent.MouseMove -= parent_MouseMove;

        }
    }
}
