using System;
using System.Drawing;
using System.Windows.Forms;
using Аудиоплеер_2.Classes;
using Аудиоплеер_2.Interface;
using Аудиоплеер_2.Controls;
using System.Threading;

namespace Аудиоплеер_2.Forms
{
    public partial class PlayList : BaseWindow, IButtonParent
    {
        private NameAudio[] an = null;
        private int anClick = 0;
        private bool mouseDown = false;
        private Thread runStr;
        private float run = 0;
        private float step, stepDraw;
        private MainBottomPanel parent;

        public PlayList(MainBottomPanel parent)
        {
            InitializeComponent();
            topPanel1.OffMax();
            topPanel1.OffMin();
            MouseWheel += new MouseEventHandler(playList_MouseWheel);
            this.parent = parent;
        }

        public void InitPlay(string []time, string []name)
        {
            //отписываемся от событий родителя и освобождаем память
            if (an != null)
            {
                for (int i = 0; i < an.Length; i++)
                {
                    an[i].Dispose();
                }
                this.Invalidate();
            }

            an = new NameAudio[name.Length];
            int y = 5 + topPanel1.Height;
            for (int i = 0; i < name.Length; i++)
            {  
                an[i] = new NameAudio(
                    new Point(25, y + 40 * i),
                    this.Width - 5,
                    35,
                    this,
                    time[i], 
                    name[i]
                );
                an[i].Number = i;
                an[i].DoubleClick += new EventHandler(playList_Click);
            }
            an[0].ThisClick = true;
            run = 0;

            // размер бегунка, не трогать!
            float count = (float)panel1.Height / (float)(an.Length * 40);
            if (count >= 1) 
                stepDraw = panel1.Height;
            else 
                stepDraw = count * panel1.Height;
            step = count;

            this.Invalidate();
            
        }

        private void playList_Click(object sender, EventArgs e)
        {
            try
            {
                int but = ((NameAudio)sender).Number;
                parent.SetAudio(but);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void playList_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void BeginRunStr(int i)
        {
            if (runStr == null || runStr.ThreadState == ThreadState.Stopped)
            {
                if (an[i].NameLenght > 50)
                {
                    runStr = new Thread(new ThreadStart(an[i].GoStr));
                    runStr.Start();
                }
            }
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(
                Аудиоплеер_2.Properties.Resources.pauseM, 
                0, 
                run, 
                panel1.Width,
                stepDraw
            );
        }

        private void runMouse(int yNow)
        {
            if (an != null)
            {
                //находим разность между предыдущем значением позиции бегунка и текущем
                //умножив на шаг найдем на сколько нужно скролить
                float mat = (run - yNow) / step;
                //проверяем получившуюся величину на границу, не выедем ли в молоко
                if ((an[0].ХУ.Y + mat <= 35) &&
                    (an[an.Length - 1].ХУ.Y + 40 + mat >= 355))
                {
                    //сдвигаем каждую картинку на вычисленную величину
                    for (int i = 0; i < an.Length; i++)
                    {
                        an[i].Parent_Size(0, (int)mat);
                    }
                    //запоминаем положение бегунка
                    run = yNow;
                }
                //отрисовываем бегунок
                panel1.Invalidate();
                //отрисовываем картинки
                this.Invalidate();
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                runMouse(e.Y);
            }
        }

        private void playList_MouseWheel(object sender, MouseEventArgs e)
        {
            if (an != null)
            {
                if ((an[0].ХУ.Y + e.Delta / 10 <= 35) &&
                    (an[an.Length - 1].ХУ.Y + 40 + e.Delta / 10 >= 355))
                {
                    run += (-e.Delta / 10 * step);
                    for (int i = 0; i < an.Length; i++)
                    {
                        an[i].Parent_Size(0, e.Delta / 10);
                    }
                }
            }
            panel1.Invalidate();
            this.Invalidate();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            runMouse(e.Y);
            mouseDown = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

            runMouse(e.Y);
            mouseDown = true;
        }

        private void playList_VisibleChanged(object sender, EventArgs e)
        {
            run = 0;
        }

        public void SetImageClick(int x){

            if (an != null)
            {
                an[anClick].ThisClick = false;
                an[x].ThisClick = true;
                anClick = x;
                this.Invalidate();
            }
        }
    }
}
