using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Аудиоплеер_2.Interface;
using ButtonBase = Аудиоплеер_2.Classes.ButtonBase;
using Аудиоплеер_2.Classes;
using Аудиоплеер_2.Forms;
using System.Threading;

namespace Аудиоплеер_2.Controls
{
    public partial class MainBottomPanel : UserControl, IButtonParent 
    {
        public ButtonBase Play;
        public ButtonBase LeftButton;
        public ButtonBase RightButton;
        public ButtonBase Volume;
        public ButtonBase PlayList;
        public ButtonBase Option;
        public ButtonBase Open;
        public ButtonBase Cycle;
        public Audio Content = new Audio();
        public GraphicsFFT display;

        bool _openFile = false;
        PlayList _PL;
        Option _Op;
        MainWindow _parent;
        System.Timers.Timer timer1;
        int counter = 0;
        int counterDemo = 0;

        public MainBottomPanel()
        {
            InitializeComponent();
            Play = new ButtonBase(new Point(30, 10), 20, 20,
                Аудиоплеер_2.Properties.Resources.play,
                Аудиоплеер_2.Properties.Resources.playM,
                null,
                this
                );
            LeftButton = new ButtonBase(new Point(5, 13), 15, 15,
                Аудиоплеер_2.Properties.Resources.left,
                Аудиоплеер_2.Properties.Resources.leftM,
                null,
                this
                );
            RightButton = new ButtonBase(new Point(55, 13), 15, 15,
                Аудиоплеер_2.Properties.Resources.right,
                Аудиоплеер_2.Properties.Resources.rightM,
                null,
                this
                );
            Volume = new ButtonBase(new Point(80, 13), 15, 15,
                Аудиоплеер_2.Properties.Resources.volume,
                Аудиоплеер_2.Properties.Resources.volumeM,
                null,
                this
                );
            Cycle = new ButtonBase(new Point(105, 13), 15, 15, 
                Аудиоплеер_2.Properties.Resources.cycle,
                Аудиоплеер_2.Properties.Resources.cycleM,
                Аудиоплеер_2.Properties.Resources.cycleC,
                this
                );
            PlayList = new ButtonBase(new Point(130, 13), 15, 15,
                Аудиоплеер_2.Properties.Resources.list,
                Аудиоплеер_2.Properties.Resources.listM,
                Аудиоплеер_2.Properties.Resources.listC,
                this
                );
            Option = new ButtonBase(new Point(155, 13), 15, 15, 
                Аудиоплеер_2.Properties.Resources.option,
                Аудиоплеер_2.Properties.Resources.optionM,
                Аудиоплеер_2.Properties.Resources.optionC,
                this
                );
            Open = new ButtonBase(new Point(180, 13), 15, 15, 
                Аудиоплеер_2.Properties.Resources.open,
                Аудиоплеер_2.Properties.Resources.openM,
                null,
                this
                );
            

            _PL = new PlayList(this);
            _Op = new Option(this);
            Play.Click += new EventHandler(Play_Click);
            LeftButton.Click += new EventHandler(LeftButton_Click);
            RightButton.Click += new EventHandler(RightButton_Click);
            Volume.Click += new EventHandler(Volume_Click);
            PlayList.Click += new EventHandler(PlayList_Click);
            Option.Click += new EventHandler(Option_Click);
            Open.Click += new EventHandler(Open_Click);
            Cycle.Click += new EventHandler(Cycle_Click);
            Content.Begin += new EventHandler(Content_Begin);
            Content.End += new EventHandler(Content_End);
            
        }

