using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Аудиоплеер_2.Forms;
using Аудиоплеер_2.Controls;

namespace Аудиоплеер_2
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
