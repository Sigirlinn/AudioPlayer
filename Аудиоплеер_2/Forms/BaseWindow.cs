using System.Windows.Forms;
using Аудиоплеер_2.Interface;

namespace Аудиоплеер_2.Forms
{
    public partial class BaseWindow : Form, IButtonParent 
    {

        public BaseWindow()
        {
            InitializeComponent();
        }

    }
}
