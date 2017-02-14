using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Аудиоплеер_2.Classes
{
    public class Settings
    {
        //стандартные цвета функции color_fft
        public int csize;
        //размер - множитель амплитуд
        public int msize;
        //радиус сферы
        public float Sr;
        //примитивы Opengl из функции DrawArrays
        public int ODA;
        //углы для сферы
        public float alfa;
        public float beta;
        public float gamma;
        public float delta;
        public float eps;
        public Color user;
        //координаты в сферической системе
        public double tet, fi, r;

        public Settings()
        {
            csize = 8;
            msize = 3;
            Sr = 50;
            ODA = 2;
            alfa = 1;
            beta = 1;
            gamma = 1;
            delta = 1;
            eps = 1;
            tet = 179;
            fi = -31;
            r = 20;
            Color user = Color.Empty;
        }

        public void Copy(Settings buffer)
        {
            buffer.alfa = this.alfa;
            buffer.beta = this.beta;
            buffer.gamma = this.gamma;
            buffer.delta = this.delta;
            buffer.eps = this.eps;
            buffer.csize = this.csize;
            buffer.msize = this.msize;
            buffer.Sr = this.Sr;
            buffer.ODA = this.ODA;

        }
        public void Camera(int i)
        {
            switch (i)
            {
                case 0:
                    {
                        tet = 179;
                        fi = -31;
                        r = 20;
                        break;
                    }
                case 1:
                    {
                        tet = 210;
                        fi = 210; 
                        r = 50;
                        break;
                    }
                case 2:
                    {
                        tet = 180;
                        fi = -42;
                        r = 20;
                        break;
                    }
                case 3:
                    {
                        tet = 429;
                        fi = -116;
                        r = 20;
                        break;
                    }
                case 4:
                    {
                        tet = 572;
                        fi = -92;
                        r = 20;
                        break;
                    }
                case 5:
                    {
                        tet = 572;
                        fi = -33;
                        r = 608;
                        break;
                    }
                case 6:
                    {
                        tet = 932;
                        fi = -85;
                        r = 380;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }
    }
}
