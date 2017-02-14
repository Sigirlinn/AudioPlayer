using System;
using System.Drawing;
using System.Windows.Forms;
using Аудиоплеер_2.Interface;

namespace Аудиоплеер_2.Classes
{
    public class ButtonBase
    {
        private Point location;
        private int width;
        private int height;
        private Image backgroundImage, moveImage, clickImage;
        private IButtonParent parent;
        private bool enterMouse = false;
        public bool Work = true;
        public bool ClickYou = false;
        public event EventHandler Click;


        public ButtonBase(Point location, int width, int height, Image background, Image move, Image click, IButtonParent parent)
        {
            parent.Paint += new PaintEventHandler(parent_Paint);
            parent.MouseMove += new MouseEventHandler(parent_MouseMove);
            parent.MouseClick += new MouseEventHandler(parent_MouseClick);
            parent.MouseLeave += new EventHandler(parent_MouseLeave);
            this.location = location;
            this.width = width;
            this.height = height;
            this.backgroundImage = background;
            this.moveImage = move;
            this.clickImage = click;
            this.parent = parent;
        }

        private void parent_MouseLeave(object sender, EventArgs e)
        {
            enterMouse = false;
            parent.Invalidate(new Rectangle(location.X, location.Y, width, height));
        }

        private void parent_MouseClick(object sender, MouseEventArgs e)
        {
            if (Work)
            {
                if (Click != null && enterMouse) Click(this, new EventArgs());
            }
        }

        private void parent_MouseMove(object sender, MouseEventArgs e)
        {
            if (Work)
            {
                int x1 = location.X;
                int x2 = x1 + width;
                int y1 = location.Y;
                int y2 = y1 + height;

                if ((e.Location.X >= x1 && e.Location.X <= x2) && (e.Location.Y >= y1 && e.Location.Y <= y2))
                {
                    if (!enterMouse) enterMouse = true;
                    else return;
                }
                else
                {
                    if (enterMouse) enterMouse = false;
                    else return;
                }
                parent.Invalidate(new Rectangle(x1, y1, width, height));
            }
        }

        private void parent_Paint(object sender, PaintEventArgs e)
        {
            if (Work)
            {
                if (ClickYou)
                {
                    e.Graphics.DrawImage(clickImage, location.X, location.Y, width, height);
                }
                else
                {
                    if (enterMouse)
                        e.Graphics.DrawImage(moveImage, location.X, location.Y, width, height);
                    else
                        e.Graphics.DrawImage(backgroundImage, location.X, location.Y, width, height);
                }
            }
        }

        public void parent_Size(int x, int y)
        {
            if (Work)
            {
                location.X = x;
                location.Y = y;
                parent.Invalidate();
            }
        }
    }
}