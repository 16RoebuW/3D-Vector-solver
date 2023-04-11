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
    // To do: line of intersection between two planes
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
                        (bool valid, double lambda) = PointIntersectsLine(ParseLine(tbxLn1.Text), ParsePoint(tbxPoint.Text));
                        if (valid)
                        {
                            MessageBox.Show("The point intersects the line when λ = " + lambda);
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
                case "Shortest distance between two lines":
                    {
                        MessageBox.Show(ShortestDistanceLines(ParseLine(tbxLn1.Text), ParseLine(tbxLn2.Text)).ToString());
                        break;
                    }
                case "Shortest distance between a point and a line":
                    {
                        MessageBox.Show(ShortestDistancePointLine(ParseLine(tbxLn1.Text), ParsePoint(tbxPoint.Text)).ToString());
                        break;
                    }
                case "Shortest distance between a point and a plane":
                    {
                        MessageBox.Show(ShortestDistancePointPlane(ParsePlane(tbxPlane.Text), ParsePoint(tbxPoint.Text)).ToString());
                        break;
                    }
                case "Reflect a point in a plane":
                    {
                        double[] point = ReflectionPointPlane(ParsePlane(tbxPlane.Text), ParsePoint(tbxPoint.Text));
                        MessageBox.Show($"Reflected point is at ({point[0]}, {point[1]}, {point[2]}");
                        break;
                    }
                case "Reflect a line in a plane":
                    {
                        double[] line = ReflectionLinePlane(ParsePlane(tbxPlane.Text), ParseLine(tbxLn1.Text));
                        MessageBox.Show($"The reflected line has equation ({line[0]}, {line[1]}, {line[2]}) + λ({line[3]}, {line[4]}, {line[5]})");
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

        double[] ParsePlane(string plane)
        {
            string[] splitPlane = plane.Split(',');
            double[] output = new double[4];

            for (int i = 0; i < 4; i++)
            {
                output[i] = Double.Parse(splitPlane[i]);
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

        (bool valid, double lambda) PointIntersectsLine(double[] line, double[] point)
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
            // Using the x co-ords to form an equation in lambda and mu (eqn[0] represents lambda, [1] is mu and [2] is the constant)
            double[] eqn1 = new double[3];
            eqn1[0] = l1[3];
            eqn1[1] = -l2[3];
            eqn1[2] = -l1[0] + l2[0];

            // Do the same with y
            double[] eqn2 = new double[3];
            eqn2[0] = l1[4];
            eqn2[1] = -l2[4];
            eqn2[2] = -l1[1] + l2[1];

            // And finally with z
            double[] eqn3 = new double[3];
            eqn3[0] = l1[5];
            eqn3[1] = -l2[5];
            eqn3[2] = -l1[2] + l2[2];

            // Solve simultaneously and check
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

        (bool valid, double lambda) LineIntersectsPlane(double[] plane, double[] line)
        {
            double value = plane[3];
            double coefficient = 0;
            // Dot product but with the unknown, lambda
            for (int i = 0; i < 3; i++)
            {
                value -= line[i] * plane[i];
                coefficient += line[i + 3] * plane[i];
            }

            if (coefficient != 0)
            {
                return (true, FindUnknown(coefficient, value));
            }
            else
            {
                return (false, 0);
            }
        }     

        double ShortestDistanceLines(double[] l1, double[] l2)
        {
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
                // Suprisingly, this is not an optimisation step. The problem cannot be solved when dealing with parallel lines unless this method is adopted

                // We make a new line, combining the offsets and making the direction in terms of t (mu - lambda)
                double[] perpOffset = new double[3];
                for (int i = 0; i < 3; i++)
                    perpOffset[i] = l2[i] - l1[i];

                // Dot product with t as this line is perpendicular to both lines so = 0
                double coefficient = 0;
                for (int i = 3; i < 6; i++)
                    coefficient += l1[i] * l1[i];

                double value = 0;
                for (int i = 0; i < 3; i++)
                    value -= perpOffset[i] * l1[i + 3];

                // Find t
                double t = FindUnknown(coefficient, value);

                // Use the value of t for the distance between the lines
                double[] distanceVect = new double[3];
                for (int i = 0; i < 3; i++)
                    distanceVect[i] = perpOffset[i] + (t * l1[i + 3]);


                return Magnitude(distanceVect);
            }
            else
            {
                // 0 col is λ, 1 col is µ, 2 col is constant
                // Rows are xyz
                double[,] perpDirection = new double[3, 3];

                perpDirection[0, 0] = -l1[3];
                perpDirection[1, 0] = -l1[4];
                perpDirection[2, 0] = -l1[5];

                perpDirection[0, 1] = l2[3];
                perpDirection[1, 1] = l2[4];
                perpDirection[2, 1] = l2[5];

                perpDirection[0, 2] = l2[0] - l1[0];
                perpDirection[1, 2] = l2[1] - l1[1];
                perpDirection[2, 2] = l2[2] - l1[2];
                // So now we have the general vector (in terms of lambda and mu) between the lines

                // Dot product of this = 0 with both lines

                double[] eqn1 = new double[3];
                double[] eqn2 = new double[3];
                double[] eqn3 = new double[3];

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        eqn1[j] += perpDirection[i, j] * l1[i + 3];
                eqn1[2] *= -1;

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        eqn2[j] += perpDirection[i, j] * l2[i + 3];
                eqn2[2] *= -1;

                var (valid, x, y) = Find2Unknowns(eqn1, eqn2);

                double[] distanceVect = new double[3];

                // Sub in lambda and mu (x and y here)
                for (int i = 0; i < 3; i++)
                    distanceVect[i] = (x * perpDirection[i, 0]) + (y * perpDirection[i, 1]) + perpDirection[i, 2];

                return Magnitude(distanceVect);
            }
        }

        double ShortestDistancePointLine(double[] line, double[] point)
        {
            // Get the vector of a general point on the line to the point in question and then dot product this with the line's direction as it is perpendicular
            double[] offset = new double[3];
            double value = 0;
            double coefficient = 0;
            for (int i = 0; i < 3; i++)
            {
                offset[i] = line[i] - point[i];
                value -= offset[i] * line[i + 3];
                coefficient = line[i + 3] * line[i + 3];
            }
            
            // Find lambda and substitute
            double lambda = FindUnknown(coefficient, value);

            double[] distanceVector = new double[3];

            for (int i = 0; i < 3; i++)
                distanceVector[i] = offset[i] + (line[i + 3] * lambda);

            return Magnitude(distanceVector);
        }

        double ShortestDistancePointPlane(double[] plane, double[] point)
        {
            // I'll be honest, I only know the formula for this, not the logic, so code may be hard to read
            double top = 0;
            double[] normal = new double[3];
            for (int i = 0; i < 3; i++)
            {
                top += plane[i] * point[i];
                normal[i] = plane[i];
            }

            top -= plane[3];
            top = Math.Abs(top);
          
            return top / Magnitude(normal);
        }

        double[] ReflectionPointPlane(double[] plane, double[] point)
        {
            double[] normalLine = new double[6];
            for (int i = 0; i < 3; i++)
                normalLine[i] = point[i];
            for (int i = 0; i < 3; i++)
                normalLine[i + 3] = plane[i];

            double lambda = LineIntersectsPlane(plane, normalLine).lambda;

            double[] reflectedPoint = new double[3];
            for (int i = 0; i < 3; i++)
                reflectedPoint[i] = point[i] + (2 * lambda * plane[i]);

            return reflectedPoint;
        }

        // Does not work for parallel line/plane
        double[] ReflectionLinePlane(double[] plane, double[] line)
        {
            double lambdaI = LineIntersectsPlane(plane, line).lambda;
            double[] intersectPoint = new double[3]; 

            // Picking some point on the line, in this case 2+ the value where they intersect
            double anyLambda = lambdaI + 2;
            double[] reflectedPoint = new double[3];

            for (int i = 0; i < 3; i++)
            {
                reflectedPoint[i] = line[i] + (anyLambda * line[i + 3]);
                intersectPoint[i] = line[i] + (lambdaI * line[i + 3]);
            }

            reflectedPoint = ReflectionPointPlane(plane, reflectedPoint);

            // Construct line using the two points
            double[] reflectedLine = new double[6];
            for (int i = 0; i < 3; i++)
            {
                reflectedLine[i] = intersectPoint[i];
                reflectedLine[i + 3] = reflectedPoint[i] - intersectPoint[i];
            }

            return reflectedLine;
        }
    }

}
