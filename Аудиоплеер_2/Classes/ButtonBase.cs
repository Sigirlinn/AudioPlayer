using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Аудиоплеер_2.Interface;

namespace Аудиоплеер_2.Classes
{
    public class ButtonBase
    {
        Point _location;
        int _width;
        int _height;
        Image _background, _move, _click;
        IButtonParent _parent;
        bool _enterMouse = false;
        public bool work = true;
        public bool clickyou = false;
        public event EventHandler Click;


        public ButtonBase(Point location, int width, int height, Image background, Image move, Image click, IButtonParent parent)
        {
            parent.Paint += new PaintEventHandler(parent_Paint);
            parent.MouseMove += new MouseEventHandler(parent_MouseMove);
            parent.MouseClick += new MouseEventHandler(parent_MouseClick);
            parent.MouseLeave += new EventHandler(parent_MouseLeave);
            _location = location;
            _width = width;
            _height = height;
            _background = background;
            _move = move;
            _click = click;
            _parent = parent;
        }

        void parent_MouseLeave(object sender, EventArgs e)
        {
            _enterMouse = false;
            _parent.Invalidate(new Rectangle(_location.X, _location.Y, _width, _height));
        }

        void parent_MouseClick(object sender, MouseEventArgs e)
        {
            if (work)
            {
                if (Click != null && _enterMouse) Click(this, new EventArgs());
            }
        }

        void parent_MouseMove(object sender, MouseEventArgs e)
        {
            if (work)
            {
                int x1 = _location.X;
                int x2 = x1 + _width;
                int y1 = _location.Y;
                int y2 = y1 + _height;

                if ((e.Location.X >= x1 && e.Location.X <= x2) && (e.Location.Y >= y1 && e.Location.Y <= y2))
                {
                    if (!_enterMouse) _enterMouse = true;
                    else return;
                }
                else
                {
                    if (_enterMouse) _enterMouse = false;
                    else return;
                }
                _parent.Invalidate(new Rectangle(x1, y1, _width, _height));
            }
        }

        void parent_Paint(object sender, PaintEventArgs e)
        {
            if (work)
            {
                if (clickyou)
                {
                    e.Graphics.DrawImage(_click, _location.X, _location.Y, _width, _height);
                }
                else
                {
                    if (_enterMouse)
                        e.Graphics.DrawImage(_move, _location.X, _location.Y, _width, _height);
                    else
                        e.Graphics.DrawImage(_background, _location.X, _location.Y, _width, _height);
                }
            }
        }
        public void parent_Size(int X, int Y)
        {
            if (work)
            {
                _location.X = X;
                _location.Y = Y;
                _parent.Invalidate();
            }
        }
    }
}