        void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_parent.panel1.Visible)
            {
                Content.SetVolume(_parent.volume);
            }
        }

        void Content_End(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        string textForm(int i)
        {
            int pred = 0;
            if (this.Width > 500) pred = 150;
            else pred = 40;
            string name = Content.GetShortFileName()[i];
            if (name.Length > pred)
            {
                name = name.Remove(pred);
                name += "...";
            }
            //string time = Content.GetShortFileTime()[i];
            return name;
            
        }

        void Content_Begin(object sender, EventArgs e)
        {
            if (timer1 != null) timer1.Dispose();
            timer1 = new System.Timers.Timer(30);
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
            int num = Content.GetNumber();
            Invoke(new Action(() =>
            {
                _parent.label1.Text = textForm(num);
                _parent.label2.Text = Content.GetShortFileTime()[num];
                _parent.label3.Text = Content.GetTimeNow();
                display.Init_OGL();
            }));
            Content.SetVolume(_parent.volume);
            
            timer1.Start();
        }

        float windLen()
        {
            double a = Content.LenAll();
            double n = Content.LenNow();
            double panel = _parent.panel2.Width;
            float rez = (float)(panel / a * n);
            return rez;
        }

        void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke( new Action(() =>{
                try
                {
                    Content.GetFFT(display.fft);
                    display.drawOGL_Sphere();
                    _parent.label3.Text = Content.GetTimeNow();
                    _parent.wind = windLen();
                    counter++;
                    counterDemo++;
                    if (counter > 25)
                    {
                        counter = 0;
                        _parent.panel2.Invalidate();
                    }
                    if(counterDemo > 100){
                        counterDemo = 0;
                        if (_Op.DemoClick)
                        {
                            _Op.number++;
                            if (_Op.number > 6) _Op.number = 0;
                            _Op.Next();
                            RightButton_Click(this, new EventArgs());
                        }
                    }
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
        }));
        }

        void Cycle_Click(object sender, EventArgs e)
        {
            Content.cycle = !Content.cycle;
            Cycle.clickyou = Content.cycle;
        }

        void Open_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _openFile = true;
                Content.MemberFile(openFileDialog1.SafeFileNames, openFileDialog1.FileNames);
                Content.CreateStream(0);
                int num = Content.GetNumber();
                _parent.label1.Text = textForm(num);
                _parent.label2.Text = Content.GetShortFileTime()[num];
                _parent.label3.Text = Content.GetTimeNow();
                _PL.InitPlay(Content.GetShortFileTime(), openFileDialog1.SafeFileNames);
            }
            openFileDialog1.Dispose();
        }

        public void SetAudio(int num)
        {
            Content.CreateStream(num);
            Content.Play();
            _PL.SetImageClick(num);
        }

        void Option_Click(object sender, EventArgs e)
        {
            _Op.Visible = !_Op.Visible;
            Option.clickyou = _Op.Visible;
        }

        void PlayList_Click(object sender, EventArgs e)
        {
            _PL.Visible = !_PL.Visible;
            PlayList.clickyou = _PL.Visible;
        }

        void Volume_Click(object sender, EventArgs e)
        {
            _parent.panel1.Visible = !_parent.panel1.Visible;
        }

        void Play_Click(object sender, EventArgs e)
        {
            if (_openFile)
            {
                Content.Play();
                int num = Content.GetNumber();
                _PL.SetImageClick(num);
            }
        }

        void LeftButton_Click(object sender, EventArgs e)
        {
            if (_openFile)
            {
                Content.SetNumber(-1);
                int num = Content.GetNumber();
                Content.CreateStream(num);
                Content.Play();
                _PL.SetImageClick(num);
            }
        }

        void RightButton_Click(object sender, EventArgs e)
        {
            if (_openFile)
            {
                Content.SetNumber(1);
                int num = Content.GetNumber();
                Content.CreateStream(num);
                Content.Play();
                _PL.SetImageClick(num);
            }
        }

        private void MainBottomPanel_Load(object sender, EventArgs e)
        {
            try
            {
                _parent = ((MainWindow)this.Parent.FindForm());
                display = new GraphicsFFT(_parent.OGL);
                _parent.panel1.MouseClick += new MouseEventHandler(panel1_MouseClick);
                _parent.panel2.MouseClick += new MouseEventHandler(panel2_MouseClick);
            }
            catch(Exception ef)
            {
                MessageBox.Show(ef.Message);
            }
        }

        void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            double next = (double)_parent.wind / (double)_parent.panel2.Width * Content.LenAll();
            Content.SetLen(next);
        }

        private void MainBottomPanel_SizeChanged(object sender, EventArgs e)
        {
            if (Content != null && display != null)
            {
                Content.GetFFT(display.fft);
                int num = Content.GetNumber();
                _parent.label1.Text = textForm(num);
                _parent.label2.Text = Content.GetShortFileTime()[num];
                _parent.label3.Text = Content.GetTimeNow();
                _parent.wind = windLen();
                _parent.panel2.Invalidate();
                display.Init_OGL();
                display.drawOGL_Sphere();
            }
        }
    }
}
