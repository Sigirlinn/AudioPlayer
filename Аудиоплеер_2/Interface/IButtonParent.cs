using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Аудиоплеер_2.Interface
{

    public interface IButtonParent
    {
        event MouseEventHandler MouseMove;

        event PaintEventHandler Paint;

        event MouseEventHandler MouseClick;

        event EventHandler DoubleClick;

        event EventHandler MouseLeave;
        
        void Invalidate(Rectangle rc);

        void Invalidate();
        
    }
}
