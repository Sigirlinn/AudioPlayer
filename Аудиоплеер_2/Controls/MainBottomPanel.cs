using System;
using System.Drawing;
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
        public GraphicsFFT Display;

        private bool openFile = false;
        private PlayList formPL;
        private Option formOption;
        private MainWindow formParent;
        private System.Timers.Timer timer1;
        private int counter = 0;
        private int counterDemo = 0;

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
            

            formPL = new PlayList(this);
            formOption = new Option(this);
            Play.Click += new EventHandler(play_Click);
            LeftButton.Click += new EventHandler(leftButton_Click);
            RightButton.Click += new EventHandler(rightButton_Click);
            Volume.Click += new EventHandler(volume_Click);
            PlayList.Click += new EventHandler(playList_Click);
            Option.Click += new EventHandler(option_Click);
            Open.Click += new EventHandler(open_Click);
            Cycle.Click += new EventHandler(cycle_Click);
            Content.Begin += new EventHandler(content_Begin);
            Content.End += new EventHandler(Content_End);
            
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (formParent.panel1.Visible)
            {
                Content.SetVolume(formParent.volume);
            }
        }

        private void Content_End(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private string textForm(int i)
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

        private void content_Begin(object sender, EventArgs e)
        {
            if (timer1 != null) timer1.Dispose();
            timer1 = new System.Timers.Timer(30);
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
            int num = Content.GetNumber();
            Invoke(new Action(() =>
            {
                formParent.label1.Text = textForm(num);
                formParent.label2.Text = Content.GetShortFileTime()[num];
                formParent.label3.Text = Content.GetTimeNow();
                Display.Init_OGL();
            }));
            Content.SetVolume(formParent.volume);
            
            timer1.Start();
        }

        private float windLen()
        {
            double a = Content.LenAll();
            double n = Content.LenNow();
            double panel = formParent.panel2.Width;
            float rez = (float)(panel / a * n);
            return rez;
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke( new Action(() =>{
                try
                {
                    Content.GetFFT(Display.FFT);
                    Display.DrawOGL_Sphere();
                    formParent.label3.Text = Content.GetTimeNow();
                    formParent.wind = windLen();
                    counter++;
                    counterDemo++;
                    if (counter > 25)
                    {
                        counter = 0;
                        formParent.panel2.Invalidate();
                    }
                    if(counterDemo > 100){
                        counterDemo = 0;
                        if (formOption.DemoClick)
                        {
                            formOption.Number++;
                            if (formOption.Number > 6) formOption.Number = 0;
                            formOption.Next();
                            rightButton_Click(this, new EventArgs());
                        }
                    }
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
        }));
        }

        private void cycle_Click(object sender, EventArgs e)
        {
            Content.Cycle = !Content.Cycle;
            Cycle.ClickYou = Content.Cycle;
        }

        private void open_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Аудио файлы (*.mp3)|*.mp3|Все файлы (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openFile = true;
                Content.SaveTimeAndNameFile(openFileDialog1.SafeFileNames, openFileDialog1.FileNames);
                Content.CreateStream(0);
                int num = Content.GetNumber();
                formParent.label1.Text = textForm(num);
                formParent.label2.Text = Content.GetShortFileTime()[num];
                formParent.label3.Text = Content.GetTimeNow();
                formPL.InitPlay(Content.GetShortFileTime(), openFileDialog1.SafeFileNames);
            }
            openFileDialog1.Dispose();
        }

        public void SetAudio(int num)
        {
            Content.CreateStream(num);
            Content.Play();
            formPL.SetImageClick(num);
        }

        private void option_Click(object sender, EventArgs e)
        {
            formOption.Visible = !formOption.Visible;
            Option.ClickYou = formOption.Visible;
        }

        private void playList_Click(object sender, EventArgs e)
        {
            formPL.Visible = !formPL.Visible;
            PlayList.ClickYou = formPL.Visible;
        }

        private void volume_Click(object sender, EventArgs e)
        {
            formParent.panel1.Visible = !formParent.panel1.Visible;
        }

        private void play_Click(object sender, EventArgs e)
        {
            if (openFile)
            {
                Content.Play();
                int num = Content.GetNumber();
                formPL.SetImageClick(num);
            }
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            if (openFile)
            {
                Content.SetNumber(-1);
                int num = Content.GetNumber();
                Content.CreateStream(num);
                Content.Play();
                formPL.SetImageClick(num);
            }
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            if (openFile)
            {
                Content.SetNumber(1);
                int num = Content.GetNumber();
                Content.CreateStream(num);
                Content.Play();
                formPL.SetImageClick(num);
            }
        }

        private void mainBottomPanel_Load(object sender, EventArgs e)
        {
            try
            {
                formParent = ((MainWindow)this.Parent.FindForm());
                Display = new GraphicsFFT(formParent.OGL);
                formParent.panel1.MouseClick += new MouseEventHandler(panel1_MouseClick);
                formParent.panel2.MouseClick += new MouseEventHandler(panel2_MouseClick);
            }
            catch(Exception ef)
            {
                MessageBox.Show(ef.Message);
            }
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            double next = (double)formParent.wind / (double)formParent.panel2.Width * Content.LenAll();
            Content.SetLen(next);
        }

        private void mainBottomPanel_SizeChanged(object sender, EventArgs e)
        {
            if (Content != null && Display != null)
            {
                Content.GetFFT(Display.FFT);
                int num = Content.GetNumber();
                formParent.label1.Text = textForm(num);
                formParent.label2.Text = Content.GetShortFileTime()[num];
                formParent.label3.Text = Content.GetTimeNow();
                formParent.wind = windLen();
                formParent.panel2.Invalidate();
                Display.Init_OGL();
                Display.DrawOGL_Sphere();
            }
        }
    }
}
