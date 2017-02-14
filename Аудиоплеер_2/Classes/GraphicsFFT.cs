using System;
using Tao.OpenGl;
using Tao.Platform.Windows;
using Tao.FreeGlut;
using System.Drawing;

namespace Аудиоплеер_2.Classes
{
    public class GraphicsFFT
    {
        private SimpleOpenGlControl ogl;
        private int fft_lenght = 4096;
        public float[] FFT;
        public Settings Set = new Settings();

        private float sDelta = 0.1f;
        private int sLen;

        //координаты
        private float[] sVector;
        //цвет точек
        private float[] sColor;
        //цветовые каналы
        private float[] rgba;
        //точка зрения
        private double x1 = 0, y1 = 0, z1 = 0;

        public GraphicsFFT(SimpleOpenGlControl form)
        {
            ogl = form;
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGBA | Glut.GLUT_DEPTH | Glut.GLUT_DOUBLE);
            FFT = new float[fft_lenght];

            sLen = (int)((float)Math.PI / sDelta + 0.5) * (int)(2 * (float)Math.PI / sDelta + 0.5) * 3;
            sVector = new float[sLen + 1];
            //цвет точек
            sColor = new float[sLen / 3 * 4];
            //цветовые каналы
            rgba = new float[4];
        }

        public void Init_OGL()
        {
            Gl.glClearColor(0f, 0f, 0f, 1);
            Gl.glViewport(0, 0, ogl.Width, ogl.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            Glu.gluPerspective(45, 1, 0.1, 500.0);
            Gl.glOrtho(
                -ogl.Width / 2.0, 
                ogl.Width / 2.0, 
                -ogl.Height / 2.0, 
                ogl.Height / 2.0, 
                0.1, 
                fft_lenght);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_POLYGON_SMOOTH);
            Gl.glHint(Gl.GL_POLYGON_SMOOTH_HINT, Gl.GL_NICEST);
        }

        public void DrawOGL_Sphere()
        {
            if (FFT != null)
            {
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
                Gl.glLoadIdentity();
                camera();
                Gl.glColor3d(1, 1, 1);

                int sId = 0, sC = 0, idf = 0;
                float sAnglF = 0, sAnglT = 0; 

                while (sAnglT <= Math.PI)
                {
                    sAnglF = 0;
                    while (sAnglF <= 2 * Math.PI)
                    {
                        if (sId == 0)
                        {
                            sVector[sId] = 0;
                            sVector[sId + 1] = 0;
                            sVector[sId + 2] = 0;
                        }
                        else
                        {
                            sVector[sId] = (FFT[idf] * Set.Msize * 10 + 1.0f) * Set.Sr 
                                * (float)Math.Sin(Set.Alfa * sAnglT) 
                                * (float)Math.Cos(Set.Beta * sAnglF);

                            sVector[sId + 1] = (FFT[idf] * Set.Msize * 10 + 1.0f) * Set.Sr 
                                * (float)Math.Sin(Set.Gamma * sAnglT) 
                                * (float)Math.Sin(Set.Delta * sAnglF);

                            sVector[sId + 2] = (FFT[idf] * Set.Msize * 10 + 1.0f) * Set.Sr 
                                * (float)Math.Cos(Set.Eps * sAnglT);
                        }   
                        if (Set.User == Color.Empty)
                        {
                            rgba = getColor_fft(rgba, FFT[idf] * 1000, Set.Csize);
                        }
                        else
                        {
                            rgba = getColor_fft_user(rgba, FFT[idf], Set.User);
                        }
                        sColor[sC] = rgba[0];
                        sColor[sC + 1] = rgba[1];
                        sColor[sC + 2] = rgba[2];
                        sColor[sC + 3] = rgba[3];

                        sC += 4;
                        sId += 3;
                        sAnglF += sDelta;
                        idf++;
                        if (idf >= fft_lenght) idf = 0;

                    }
                    if (sId + 3 >= sLen) break;
                    sAnglT += sDelta;
                }


                Gl.glEnableClientState(Gl.GL_COLOR_ARRAY);
                Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);

                Gl.glColorPointer(4, Gl.GL_FLOAT, 0, sColor);
                Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, sVector);
                
