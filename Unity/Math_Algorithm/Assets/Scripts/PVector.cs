using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PVector
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public static PVector zero { get { return new PVector(0, 0, 0); } }
    public static PVector one { get { return new PVector(1, 1, 1); } }
    public static PVector forward { get { return new PVector(0, 0, 1); } }
    public static PVector back { get { return new PVector(0, 0, -1); } }
    public static PVector right { get { return new PVector(1, 0, 0); } }
    public static PVector left { get { return new PVector(-1, 0, 0); } }
    public static PVector up { get { return new PVector(0, 1, 0); } }
    public static PVector down { get { return new PVector(0, -1, 0); } }
    public float magnitude { get { return Magnitude(this); } } 
    public PVector normalize { get { return Normalize(this); } }

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

    public static PVector operator -(PVector v1)
    {
        return new PVector(-v1.X, -v1.Y, -v1.Z);
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

    public static PVector operator /(PVector v1, float a)
    {
        return new PVector(v1.X / a, v1.Y / a, v1.Z / a);
    }

    public static float Magnitude(PVector v1)
    {
        return Mathf.Sqrt(v1.X * v1.X + v1.Y * v1.Y + v1.Z * v1.Z);
    }

    public static PVector Normalize(PVector v1)
    {
        return new PVector(v1.X / Magnitude(v1), v1.Y / Magnitude(v1), v1.Z / Magnitude(v1));
    }
}
