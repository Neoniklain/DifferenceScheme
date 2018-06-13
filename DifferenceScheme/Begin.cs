using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Tao.FreeGlut;
using Tao.OpenGl;
using NCalc;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace DifferenceScheme
{
    public partial class Begin : Form
    {
        [DllImport("user32.dll")]
        public static extern int ShowCursor(bool bShow);
        /// <summary>
        /// Функция
        /// </summary>
        protected Expression Fx;
        /// <summary>
        /// Граничное условие
        /// </summary>
        protected Expression Psi;
        /// <summary>
        /// Колличество итераций по времени
        /// </summary>
        protected int nt;
        /// <summary>
        /// Колличество итераций по пространству
        /// </summary>
        protected int n;
        /// <summary>
        /// Матрица Значений
        /// </summary>
        protected double[,] U;
        /// <summary>
        /// Конечное значение времени 
        /// </summary>
        double T = 1;
        /// <summary>
        /// Шаг
        /// </summary>
        double h;

        double A;
        double B;
        double C;
        protected double kappa;
        protected double tau;
        protected int K_slow;
        protected int K_length;

        protected int current_step = 0;

        /// Блок переменных для управления изображением
        protected int far = -3;
        protected float horizont = -0.5f;
        protected float vertical = -0.5f;
        protected float last_X = 0;
        protected float last_Y = 0;
        protected float last_Xr = 0;
        protected float last_Yr = 0;
        protected int fi = 70;
        protected int tu = 0;
        protected int tr = 0;
        protected int k = 0;

        Stopwatch sw;

        private BufferedGraphicsContext bufferContext = BufferedGraphicsManager.Current;

        public Begin()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            this.UpdateStyles();
            InitializeComponent();
            anT.InitializeContexts();
            anT.MouseWheel += new MouseEventHandler(this_MouseWheel);
           
        }

        /// <summary>
        /// Запуск рассчетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Click(object sender, EventArgs e)
        {
            variableInit();
            k = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    U[i, j] = Func(i * h, j * h);
                    // Граничные значения
                    U[0, j] = Func(0, j * h);
                    U[n - 1, j] = Func((n - 1) * h, j * h);
                }
                // Граничные значения
                U[i, 0] = Func(i * h, 0);
                U[i, n - 1] = Func(i * h, (n - 1) * h);
            }
            Print();

        }

        /// <summary>
        /// Тест скорости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Test_Click(object sender, EventArgs e)
        {
            double perfomance = 0;
            int maxCount = Convert.ToInt32(TestMax.Text) + 100;
            for (int count = Iter.Checked ? 100 : (maxCount - 100); count < maxCount; count+=100)
            {
                try
                {
                    nt = 1;
                    n = count;
                    h = (double)1 / n;
                    K_slow = Convert.ToInt32(k_slow.Text);
                    T = Convert.ToInt32(k_length.Text);

                }
                catch { MessageBox.Show("В графе 'Шаг' допустимы только числа"); return; }
                try
                {
                    Fx = new Expression(Fx_box.Text);
                }
                catch { MessageBox.Show("Формула функции не верна"); return; }

                h = (double)1 / n;
                U = new double[n, n];
                tau = (T / nt);
                kappa = (tau / (h * h * 2)) / 10000000;
                A = -kappa * 10000000;
                B = 1 + (2 * kappa) * 10000000;
                C = -kappa * 10000000;

                k = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        U[i, j] = Func(i * h, j * h);
                        // Граничные значения
                        U[0, j] = Func(0, j * h);
                        U[n - 1, j] = Func((n - 1) * h, j * h);
                    }
                    // Граничные значения
                    U[i, 0] = Func(i * h, 0);
                    U[i, n - 1] = Func(i * h, (n - 1) * h);
                }

                sw = new Stopwatch();
                sw.Start();
                if(Paral.Checked)
                    ParallConsolePrint();
                else
                    ConsolePrint();
                sw.Stop();
                perfomance += sw.ElapsedMilliseconds;
                if (Paral.Checked)
                    System.Console.WriteLine("Параллельно Колличество точек = " + n + " Время =  " + (perfomance / 1000.0).ToString() + " сек.");
                else
                    System.Console.WriteLine("Последовательно Колличество точек = " + n + " время =  " + (perfomance / 1000.0).ToString() + " сек.");
            }
        }

        private void ParallConsolePrint()
        {
            if (k == (nt))
            {
                current_step = k;
                U = ParallSolve(U, current_step);
            }

            for (; k < nt; k++)
            {
                current_step = k;
                U = ParallSolve(U, current_step);
            }
        }
        private void ConsolePrint()
        {
            if (k == (nt))
            {
                current_step = k;
                U = Solve(U, current_step);
            }

            for (; k < nt; k++)
            {
                current_step = k;
                U = Solve(U, current_step);
            }
        }

        private void Print()
        {
            try
            {
                anT.InitializeContexts();
                Glu.gluPerspective(60.0f, (anT.Width - 100) / anT.Height, 0.01f, 800.0f);

                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
                Gl.glTranslatef(horizont / 10, vertical / 10, far);
                Gl.glRotated(fi, -1, 0, 0);
                Gl.glRotated(tu,  0, 0, -1);

                if(k == (nt))
                {
                    current_step = k;
                    Display();
                    anT.SwapBuffers();
                }

                for (;k<nt; k++)
                {
                    current_step = k;
                    Display();
                    anT.SwapBuffers();
                    Thread.Sleep(K_slow * 10);
                }
            }
            catch { }       
        }

        private void Display()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glColor3d(0.2, 0.2, 0.2);
            if (Paral.Checked)
                U = ParallSolve(U, current_step);
            else
                U = Solve(U, current_step);

            double max = max_of_mass(U);
            double min = min_of_mass(U);
            double ed = 1 / (max - min);


            for (int i = -10; i < 50; i++)
            {
                Gl.glBegin(Gl.GL_LINE_STRIP);
                Gl.glVertex3d(100, i, 0);
                Gl.glVertex3d(-100, i, 0);
                Gl.glEnd();
            }
            for (int i = -10; i < 50; i++)
            {
                Gl.glBegin(Gl.GL_LINE_STRIP);
                Gl.glVertex3d(i, 100, 0);
                Gl.glVertex3d(i, -100, 0);
                Gl.glEnd();
            }
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex3d(0, 0, 100);
            Gl.glVertex3d(0, 0, -100);
            Gl.glEnd();            


            Gl.glColor3d(225.1d, 20, 20);
            for (int i = 0; i < n; i++)
            {
                Gl.glBegin(Gl.GL_LINE_STRIP);
                for (int j = 0; j < n; j++)
                {
                    Gl.glColor3d(ed * (U[i, j] - min), 0, ed * (max - U[i, j]));
                    Gl.glVertex3d(i * h, j * h, U[i, j]);
                }
                Gl.glEnd();
            }

            for (int j = 0; j < n; j++)
            {
                Gl.glBegin(Gl.GL_LINE_STRIP);
                for (int i = 0; i < n; i++)
                {
                    Gl.glColor3d(ed * (U[i, j] - min), 0, ed * (max - U[i, j]));
                    Gl.glVertex3d(i * h, j * h, U[j, i]);
                }
                Gl.glEnd();
            }
        }

        private void writeMatrix(double[,] U0)
        {
            string writePath = @"E:\text.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {

                   /* sw.WriteLine("Переменные: ");
                    sw.WriteLine("Колличество_итераций_по_времени: "+ nt);
                    sw.WriteLine("Колличество_точек: " + n);
                    sw.WriteLine("Шаг: " + h);
                    sw.WriteLine("Тау: " + tau);
                    sw.WriteLine("Каппа: " + kappa);

                    sw.WriteLine("Матрица: ");*/
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            sw.Write(U0[i, j] + " ");
                        }
                        sw.WriteLine(" ");
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        double[,] Solve(double[,] U0, int step)
        {
            double t2 = step * tau + tau / 2; //n + 1/2 
            double t1 = (step + 1) * tau; //n + 1 

            double[,] U05 = new double[n, n];
            if (step == 0)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == 0)
                        {
                            U0[i,j] = Func(i * h, j * h, step);
                        }
                        else if (i == n - 1)
                        {
                            U0[i,j] = Func(i * h, j * h, step);
                        }
                        else
                        {
                            if (j == 0)
                            {
                                U0[i,j] = Func(i * h, j * h, step);
                            }
                            else if (j == n - 1)
                            {
                                U0[i,j] = Func(i * h, j * h, step);
                            }
                            else
                            {
                                U0[i,j] = _Psi(i * h, j * h);
                            }
                        }
                    }
                }
            }
            else
            {
                if (U0 != null)
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            U05[i,j] = U0[i,j];
                        }
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    U05[0,i] = Func(0, i * h, t2);
                    U05[n - 1,i] = Func((n - 1) * h, i * h, t2);
                    U05[i,0] = Func(i * h, 0, t2);
                    U05[i,n - 1] = Func(i * h, (n - 1) * h, t2);
                }

                for (int i = 1; i < n - 1; i++)
                {
                    solveMatrix(ref U0, ref U05, i, t2, true);
                }

                for (int i = 0; i < n; i++)
                {
                    U0[0,i] = Func(0, i * h, t1);
                    U0[n - 1,i] = Func((n - 1) * h, i * h, t1);
                    U0[i,0] = Func(i * h, 0, t1);
                    U0[i,n - 1] = Func(i * h, (n - 1) * h, t1);
                }
                //Инициализация граничных значений 
                for (int i = 1; i < n - 1; i++)
                {
                    solveMatrix(ref U05, ref U0, i, t1, false);
                }
            }
            return U0;
        }

        double[,] ParallSolve(double[,] U0, int step)
        {
            double t2 = step * tau + tau / 2; //n + 1/2 
            double t1 = (step + 1) * tau; //n + 1 

            double[,] U05 = new double[n, n];
            if (step == 0)
            {
                Parallel.For(1, n - 1, i =>
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == 0)
                        {
                            U0[i, j] = Func(i * h, j * h, step);
                        }
                        else if (i == n - 1)
                        {
                            U0[i, j] = Func(i * h, j * h, step);
                        }
                        else
                        {
                            if (j == 0)
                            {
                                U0[i, j] = Func(i * h, j * h, step);
                            }
                            else if (j == n - 1)
                            {
                                U0[i, j] = Func(i * h, j * h, step);
                            }
                            else
                            {
                                U0[i, j] = _Psi(i * h, j * h);
                            }
                        }
                    }
                });
            }
            else
            {
                if (U0 != null)
                {
                    Parallel.For(0, n, i =>
                    {
                        for (int j = 0; j < n; j++)
                        {
                            U05[i, j] = U0[i, j];
                        }
                    });
                }

                for (int i = 0; i < n; i++)
                {
                    U05[0, i] = Func(0, i * h, t2);
                    U05[n - 1, i] = Func((n - 1) * h, i * h, t2);
                    U05[i, 0] = Func(i * h, 0, t2);
                    U05[i, n - 1] = Func(i * h, (n - 1) * h, t2);
                };

                for (int i = 1; i < n - 1; i++)
                {
                    ParallelsolveMatrix(ref U0, ref U05, i, t2, true);
                };

                for (int i = 0; i < n; i++)
                {
                    U0[0, i] = Func(0, i * h, t1);
                    U0[n - 1, i] = Func((n - 1) * h, i * h, t1);
                    U0[i, 0] = Func(i * h, 0, t1);
                    U0[i, n - 1] = Func(i * h, (n - 1) * h, t1);
                };

                for (int i = 1; i < n - 1; i++)
                {
                    ParallelsolveMatrix(ref U05, ref U0, i, t1, false);
                };
            }
            return U0;
        }

        /// <summary>
        /// Прогонка
        /// </summary>
        /// <param name="oldM"></param>
        /// <param name="newM"></param>
        /// <param name="index"></param>
        /// <param name="time"></param>
        /// <param name="isVertical"></param>
        private void solveMatrix(ref double[,] oldM, ref double[,] newM, int index, double time, bool isVertical)
        {
            double[] X = new double[n];
            double[] Y = new double[n];
            double[] F = new double[n];
            X[0] = 0;
            for (int i = 0; i < n - 1; i++)
            {
                X[i + 1] = (-C) / (A * X[i] + B);
            }
            if (isVertical)
            {
                Y[0] = Func(0, index * h, time);

                for (int i = 0; i < n - 1; i++)
                {
                    F[i] = (kappa * oldM[i, index + 1] + (1 - 2 * kappa) * oldM[i, index] + kappa * oldM[i, index - 1]);
                    Y[i + 1] = (F[i] - A * Y[i]) / (A * X[i] + B);
                }
                for (int i = n - 2; i > 0; i--)
                {
                    newM[i, index] = X[i + 1] * newM[i + 1, index] + Y[i + 1];
                }
                F[n - 1] = (kappa * oldM[n - 1, index + 1] + (1 - 2 * kappa) * oldM[n - 1, index] + kappa * oldM[n - 1, index - 1]);
            }
            else
            {
                Y[0] = Func(index * h, 0, time);
                for (int i = 0; i < n - 1; i++)
                {
                    F[i] = (kappa * oldM[index + 1, i] + (1 - 2 * kappa) * oldM[index, i] + kappa * oldM[index - 1, i]);
                    Y[i + 1] = (F[i] - A * Y[i]) / (A * X[i] + B);
                }
                for (int i = n - 2; i > 0; i--)
                {
                    newM[index, i] = X[i + 1] * newM[index, i + 1] + Y[i + 1];
                }
                F[n - 1] = (kappa * oldM[index + 1, n - 1] + (1 - 2 * kappa) * oldM[index, n - 1] + kappa * oldM[index - 1, n - 1]);
            }

        }

        /// <summary>
        /// Прогонка
        /// </summary>
        /// <param name="oldM"></param>
        /// <param name="newM"></param>
        /// <param name="index"></param>
        /// <param name="time"></param>
        /// <param name="isVertical"></param>
        private void ParallelsolveMatrix(ref double[,] oldM, ref double[,] newM, int index, double time, bool isVertical)
        {
            double[] X = new double[n];
            double[] Y = new double[n];
            double[] F = new double[n];
            X[0] = 0;
            for (int i = 0; i < n - 1; i++)
            {
                X[i + 1] = (-C) / (A * X[i] + B);
            }
            if (isVertical)
            {
                Y[0] = Func(0, index * h, time);

                for (int i = 0; i < n - 1; i++)
                {
                    F[i] = (kappa * oldM[i, index + 1] + (1 - 2 * kappa) * oldM[i, index] + kappa * oldM[i, index - 1]);
                    Y[i + 1] = (F[i] - A * Y[i]) / (A * X[i] + B);
                }
                //var temp = oldM;
                //Parallel.ForEach(Partitioner.Create(0, n-1, n-1 / 4), (range, loopState) =>
                //  {
                //      for (int i = range.Item1; i < range.Item2; i++)
                //      {
                //          F[i] = (kappa * temp[i, index + 1] + (1 - 2 * kappa) * temp[i, index] + kappa * temp[i, index - 1]);
                //          Y[i + 1] = (F[i] - A * Y[i]) / (A * X[i] + B);
                //      }
                //  });
                //oldM = temp;

                for (int i = n - 2; i > 0; i--)
                {
                    newM[i, index] = X[i + 1] * newM[i + 1, index] + Y[i + 1];
                }
                F[n - 1] = (kappa * oldM[n - 1, index + 1] + (1 - 2 * kappa) * oldM[n - 1, index] + kappa * oldM[n - 1, index - 1]);
            }
            else
            {

                Y[0] = Func(index * h, 0, time);
                //var temp = oldM;
                //Parallel.ForEach(Partitioner.Create(0, n - 1, n - 1 / 4), (range, loopState) =>
                //{
                //    for (int i = range.Item1; i < range.Item2; i++)
                //    {
                //        F[i] = (kappa * temp[index + 1, i] + (1 - 2 * kappa) * temp[index, i] + kappa * temp[index - 1, i]);
                //        Y[i + 1] = (F[i] - A * Y[i]) / (A * X[i] + B);
                //    }
                //});
                //oldM = temp;
                for (int i = 0; i < n - 1; i++)
                {
                    F[i] = (kappa * oldM[index + 1, i] + (1 - 2 * kappa) * oldM[index, i] + kappa * oldM[index - 1, i]);
                    Y[i + 1] = (F[i] - A * Y[i]) / (A * X[i] + B);
                }
                for (int i = n - 2; i > 0; i--)
                {
                    newM[index, i] = X[i + 1] * newM[index, i + 1] + Y[i + 1];
                }
                F[n - 1] = (kappa * oldM[index + 1, n - 1] + (1 - 2 * kappa) * oldM[index, n - 1] + kappa * oldM[index - 1, n - 1]);

            }

        }

        /// <summary>
        /// Вычисление функции
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="t">T - время (шаг)</param>
        /// <returns></returns>
        private double Func(double x, double y, double t=1)
        {            
            try
            {
                Fx.Parameters["x"] = x;
                Fx.Parameters["y"] = y;
                Fx.Parameters["t"] = t;
                return Convert.ToDouble(Fx.Evaluate());
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return -0.12312315;
            }
        }

        /// <summary>
        /// Граничные значения
        /// </summary>
        /// <returns></returns>
        private double _Psi(double x, double y)
        {
            return Func(x, y, 0);
        }

        /// <summary>
        /// Инициализация значений
        /// </summary>
        private void variableInit()
        {            
            try
            {
                nt = Convert.ToInt32(nt_box.Text);
                n = Convert.ToInt32(N_box.Text);
                h = (double)1 / n;
                K_slow = Convert.ToInt32(k_slow.Text);
                T = Convert.ToInt32(k_length.Text); 

            }
            catch { MessageBox.Show("В графе 'Шаг' допустимы только числа"); return; }
            try
            {
                Fx = new Expression(Fx_box.Text);
            } catch { MessageBox.Show("Формула функции не верна"); return; }
            
            h = (double)1 / n;
            U = new double[n , n];
            tau = (T / nt);
            kappa =  (tau / (h * h * 2))/10000000;
            A = -kappa* 10000000;
            B =  1 + (2 * kappa)* 10000000;
            C = -kappa* 10000000;
            
        }
        
        /// Блок функция для управления изображением
        private void anT_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (last_X==last_Y && last_Y == 0)
                {
                    last_X = e.X;
                    last_Y = e.Y;
                    return;
                }
                var deltaX = (e.X- last_X) / Math.Abs(10);
                var deltaY = (last_Y- e.Y) / Math.Abs(10);
                if (Math.Abs(deltaX) >= 0.8 || Math.Abs(deltaY) >= 0.8)
                {
                    horizont += deltaX;
                    vertical += deltaY;
                    Print();
                    last_X = e.X;
                    last_Y = e.Y;
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (last_X == last_Y && last_Y == 0)
                {
                    last_X = e.X;
                    last_Y = e.Y;
                    return;
                }
                var deltaX = (last_Xr - e.X) / Math.Abs(10);
                var deltaY = (last_Yr - e.Y) / Math.Abs(10);
                if (Math.Abs(deltaX) >= 1 || Math.Abs(deltaY) >= 1)
                {
                    fi += Convert.ToInt32(deltaY)*3;
                    tu += Convert.ToInt32(deltaX)*3;
                    Print();
                    last_Xr = e.X;
                    last_Yr = e.Y;
                }
            }
        }
        private void ant_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowCursor(true);
            }
        }
        private void ant_MouseDown(object sender, MouseEventArgs e)
        {

            last_Xr = e.X;
            last_Yr = e.Y;
            last_X = e.X;
            last_Y = e.Y;
            if (e.Button == MouseButtons.Left)
            {
                ShowCursor(false);
            }
        }
        private void this_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                far += 1;
            else
                far -= 1;

            if (e.Delta != 0)
            {
                Print();
            }
        }

        //Максимальное значение в массиве (для цвета температуры) 
        double max_of_mass(double[,] ms)
        {
            double temp = -1000000000;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (ms[i,j] > temp)
                        temp = ms[i,j];
                }
            }
            return temp;
        }

        //Минимальное значение в массиве (для цвета температуры) 
        double min_of_mass(double[,] ms)
        {
            double temp = 1000000000;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (ms[i,j] < temp)
                        temp = ms[i,j];
                }
            }
            return temp;
        }

    }
}
