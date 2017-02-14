using System;
using System.Drawing;
using System.Windows.Forms;
using Аудиоплеер_2.Interface;
using Аудиоплеер_2.Controls;
using Аудиоплеер_2.Classes;
using System.IO;

namespace Аудиоплеер_2.Forms
{
    public partial class Option : BaseWindow, IButtonParent
    {
        public bool DemoClick = false;
        public int Number = 0;
        private Settings_file[] sf;
        private MainBottomPanel bParent;
        private Settings set;
        private Settings buf;

        public struct Settings_file
        {
            public string Name;
            public int A, B, G, D, E, M, S, Oda;
            public Color Csize;
            
        }

        public Option(MainBottomPanel parent)
        {
            InitializeComponent();
            topPanel1.OffMax();
            topPanel1.OffMin();
            buttonOption1.Label = "Задать цвет";
            buttonOption2.Label = "Расположение камеры";
            buttonOption3.Label = "Параметры визуализации";
            buttonOption4.Label = "Выбрать форму";
            buttonOption5.Label = "Камера 5";
            buttonOption6.Label = "Отмена"; 
            buttonOption7.Label = "Сохранить";
            buttonOption8.Label = "Сброс";
            buttonOption9.Label = "Выбрать цвет из палитры";
            buttonOption10.Label = "Камера 1";
            buttonOption11.Label = "Камера 2";
            buttonOption13.Label = "Камера 3";
            buttonOption14.Label = "Камера 4";
            buttonOption12.Label = "Сохранить";
            buttonOption15.Label = "DEmo";
            bParent = parent;
            Open();
            
            
        }

        public void Open()
        {
            StreamReader sr;
            try
            {
                sr = new StreamReader(Application.StartupPath.ToString() + "\\Settings.ini");
            }
            catch{
                MessageBox.Show("Не удалось открыть файл настроек 'Settings.ini'.");
                return;
            }
            try
            {
                string buf = sr.ReadToEnd();
                string[] line = buf.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                sf = new Settings_file[line.Length];
                for (int i = 0; i < line.Length; i++)
                {
                    sf[i] = new Settings_file();
                    string[] s = line[i].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    sf[i].Name = s[0];
                    sf[i].A = Convert.ToInt32(s[1]);
                    sf[i].B = Convert.ToInt32(s[2]);
                    sf[i].G = Convert.ToInt32(s[3]);
                    sf[i].D = Convert.ToInt32(s[4]);
                    sf[i].E = Convert.ToInt32(s[5]);
                    sf[i].M = Convert.ToInt32(s[6]);
                    sf[i].S = Convert.ToInt32(s[7]);
                    sf[i].Oda = Convert.ToInt32(s[8]);
                    sf[i].Csize = Color.FromArgb(Convert.ToInt32(s[9]));
                }
                sr.Dispose();
                sr.Close();
                for (int i = 0; i < sf.Length; i++)
                {
                    comboBox1.Items.Add(sf[i].Name);
                }
            }
            catch{
                MessageBox.Show("Не удалось считать данные из файла настроек 'Settings.ini'.");
                return;
            }
        }

        private void option_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();   
        }

