using System;
using System.Drawing;
using System.Windows.Forms;

namespace Аудиоплеер_2.Controls
{
    public partial class ButtonOption : UserControl
    {
        public string Label = "";

        private Font font = new Font("Courier New", 10, FontStyle.Bold);
        private Brush brush = new SolidBrush(Color.Black);
        private RectangleF rectangle;
        private StringFormat stringFormat = new StringFormat();

        public ButtonOption()
        {
            InitializeComponent();
            rectangle = new RectangleF(0, 0, this.Width, this.Height);
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
        }
        private void buttonOption_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(Label, font, brush, rectangle, stringFormat);
        }

        private void buttonOption_Load(object sender, EventArgs e)
        {
            rectangle = new RectangleF(0, 0, this.Width, this.Height);
        }
    }
}
