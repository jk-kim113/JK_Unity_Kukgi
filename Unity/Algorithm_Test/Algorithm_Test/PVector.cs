using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Test
{
    class PVector
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public PVector(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static PVector operator +(PVector v1, PVector v2)
        {
            return new PVector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static PVector operator -(PVector v1, PVector v2)
        {
            return new PVector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static PVector operator *(PVector v1, float a)
        {
            return new PVector(v1.X * a, v1.Y * a, v1.Z * a);
        }

        public static PVector operator *(float a, PVector v1)
        {
            return new PVector(v1.X * a, v1.Y * a, v1.Z * a);
        }

    }
}
