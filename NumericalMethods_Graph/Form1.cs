using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumericalMethods_Graph
{
    public partial class MainForm : Form
    {
        public static int m = 10;
        public static double[] xValues = new double[m + 1];
        public static double[] fValues = new double[m + 1];
        public static List<double> errors = new List<double>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void pictureButton_Click(object sender, EventArgs e)
        {
            NumericalMethods();

            this.chart.Series[0].Points.Clear();

            for (int i = 0; i < xValues.Length; i++)
            {
                this.chart.Series[0].Points.AddXY(xValues[i], fValues[i]);
            }

            for (int i = 1; i < m; i++)
            {
                this.chart2.Series[0].Points.AddXY(xValues[i], errors[i - 1]);
            
            }
            
        }

        static void NumericalMethods()
        {
            double x, q, An, sum;
            int n;
            int a = 0, b = 4, N = 5;
            double h = (double)(b - a) / N;

            for (int i = 0; i < m; i++)
            {
                xValues[i] = i * h;
            }

            int index = 0;
            for (x = a; x <= b; x += h)
            {
                fValues[index] = F(x);
                index++;
            }

            h = (double)(b - a) / m;
            float max = 0;
           
            Console.WriteLine("x\t\tf(x)\t\tLnX\t\terr");
            
            for (x = a; x <= b; x += h)
            {
                float lagrange = (float)Lagrange(xValues, fValues, 6, x);
                float fx = (float)F(x);
                float error = Math.Abs(lagrange - fx);
                errors.Add(error);

                if (error > max)
                    max = error;

                Console.WriteLine(Math.Round(x, 1) + "\t\t" + fx + "\t" +
                lagrange + "\t" + error);
            }
            Console.WriteLine("max|err| = " + max);

            Console.ReadLine();


        }

        public static double F(double x)// считаем F(x)
        {
            int n = 0;
            double sum = 0, q;
            double An = x;
            double eps = Math.Pow(10, -6);

            while (Math.Abs(An) >= eps)
            {
                sum += An;

                q = ((-1) * x * x * (2 * n + 1)) / ((2 * n + 3) * (2 * n + 3) * (2 * n + 2));

                An *= q;

                n++;
            }

            return sum;
        }

        public static double Lagrange(double[] xValues, double[] fValues, int n, double argX)
        {
            double con, LnX = 0;

            for (int i = 0; i < n; i++)
            {
                con = 1;

                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                        con *= (argX - xValues[j]) / (xValues[i] - xValues[j]);
                }

                LnX += con * fValues[i];
            }

            return LnX;
        }
    }
}
