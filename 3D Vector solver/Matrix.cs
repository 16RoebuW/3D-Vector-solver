using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Vector_solver
{
    class Matrix
    {
        public double[,] values = new double[3,3];

        double[,] CreateMinor(int row, int col)
        {
            int minor_row, minor_col;
            double[,] minor = new double[2, 2];

            for (int i = 0; i < 3; i++)
            {
                minor_row = i;
                if (i > row)
                    minor_row--;
                for (int j = 0; j < 3; j++)
                {
                    minor_col = j;
                    if (j > col)
                        minor_col--;
                    if (i != row && j != col)
                        minor[minor_row, minor_col] = values[i, j];
                }
            }
            return minor;
        }


        public Matrix Invert()
        {           
            double[,] detMinors = new double[3, 3];
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    double[,] minor = CreateMinor(i,j);
                    double det = (minor[0, 0] * minor[1, 1]) - (minor[0, 1] * minor[1, 0]);
                    detMinors[i, j] = det;
                }
            }

            double determinant = (values[0, 0] * detMinors[0,0]) - (values[0,1] * detMinors[0,1]) + (values[0,2] * detMinors[0,2]);

            for (int i = 0; i < 9; i++)
            {
                detMinors[i/3, i % 3] *= Math.Pow(-1, i) ;
            }

            Matrix final = new Matrix();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    final.values[i, j] = detMinors[i, j] / determinant;
                }
            }

            return final;
        }
    }
}