        private void buttonOption3_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
            if (panel1.Visible)
            {
                set.Copy(buf);
                panel2.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
            }
        }

        private void buttonOption2_Click(object sender, EventArgs e)
        {
            //Камера
            panel4.Visible = !panel4.Visible;
            if (panel4.Visible)
            {
                set.Copy(buf);
                panel2.Visible = false;
                panel1.Visible = false;
                panel5.Visible = false;
            }
        }

        private void buttonOption1_Click(object sender, EventArgs e)
        {
            
            panel2.Visible = !panel2.Visible;
            if (panel2.Visible)
            {
                set.Copy(buf);
                panel1.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
            }
        }

        private void buttonOption9_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                set.User = colorDialog1.Color;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;
                radioButton9.Checked = false;
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) { set.Csize = 0; set.User = Color.Empty; }
            if (radioButton2.Checked) { set.Csize = 1; set.User = Color.Empty; }
            if (radioButton3.Checked) { set.Csize = 2; set.User = Color.Empty; }
            if (radioButton4.Checked) { set.Csize = 3; set.User = Color.Empty; }
            if (radioButton5.Checked) { set.Csize = 4; set.User = Color.Empty; }
            if (radioButton6.Checked) { set.Csize = 5; set.User = Color.Empty; }
            if (radioButton7.Checked) { set.Csize = 6; set.User = Color.Empty; }
            if (radioButton8.Checked) { set.Csize = 7; set.User = Color.Empty; }
            if (radioButton9.Checked) { set.Csize = 8; set.User = Color.Empty; }
        }

        private void option_Load(object sender, EventArgs e)
        {
            set = bParent.Display.Set;
            buf = new Settings();
        }

        private void buttonOption7_Click(object sender, EventArgs e)
        {
            set.Copy(buf);
        }

        private void synchronous()
        {
            numericUpDown1.Value = (decimal)set.Alfa;
            numericUpDown2.Value = (decimal)set.Beta;
            numericUpDown3.Value = (decimal)set.Gamma;
            numericUpDown4.Value = (decimal)set.Delta;
            numericUpDown5.Value = (decimal)set.Eps;
            numericUpDown6.Value = (decimal)set.Msize;
            numericUpDown7.Value = (decimal)set.Sr;
            switch (set.ODA)
            {
                case 4: radioButton10.Checked = true; break;
                case 1: radioButton11.Checked = true; break;
                case 0: radioButton12.Checked = true; break;
                case 2: radioButton13.Checked = true; break;
                case 3: radioButton14.Checked = true; break;
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            set.Eps = (float)numericUpDown5.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            set.Delta = (float)numericUpDown4.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            set.Gamma = (float)numericUpDown3.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            set.Beta = (float)numericUpDown2.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            set.Alfa = (float)numericUpDown1.Value;
        }

        private void buttonOption6_Click(object sender, EventArgs e)
        {
            buf.Copy(set);
            synchronous();
        }

        private void buttonOption8_Click(object sender, EventArgs e)
        {
            Settings reset = new Settings();
            reset.Copy(set);
            synchronous();
        }

        private void buttonOption10_Click(object sender, EventArgs e)
        {
            set.Camera(0);
        }

        private void buttonOption11_Click(object sender, EventArgs e)
        {
            set.Camera(1);
        }

        private void buttonOption13_Click(object sender, EventArgs e)
        {
            set.Camera(3);
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            set.Msize = (int)numericUpDown6.Value;
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            set.Sr = (float)numericUpDown7.Value;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked) { set.ODA = 4;}
            if (radioButton11.Checked) { set.ODA = 1;}
            if (radioButton12.Checked) { set.ODA = 0;}
            if (radioButton13.Checked) { set.ODA = 2;}
            if (radioButton14.Checked) { set.ODA = 3;}
        }

        private void buttonOption5_Click(object sender, EventArgs e)
        {
            set.Camera(5);
        }

        private void buttonOption14_Click(object sender, EventArgs e)
        {
            set.Camera(6);
        }

        private void buttonOption4_Click(object sender, EventArgs e)
        {
            panel5.Visible = !panel5.Visible;
            if (panel5.Visible)
            {
                set.Copy(buf);
                panel1.Visible = false;
                panel4.Visible = false;
                panel2.Visible = false;
            }
        }

        public void Next()
        {
            set.Alfa = sf[Number].A;
            set.Beta = sf[Number].B;
            set.Gamma = sf[Number].G;
            set.Delta = sf[Number].D;
            set.Eps = sf[Number].E;
            set.Msize = sf[Number].M;
            set.Sr = sf[Number].S;
            set.ODA = sf[Number].Oda;
            set.User = sf[Number].Csize;
            //synchronous();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < sf.Length; i++)
            {
                if (comboBox1.Text == sf[i].Name)
                {
                    set.Alfa = sf[i].A;
                    set.Beta = sf[i].B;
                    set.Gamma = sf[i].G;
                    set.Delta = sf[i].D;
                    set.Eps = sf[i].E;
                    set.Msize = sf[i].M;
                    set.Sr = sf[i].S;
                    set.ODA = sf[i].Oda;
                    set.User = sf[i].Csize;
                    synchronous();
                    Number = i;
                }
            }
        }

        private void buttonOption12_Click(object sender, EventArgs e)
        {
            sf = AddSettings(textBox1.Text);
            comboBox1.Items.Add(textBox1.Text);
            Write();
            textBox1.Clear();
        }

        public Settings_file[] AddSettings(string name)
        {
            Settings_file[] buf = new Settings_file[sf.Length + 1];
            int i;
            for (i = 0; i < sf.Length; i++)
            {
                buf[i] = new Settings_file();
                buf[i].A = sf[i].A;
                buf[i].B = sf[i].B;
                buf[i].G = sf[i].G;
                buf[i].D = sf[i].D;
                buf[i].E = sf[i].E;
                buf[i].M = sf[i].M;
                buf[i].S = sf[i].S;
                buf[i].Oda = sf[i].Oda;
                buf[i].Name = sf[i].Name;
                buf[i].Csize = sf[i].Csize;

            }
            buf[i] = new Settings_file();
            buf[i].A = (int)set.Alfa;
            buf[i].B = (int)set.Beta;
            buf[i].G = (int)set.Gamma;
            buf[i].D = (int)set.Delta;
            buf[i].E = (int)set.Eps;
            buf[i].M = set.Msize;
            buf[i].S = (int)set.Sr;
            buf[i].Oda = set.ODA;
            buf[i].Name = name;
            buf[i].Csize = set.User;
            return buf;
        }

        public void Write()
        {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "/settings.ini",false);
            for (int i = 0; i < sf.Length; i++)
            {
                sw.WriteLine(sf[i].Name + " " +
                    sf[i].A.ToString() + " " +
                    sf[i].B.ToString() + " " +
                    sf[i].G.ToString() + " " +
                    sf[i].D.ToString() + " " +
                    sf[i].E.ToString() + " " +
                    sf[i].M.ToString() + " " +
                    sf[i].S.ToString() + " " +
                    sf[i].Oda.ToString() + " " +
                    sf[i].Csize.ToArgb());
            }
            sw.Close();
        }

        private void buttonOption15_Click(object sender, EventArgs e)
        {
            DemoClick = !DemoClick;
        }

    }
}
