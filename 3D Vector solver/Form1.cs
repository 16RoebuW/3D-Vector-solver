using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3D_Vector_solver
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            switch (cbxOptions.Text)
            {
                case "Intersection between a point and a line":
                    {
                        (bool valid, double pos) = PointIntersectsLine(ParseLine(tbxLn1.Text), ParsePoint(tbxPoint.Text));
                        if (valid)
                        {
                            MessageBox.Show("The point intersects the line when λ = " + pos);
                        }
                        else
                        {
                            MessageBox.Show("The point does not lie on the line");
                        }
                        break;
                    }
                case "Intersection between two lines":
                    {
                        (bool valid, double[] pos) = LineIntersection(ParseLine(tbxLn1.Text), ParseLine(tbxLn2.Text));
                        if (valid)
                        {
                            MessageBox.Show($"The lines meet at the point {pos[0]}, {pos[1]}, {pos[2]}");
                        }
                        else
                        {
                            MessageBox.Show("The lines do not meet");
                        }

                        break;                       
                    }
                case "Are two lines skew?":
                    {
                        double[] l1 = ParseLine(tbxLn1.Text);
                        double[] l2 = ParseLine(tbxLn2.Text);
                        bool parallel = true;

                        for (int i = 3; i < 6; i++)
                        {
                            if (l1[i] != l2[i])
                            {
                                parallel = false;
                                i = 6; // End the loop
                            }
                        }

                        if (parallel)
                        {
                            MessageBox.Show("The lines are not skew, they are parallel");
                            break;
                        }

                        (bool valid, double[] pos) = LineIntersection(l1, l2);

                        if (valid)
                        {
                            MessageBox.Show($"The lines are not skew, they meet at the point {pos[0]}, {pos[1]}, {pos[2]}");
                            break;
                        }

                        MessageBox.Show("The lines are skew");
                        break;
                    }
            }
        }

        double FindUnknown(double coefficient, double value)
        {
            return value / coefficient;
        }

        double[] ParseLine(string line)
        {
            string[] splitLine = line.Split(',');
            double[] output = new double[6];

            for (int i = 0; i < 6; i++)
            {
                output[i] = Double.Parse(splitLine[i]);
            }

            return output;
        }

        double[] ParsePoint(string point)
        {
            string[] splitPoint = point.Split(',');
            double[] output = new double[3];

            for (int i = 0; i < 3; i++)
            {
                output[i] = Double.Parse(splitPoint[i]);
            }

            return output;
        }

        double DotProduct(double[] vctA, double[] vctB)
        {
            return (vctA[0] * vctB[0]) + (vctA[1] * vctB[1]) + (vctA[2] * vctB[2]);
        }

        double Magnitude(double[] vector)
        {
            return Math.Sqrt((vector[0] * vector[0]) + (vector[1] * vector[1]) + (vector[2] * vector[2]));
        }

        (bool valid, double pos) PointIntersectsLine(double[] line, double[] point)
        {
            double coefficient1 = line[3];
            double value1 = point[0] - line[0];
            // Find λ for x

            double coefficient2 = line[4];
            double value2 = point[1] - line[1];
            // Check λ for y
            
            double coefficient3 = line[5];
            double value3 = point[2] - line[2];
            // Check λ for z

            double lambda1 = FindUnknown(coefficient1, value1);
            double lambda2 = FindUnknown(coefficient2, value2);
            double lambda3 = FindUnknown(coefficient3, value3);

            if (lambda1 == lambda2 && lambda2 == lambda3)
            {
                return (true, FindUnknown(coefficient1, value1));
            }
            else
            {
                return (false, 0);
            }
        }

        (bool valid, double[] pos) LineIntersection(double[] l1, double[] l2)
        {
            double[] eqn1 = new double[3];
            eqn1[0] = l1[3];
            eqn1[1] = -l2[3];
            eqn1[2] = -l1[0] + l2[0];

            double[] eqn2 = new double[3];
            eqn2[0] = l1[4];
            eqn2[1] = -l2[4];
            eqn2[2] = -l1[1] + l2[1];

            double[] eqn3 = new double[3];
            eqn3[0] = l1[5];
            eqn3[1] = -l2[5];
            eqn3[2] = -l1[2] + l2[2];

            var (valid, x, y) = Find2Unknowns(eqn1, eqn2);
            var (valid2, y2, z) = Find2Unknowns(eqn2, eqn3);
            var (valid3, x2, z2) = Find2Unknowns(eqn1, eqn3);

            if (valid && valid2 && valid3)
            {
                double[] point = new double[3];
                point[0] = l1[0] + (x * l1[3]);
                point[1] = l1[1] + (x * l1[4]);
                point[2] = l1[2] + (x * l1[5]);
                return (true, point);
            }
            else
            {
                return (false, new double[3] { 0, 0, 0 });
            }


        }

        (bool valid, double x, double y) Find2Unknowns(double[] eqn1, double[] eqn2)
        {
            // Construct a 2x2 matrix and invert it
            double[] inverse = new double[4];
            inverse[0] = eqn1[0];
            inverse[1] = eqn1[1];
            inverse[2] = eqn2[0];
            inverse[3] = eqn2[1];

            // Determinant
            double det = inverse[0] * inverse[3] - inverse[1] * inverse[2];

            if (det == 0)
            {
                return (false, 0, 0);
            }

            // Transpose
            double temp = inverse[0];
            inverse[0] = inverse[3];
            inverse[3] = temp;

            // Cofactors
            inverse[1] *= -1;
            inverse[2] *= -1;

            for (int i = 0; i < 4; i++)
            {
                inverse[i] /= det;
            }

            double x = inverse[0] * eqn1[2] + inverse[1] * eqn2[2];
            double y = inverse[2] * eqn1[2] + inverse[3] * eqn2[2];

            return (true, x, y);
        }

        double ShortestDistance(double[] l1, double[] l2)
        {
            // 0 col is λ, 1 col is µ, 2 col is constant
            // Rows are xyz
            double[,] perpDirection = new double[3,3];

            perpDirection[0, 0] = l1[3];
            perpDirection[1, 0] = l1[4];
            perpDirection[2, 0] = l1[5];

            perpDirection[0, 1] = l2[3];
            perpDirection[1, 1] = l2[4];
            perpDirection[2, 1] = l2[5];

            perpDirection[0, 2] = l2[0] - l1[0];
            perpDirection[1, 2] = l2[1] - l1[1];
            perpDirection[2, 2] = l2[2] - l1[2];

            // Dot product of this = 0 with both lines
        }
    }

}
