using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Аудиоплеер_2.Controls
{
    public partial class ButtonOption : UserControl
    {
        public string text = "";
        Font _f = new Font("Courier New", 10, FontStyle.Bold);
        Brush _b = new SolidBrush(Color.Black);
        RectangleF _r;
        StringFormat _sf = new StringFormat();
        public ButtonOption()
        {
            InitializeComponent();
            _r = new RectangleF(0, 0, this.Width, this.Height);
            _sf.Alignment = StringAlignment.Center;
            _sf.LineAlignment = StringAlignment.Center;
        }
        private void ButtonOption_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(text, _f, _b, _r, _sf);
        }

        private void ButtonOption_Load(object sender, EventArgs e)
        {
            _r = new RectangleF(0, 0, this.Width, this.Height);
        }
    }
}
