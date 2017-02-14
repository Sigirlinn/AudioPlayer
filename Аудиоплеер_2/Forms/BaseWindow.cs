using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
