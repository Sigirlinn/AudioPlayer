using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Tao.Platform.Windows;
using Tao.FreeGlut;
using System.Drawing;

namespace Аудиоплеер_2.Classes
{
    public class GraphicsFFT
    {
        SimpleOpenGlControl OGL;
        int fft_lenght = 4096;
        public float[] fft;
        public Settings Set = new Settings();


        float Sdelta = 0.1f;
        int Slen;

        //координаты
        float[] Svector;
        //цвет точек
        float[] Scolor;
        //цветовые каналы
        float[] rgba;
        //точка зрения
        double X1 = 0, Y1 = 0, Z1 = 0;

        public GraphicsFFT(SimpleOpenGlControl form)
        {
            OGL = form;
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGBA | Glut.GLUT_DEPTH | Glut.GLUT_DOUBLE);
            fft = new float[fft_lenght];

            Slen = (int)((float)Math.PI / Sdelta + 0.5) * (int)(2 * (float)Math.PI / Sdelta + 0.5) * 3;
            Svector = new float[Slen + 1];
            //цвет точек
            Scolor = new float[Slen / 3 * 4];
            //цветовые каналы
            rgba = new float[4];
        }

        public void Init_OGL()
        {
            Gl.glClearColor(0f, 0f, 0f, 1);
            Gl.glViewport(0, 0, OGL.Width, OGL.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            Glu.gluPerspective(45, 1, 0.1, 500.0);
            Gl.glOrtho(
                -OGL.Width / 2.0, 
                OGL.Width / 2.0, 
                -OGL.Height / 2.0, 
                OGL.Height / 2.0, 
                0.1, 
                fft_lenght);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_POLYGON_SMOOTH);
            Gl.glHint(Gl.GL_POLYGON_SMOOTH_HINT, Gl.GL_NICEST);
        }

        public void drawOGL_Sphere()
        {
            if (fft != null)
            {
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
                Gl.glLoadIdentity();
                Camera();
                Gl.glColor3d(1, 1, 1);

                int Sid = 0, Sc = 0, idf = 0;
                float SanglF = 0, SanglT = 0; 

                while (SanglT <= Math.PI)
                {
                    SanglF = 0;
                    while (SanglF <= 2 * Math.PI)
                    {
                        if (Sid == 0)
                        {
                            Svector[Sid] = 0;
                            Svector[Sid + 1] = 0;
                            Svector[Sid + 2] = 0;
                        }
                        else
                        {
                            Svector[Sid] = (fft[idf] * Set.msize * 10 + 1.0f) * Set.Sr 
                                * (float)Math.Sin(Set.alfa * SanglT) 
                                * (float)Math.Cos(Set.beta * SanglF);

                            Svector[Sid + 1] = (fft[idf] * Set.msize * 10 + 1.0f) * Set.Sr 
                                * (float)Math.Sin(Set.gamma * SanglT) 
                                * (float)Math.Sin(Set.delta * SanglF);

                            Svector[Sid + 2] = (fft[idf] * Set.msize * 10 + 1.0f) * Set.Sr 
                                * (float)Math.Cos(Set.eps * SanglT);
                        }   
                        if (Set.user == Color.Empty)
                        {
                            rgba = Color_fft(rgba, fft[idf] * 1000, Set.csize);
                        }
                        else
                        {
                            rgba = Color_fft_user(rgba, fft[idf], Set.user);
                        }
                        Scolor[Sc] = rgba[0];
                        Scolor[Sc + 1] = rgba[1];
                        Scolor[Sc + 2] = rgba[2];
                        Scolor[Sc + 3] = rgba[3];

                        Sc += 4;
                        Sid += 3;
                        SanglF += Sdelta;
                        idf++;
                        if (idf >= fft_lenght) idf = 0;

                    }
                    if (Sid + 3 >= Slen) break;
                    SanglT += Sdelta;
                }


                Gl.glEnableClientState(Gl.GL_COLOR_ARRAY);
                Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);

                Gl.glColorPointer(4, Gl.GL_FLOAT, 0, Scolor);
                Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, Svector);
                
                switch (Set.ODA)
                {
                    case 0:
                        {
                            Gl.glDrawArrays(Gl.GL_QUADS, 0, Slen / 3);
                            break;
                        }
                    case 1:
                        {
                            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, Slen / 3);
                            break;
                        }
                    case 2:
                        {
                            Gl.glDrawArrays(Gl.GL_TRIANGLE_STRIP, 0, Slen / 3);
                            break;
                        }
                    case 3:
                        {
                            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 0, Slen / 3);
                            break;
                        }
                    case 4:
                        {
                            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, Slen / 3);
                            break;
                        }
                }
                Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
                Gl.glDisableClientState(Gl.GL_COLOR_ARRAY);
                Gl.glFlush();

                OGL.Invoke(new Action(()=> OGL.Invalidate()));
            }
        }
        

        private float[] Color_fft(float []RGBA, float ampl, int c)
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

        private float[] Color_fft_user(float[] RGBA, float ampl, Color c)
        {
            RGBA[0] = c.R * ampl;
            RGBA[1] = c.G * ampl;
            RGBA[2] = c.B * ampl;
            RGBA[3] = 1;
            return RGBA;
        }

        private double[] R_D(double r, double t, double f)
        {
            double x, y, z;
            x = r * Math.Sin(t / 180 * Math.PI) * Math.Cos(f / 180 * Math.PI);
            y = r * Math.Sin(t / 180 * Math.PI) * Math.Sin(f / 180 * Math.PI);
            z = r * Math.Cos(t / 180 * Math.PI);
            double[] B = new double[3];
            B[0] = x;
            B[1] = y;
            B[2] = z;
            return B;
        }

        private double[] D_R(double x, double y, double z)
        {
            double r, t, f;
            r = Math.Sqrt(x * x + y * y + z * z);
            t = Math.Atan(Math.Sqrt(x * x + y * y) / z);
            f = Math.Atan(y / x);
            double[] A = new double[3];
            A[0] = r;
            A[1] = t;
            A[2] = f;
            return A;

        }

        private void Camera()
        {
            double[] w = R_D(Set.r, Set.tet, Set.fi);
            X1 = w[0];
            Y1 = w[1];
            Z1 = w[2];
            Glu.gluLookAt(X1, Y1, Z1, 0, 0, 0, 0, 1, 0);
        }
    }
}
