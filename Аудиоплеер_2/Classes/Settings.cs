using System.Drawing;

namespace Аудиоплеер_2.Classes
{
    public class Settings
    {
        //стандартные цвета функции color_fft
        public int Csize;
        //размер - множитель амплитуд
        public int Msize;
        //радиус сферы
        public float Sr;
        //примитивы Opengl из функции DrawArrays
        public int ODA;
        //углы для сферы
        public float Alfa;
        public float Beta;
        public float Gamma;
        public float Delta;
        public float Eps;
        public Color User;
        //координаты в сферической системе
        public double Tet, Fi, R;

        public Settings()
        {
            Csize = 8;
            Msize = 3;
            Sr = 50;
            ODA = 2;
            Alfa = 1;
            Beta = 1;
            Gamma = 1;
            Delta = 1;
            Eps = 1;
            Tet = 179;
            Fi = -31;
            R = 20;
            User = Color.Empty;
        }

        public void Copy(Settings buffer)
        {
            buffer.Alfa = this.Alfa;
            buffer.Beta = this.Beta;
            buffer.Gamma = this.Gamma;
            buffer.Delta = this.Delta;
            buffer.Eps = this.Eps;
            buffer.Csize = this.Csize;
            buffer.Msize = this.Msize;
            buffer.Sr = this.Sr;
            buffer.ODA = this.ODA;

        }
        public void Camera(int i)
        {
            switch (i)
            {
                case 0:
                    {
                        Tet = 179;
                        Fi = -31;
                        R = 20;
                        break;
                    }
                case 1:
                    {
                        Tet = 210;
                        Fi = 210; 
                        R = 50;
                        break;
                    }
                case 2:
                    {
                        Tet = 180;
                        Fi = -42;
                        R = 20;
                        break;
                    }
                case 3:
                    {
                        Tet = 429;
                        Fi = -116;
                        R = 20;
                        break;
                    }
                case 4:
                    {
                        Tet = 572;
                        Fi = -92;
                        R = 20;
                        break;
                    }
                case 5:
                    {
                        Tet = 572;
                        Fi = -33;
                        R = 608;
                        break;
                    }
                case 6:
                    {
                        Tet = 932;
                        Fi = -85;
                        R = 380;
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
