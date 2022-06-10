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
                        (bool valid, double pos) returnVal = PointIntersectsLine(ParseLine(tbxLn1.Text), ParsePoint(tbxPoint.Text));
                        if (returnVal.valid)
                        {
                            MessageBox.Show("The point intersects the line when λ = " + returnVal.pos);
                        }
                        else
                        {
                            MessageBox.Show("The point does not lie on the line");
                        }
                        break;
                    }
                case "Intersection between two lines":
                    {
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

        (bool valid, double pos) PointIntersectsLine(double[] line, double[] point)
        {
            double coefficient1 = line[3];
            double value1 = point[0] - line[0];
            // Find λ for x

            double coefficient2 = line[4];
            double value2 = point[1] - line[1];
            // Check λ for y

            if (FindUnknown(coefficient1, value1) == FindUnknown(coefficient2, value2))
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
            double det = inverse[0] * inverse[3] - inverse[2] * inverse[3];

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
    }

}
