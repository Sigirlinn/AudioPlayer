using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Аудиоплеер_2.Interface;
using Аудиоплеер_2.Controls;
using Аудиоплеер_2.Classes;
using System.IO;

namespace Аудиоплеер_2.Forms
{
    public partial class Option : BaseWindow, IButtonParent
    {
        public struct settings_file
        {
            public string name;
            public int a, b, g, d, e, m, s, oda;
            public Color csize;
            
        }
        public bool DemoClick = false;
        public int number = 0;
        settings_file []SF;
        MainBottomPanel _parent;
        Settings set;
        Settings buf;
              


        public Option(MainBottomPanel parent)
        {
            InitializeComponent();
            topPanel1.OffMax();
            topPanel1.OffMin();
            buttonOption1.text = "Задать цвет";
            buttonOption2.text = "Расположение камеры";
            buttonOption3.text = "Параметры визуализации";
            buttonOption4.text = "Выбрать форму";
            buttonOption5.text = "Камера 5";
            buttonOption6.text = "Отмена"; 
            buttonOption7.text = "Сохранить";
            buttonOption8.text = "Сброс";
            buttonOption9.text = "Выбрать цвет из палитры";
            buttonOption10.text = "Камера 1";
            buttonOption11.text = "Камера 2";
            buttonOption13.text = "Камера 3";
            buttonOption14.text = "Камера 4";
            buttonOption12.text = "Сохранить";
            buttonOption15.text = "DEmo";
            _parent = parent;
            open();
            
            
        }

        public void open()
        {
            StreamReader sr;
            try
            {
                sr = new StreamReader(Application.StartupPath + "/settings.ini");
            }
            catch{
                MessageBox.Show("Не удалось открыть файл настроек 'Settings.ini'.");
                return;
            }
            try
            {
                string buf = sr.ReadToEnd();
                string[] line = buf.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                SF = new settings_file[line.Length];
                for (int i = 0; i < line.Length; i++)
                {
                    SF[i] = new settings_file();
                    string[] s = line[i].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    SF[i].name = s[0];
                    SF[i].a = Convert.ToInt32(s[1]);
                    SF[i].b = Convert.ToInt32(s[2]);
                    SF[i].g = Convert.ToInt32(s[3]);
                    SF[i].d = Convert.ToInt32(s[4]);
                    SF[i].e = Convert.ToInt32(s[5]);
                    SF[i].m = Convert.ToInt32(s[6]);
                    SF[i].s = Convert.ToInt32(s[7]);
                    SF[i].oda = Convert.ToInt32(s[8]);
                    SF[i].csize = Color.FromArgb(Convert.ToInt32(s[9]));
                }
                sr.Dispose();
                sr.Close();
                for (int i = 0; i < SF.Length; i++)
                {
                    comboBox1.Items.Add(SF[i].name);
                }
            }
            catch{
                MessageBox.Show("Не удалось считать данные из файла настроек 'Settings.ini'.");
                return;
            }
        }

        private void Option_FormClosing(object sender, FormClosingEventArgs e)
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
                set.user = colorDialog1.Color;
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
            if (radioButton1.Checked) { set.csize = 0; set.user = Color.Empty; }
            if (radioButton2.Checked) { set.csize = 1; set.user = Color.Empty; }
            if (radioButton3.Checked) { set.csize = 2; set.user = Color.Empty; }
            if (radioButton4.Checked) { set.csize = 3; set.user = Color.Empty; }
            if (radioButton5.Checked) { set.csize = 4; set.user = Color.Empty; }
            if (radioButton6.Checked) { set.csize = 5; set.user = Color.Empty; }
            if (radioButton7.Checked) { set.csize = 6; set.user = Color.Empty; }
            if (radioButton8.Checked) { set.csize = 7; set.user = Color.Empty; }
            if (radioButton9.Checked) { set.csize = 8; set.user = Color.Empty; }
        }

        private void Option_Load(object sender, EventArgs e)
        {
            set = _parent.display.Set;
            buf = new Settings();
        }

        private void buttonOption7_Click(object sender, EventArgs e)
        {
            set.Copy(buf);
        }

        private void synchronous()
        {
            numericUpDown1.Value = (decimal)set.alfa;
            numericUpDown2.Value = (decimal)set.beta;
            numericUpDown3.Value = (decimal)set.gamma;
            numericUpDown4.Value = (decimal)set.delta;
            numericUpDown5.Value = (decimal)set.eps;
            numericUpDown6.Value = (decimal)set.msize;
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
            set.eps = (float)numericUpDown5.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            set.delta = (float)numericUpDown4.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            set.gamma = (float)numericUpDown3.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            set.beta = (float)numericUpDown2.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            set.alfa = (float)numericUpDown1.Value;
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
            set.msize = (int)numericUpDown6.Value;
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
            set.alfa = SF[number].a;
            set.beta = SF[number].b;
            set.gamma = SF[number].g;
            set.delta = SF[number].d;
            set.eps = SF[number].e;
            set.msize = SF[number].m;
            set.Sr = SF[number].s;
            set.ODA = SF[number].oda;
            set.user = SF[number].csize;
            //synchronous();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < SF.Length; i++)
            {
                if (comboBox1.Text == SF[i].name)
                {
                    set.alfa = SF[i].a;
                    set.beta = SF[i].b;
                    set.gamma = SF[i].g;
                    set.delta = SF[i].d;
                    set.eps = SF[i].e;
                    set.msize = SF[i].m;
                    set.Sr = SF[i].s;
                    set.ODA = SF[i].oda;
                    set.user = SF[i].csize;
                    synchronous();
                    number = i;
                }
            }
        }

        private void buttonOption12_Click(object sender, EventArgs e)
        {
            SF = add(textBox1.Text);
            comboBox1.Items.Add(textBox1.Text);
            write();
            textBox1.Clear();
        }

        public settings_file[] add(string name)
        {
            settings_file[] buf = new settings_file[SF.Length + 1];
            int i;
            for (i = 0; i < SF.Length; i++)
            {
                buf[i] = new settings_file();
                buf[i].a = SF[i].a;
                buf[i].b = SF[i].b;
                buf[i].g = SF[i].g;
                buf[i].d = SF[i].d;
                buf[i].e = SF[i].e;
                buf[i].m = SF[i].m;
                buf[i].s = SF[i].s;
                buf[i].oda = SF[i].oda;
                buf[i].name = SF[i].name;
                buf[i].csize = SF[i].csize;

            }
            buf[i] = new settings_file();
            buf[i].a = (int)set.alfa;
            buf[i].b = (int)set.beta;
            buf[i].g = (int)set.gamma;
            buf[i].d = (int)set.delta;
            buf[i].e = (int)set.eps;
            buf[i].m = set.msize;
            buf[i].s = (int)set.Sr;
            buf[i].oda = set.ODA;
            buf[i].name = name;
            buf[i].csize = set.user;
            return buf;
        }

        public void write()
        {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "/settings.ini",false);
            for (int i = 0; i < SF.Length; i++)
            {
                sw.WriteLine(SF[i].name + " " +
                    SF[i].a.ToString() + " " +
                    SF[i].b.ToString() + " " +
                    SF[i].g.ToString() + " " +
                    SF[i].d.ToString() + " " +
                    SF[i].e.ToString() + " " +
                    SF[i].m.ToString() + " " +
                    SF[i].s.ToString() + " " +
                    SF[i].oda.ToString() + " " +
                    SF[i].csize.ToArgb());
            }
            sw.Close();
        }

        private void buttonOption15_Click(object sender, EventArgs e)
        {
            DemoClick = !DemoClick;
        }

    }
}
