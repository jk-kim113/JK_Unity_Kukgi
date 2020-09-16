using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Test
{
    class PMatrix
    {
        float[,] _matrix = new float[4, 4];
        public float this[int column, int row]
        {
            get
            {
                return _matrix[column, row];
            }
        }

        public PMatrix()
        {
            for(int n = 0; n < _matrix.GetLength(0); n++)
                for (int m = 0; m < _matrix.GetLength(1); m++)
                    if (n == m)
                        _matrix[n, m] = 1;
                    else
                        _matrix[n, m] = 0;
        }

        public static PVector operator *(PVector v1, PMatrix m1)
        {
            float[] value = new float[3];
            
            for (int n = 0; n < m1._matrix.GetLength(1); n++)
            {
                value[n] = v1.X * m1[0, n] + v1.Y * m1[1, n] + v1.Z * m1[2, n];
            }

            return new PVector(value[0], value[1], value[2]);
        }
    }
}
