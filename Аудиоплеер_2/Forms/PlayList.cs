using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Аудиоплеер_2.Classes;
using System.Runtime.InteropServices;
using System.Reflection;
using Аудиоплеер_2.Interface;
using Аудиоплеер_2.Controls;
using System.Threading;

namespace Аудиоплеер_2.Forms
{
    public partial class PlayList : BaseWindow, IButtonParent
    {
        int ANClick = 0;
        NameAudio []_AN = null;
        bool _mouseDown = false;
        Thread _RunStr;
        float _Run = 0;
        float _step, _stepDraw;
        MainBottomPanel _parent;

        public PlayList(MainBottomPanel parent)
        {
            InitializeComponent();
            topPanel1.OffMax();
            topPanel1.OffMin();
            MouseWheel += new MouseEventHandler(PlayList_MouseWheel);
            _parent = parent;
        }

        public void InitPlay(string []time, string []name)
        {
            //отписываемся от событий родителя и освобождаем память (типа)
            if (_AN != null)
            {
                for (int i = 0; i < _AN.Length; i++)
                {
                    _AN[i].dispose();
                }
                this.Invalidate();
            }

            _AN = new NameAudio[name.Length];
            int y = 5 + topPanel1.Height;
            for (int i = 0; i < name.Length; i++)
            {  
                _AN[i] = new NameAudio(
                    new Point(25, y + 40 * i),
                    this.Width - 5,
                    35,
                    this,
                    time[i], 
                    name[i]
                );
                _AN[i].number = i;
                _AN[i].DoubleClick += new EventHandler(PlayList_Click);
            }
            _AN[0].thisClick = true;
            _Run = 0;

            // размер бегунка, не трогать!
            float count = (float)panel1.Height / (float)(_AN.Length * 40);
            if (count >= 1) 
                _stepDraw = panel1.Height;
            else 
                _stepDraw = count * panel1.Height;
            _step = count;

            this.Invalidate();
            
        }

        private void PlayList_Click(object sender, EventArgs e)
        {
            try
            {
                int but = ((NameAudio)sender).number;
                _parent.SetAudio(but);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PlayList_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void BeginRunStr(int i)
        {
            if (_RunStr == null || _RunStr.ThreadState == ThreadState.Stopped)
            {
                if (_AN[i].NameLenght > 50)
                {
                    _RunStr = new Thread(new ThreadStart(_AN[i].goStr));
                    _RunStr.Start();
                }
            }
        }

        public void EndRunStr()
        {
            
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(
                Аудиоплеер_2.Properties.Resources.pauseM, 
                0, 
                _Run, 
                panel1.Width,
                _stepDraw
            );
        }

        private void RunMouse(int Ynow)
        {
            if (_AN != null)
            {
                //находим разность между предыдущем значением позиции бегунка и текущем
                //умножив на шаг найдем на сколько нужно скролить
                float mat = (_Run - Ynow) / _step;
                //проверяем получившуюся величину на границу, не выедем ли в молоко
                if ((_AN[0].ХУ.Y + mat <= 35) &&
                    (_AN[_AN.Length - 1].ХУ.Y + 40 + mat >= 355))
                {
                    //сдвигаем каждую картинку на вычисленныю величину
                    for (int i = 0; i < _AN.Length; i++)
                    {
                        _AN[i].parent_Size(0, (int)mat);
                    }
                    //запоминаем положение бегунка
                    _Run = Ynow;
                }
                //отрисовываем бегунок
                panel1.Invalidate();
                //отрисовываем картинки
                this.Invalidate();
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
            {
                RunMouse(e.Y);
            }
        }

        private void PlayList_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_AN != null)
            {
                if ((_AN[0].ХУ.Y + e.Delta / 10 <= 35) &&
                    (_AN[_AN.Length - 1].ХУ.Y + 40 + e.Delta / 10 >= 355))
                {
                    _Run += (-e.Delta / 10 * _step);
                    for (int i = 0; i < _AN.Length; i++)
                    {
                        _AN[i].parent_Size(0, e.Delta / 10);
                    }
                }
            }
            panel1.Invalidate();
            this.Invalidate();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            RunMouse(e.Y);
            _mouseDown = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Y < Run && e.Y > Run + stepDraw)
            //{
            RunMouse(e.Y);
            //}
            _mouseDown = true;
        }

        private void PlayList_VisibleChanged(object sender, EventArgs e)
        {
            _Run = 0;
        }

        public void SetImageClick(int x){

            if (_AN != null)
            {
                _AN[ANClick].thisClick = false;
                _AN[x].thisClick = true;
                ANClick = x;
                this.Invalidate();
            }
        }
    }
}