                switch (Set.ODA)
                {
                    case 0:
                        {
                            Gl.glDrawArrays(Gl.GL_QUADS, 0, sLen / 3);
                            break;
                        }
                    case 1:
                        {
                            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, sLen / 3);
                            break;
                        }
                    case 2:
                        {
                            Gl.glDrawArrays(Gl.GL_TRIANGLE_STRIP, 0, sLen / 3);
                            break;
                        }
                    case 3:
                        {
                            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 0, sLen / 3);
                            break;
                        }
                    case 4:
                        {
                            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, sLen / 3);
                            break;
                        }
                }
                Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
                Gl.glDisableClientState(Gl.GL_COLOR_ARRAY);
                Gl.glFlush();

                ogl.Invoke(new Action(()=> ogl.Invalidate()));
            }
        }
        

        private float[] getColor_fft(float []RGBA, float ampl, int c)
        {
            RGBA[3] = 1;
            switch (c)
            {
                case 0:
                    {
                        //фиолетовый
                        if (ampl > 0 && ampl <= 1)
                        {
                            RGBA[0] = ampl;
                            RGBA[1] = 0;
                            RGBA[2] = ampl;

                        }
                        //синий
                        if (ampl > 1 && ampl <= 10)
                        {
                            RGBA[0] = 0;
                            RGBA[1] = 0;
                            RGBA[2] = ampl;
                        }
                        //голубой
                        if (ampl > 10 && ampl <= 20)
                        {
                            RGBA[0] = 0;
                            RGBA[1] = ampl;
                            RGBA[2] = ampl;
                        }
                        //зеленый
                        if (ampl > 20 && ampl <= 30)
                        {
                            RGBA[0] = 0;
                            RGBA[1] = ampl;
                            RGBA[2] = 0;
                        }
                        //желтый
                        if (ampl > 30 && ampl <= 40)
                        {
                            RGBA[0] = ampl;
                            RGBA[1] = ampl;
                            RGBA[2] = 0;
                        }
                        //оранжевый
                        if (ampl > 40 && ampl <= 50)
                        {
                            RGBA[0] = ampl;
                            RGBA[1] = ampl / 2;
                            RGBA[2] = 0;
                        }
                        //красный
                        if (ampl > 50 && ampl <= 60)
                        {
                            RGBA[0] = ampl;
                            RGBA[1] = 0;
                            RGBA[2] = 0;
                        }
                        break;
                    }
                case 1:
                    {
                        RGBA[0] = 0.255f * ampl / 5.0f;
                        RGBA[1] = 0;
                        RGBA[2] = 0.255f * ampl / 5.0f;
                        break;
                    }
                case 2:
                    {
                        RGBA[0] = 0;
                        RGBA[1] = 0;
                        RGBA[2] = 0.255f * ampl / 5.0f;
                        break;
                    }
                case 3:
                    {
                        RGBA[0] = 0;
                        RGBA[1] = 0.255f * ampl / 5.0f;
                        RGBA[2] = 0.255f * ampl / 5.0f;
                        break;
                    }
                case 4:
                    {
                        RGBA[0] = 0;
                        RGBA[1] = 0.255f * ampl / 5.0f;
                        RGBA[2] = 0;
                        break;
                    }
                case 5:
                    {
                        RGBA[0] = 0.255f * ampl / 2.0f;
                        RGBA[1] = 0.255f * ampl / 2.0f;
                        RGBA[2] = 0;
                        break;
                    }
                case 6:
                    {
                        RGBA[0] = 0.255f * ampl;
                        RGBA[1] = 0.255f * ampl / 2.0f;
                        RGBA[2] = 0;
                        break;
                    }
                case 7:
                    {
                        RGBA[0] = 0.255f * ampl / 5.0f;
                        RGBA[1] = 0;
                        RGBA[2] = 0;
                        break;
                    }
                case 8:
                    {
                        RGBA[0] = 0.255f * ampl;
                        RGBA[1] = 0.255f * ampl;
                        RGBA[2] = 0.255f * ampl;
                        break;
                    }
            }
            return RGBA;
        }

        private float[] getColor_fft_user(float[] RGBA, float ampl, Color c)
        {
            RGBA[0] = c.R * ampl;
            RGBA[1] = c.G * ampl;
            RGBA[2] = c.B * ampl;
            RGBA[3] = 1;
            return RGBA;
        }

        private double[] r_d(double r, double t, double f)
        {
            double x, y, z;
            x = r * Math.Sin(t / 180 * Math.PI) * Math.Cos(f / 180 * Math.PI);
            y = r * Math.Sin(t / 180 * Math.PI) * Math.Sin(f / 180 * Math.PI);
            z = r * Math.Cos(t / 180 * Math.PI);
            double[] b = new double[3];
            b[0] = x;
            b[1] = y;
            b[2] = z;
            return b;
        }

        private double[] d_r(double x, double y, double z)
        {
            double r, t, f;
            r = Math.Sqrt(x * x + y * y + z * z);
            t = Math.Atan(Math.Sqrt(x * x + y * y) / z);
            f = Math.Atan(y / x);
            double[] a = new double[3];
            a[0] = r;
            a[1] = t;
            a[2] = f;
            return a;

        }

        private void camera()
        {
            double[] w = r_d(Set.R, Set.Tet, Set.Fi);
            x1 = w[0];
            y1 = w[1];
            z1 = w[2];
            Glu.gluLookAt(x1, y1, z1, 0, 0, 0, 0, 1, 0);
        }
    }
}
