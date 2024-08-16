using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourierTransformOfVectorAutocorrelation
{
    public struct Vector3
    {
        // Properties for X, Y, Z components of the vector.
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double SumComponents()
        {
            return X + Y + Z;
        }

        public double DotProduct(Vector3 other)
        {
            return X * other.X + Y * other.Y + Z * other.Z;
        }

        public double Norm()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public double Cosine(Vector3 other)
        {
            return DotProduct(other) / (Norm() * other.Norm());
        }

        // A static read-only property representing a zero vector.
        public static Vector3 Zero { get; } = new Vector3(0, 0, 0);
        public static Vector3 One { get; } = new Vector3(1, 1, 1);
        public static Vector3 Multiply(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        // Constructor to initialize the vector with X, Y, Z components.
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Add two vectors.
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        // Subtract two vectors.
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        // Negate a vector.
        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.X, -a.Y, -a.Z);
        }

        // Multiply vector by a scalar.
        public static Vector3 operator *(Vector3 a, double scalar)
        {
            return new Vector3(a.X * scalar, a.Y * scalar, a.Z * scalar);
        }

        // Multiply scalar by vector (commutative).
        public static Vector3 operator *(double scalar, Vector3 a)
        {
            return a * scalar;
        }

        // Divide vector by a scalar.
        public static Vector3 operator /(Vector3 a, double scalar)
        {
            if (scalar == 0)
                throw new DivideByZeroException("Cannot divide by zero.");

            return new Vector3(a.X / scalar, a.Y / scalar, a.Z / scalar);
        }

        // Calculate dot product of two vectors.
        public static double operator *(Vector3 a, Vector3 b)
        {
            return Vector3.Dot(a, b);
        }

        // Calculate Dot product of two Vec3 objects
        public double Dot(Vector3 other) => X * other.X + Y * other.Y + Z * other.Z;

        public static double Dot(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        // Calculate cross product of two vectors.
        public static Vector3 operator %(Vector3 a, Vector3 b)
        {
            return new Vector3(
               a.Y * b.Z - a.Z * b.Y,
               a.Z * b.X - a.X * b.Z,
               a.X * b.Y - a.Y * b.X);
        }

        // Check if two vectors are equal.
        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        // Check if two vectors are not equal.
        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !(a == b);
        }

        // Override Equals() method for value comparison.
        public override bool Equals(object obj)
        {
            // Instead of direct type check, use 'as' to allow nulls
            Vector3 vec = (Vector3)obj;
            return vec != null && X == vec.X && Y == vec.Y && Z == vec.Z;
        }

        // Override GetHashCode() method.
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + Z.GetHashCode();
                return hash;
            }
        }

        // Calculate magnitude (length) of the vector.
        public double Magnitude()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public double LengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        // Normalize the vector (make it unit length).
        public Vector3 Normalize()
        {
            double magnitude = Magnitude();
            if (magnitude == 0)
                throw new InvalidOperationException("Cannot normalize a zero vector.");
            return this / magnitude;
        }

        // Calculate distance between two vectors.
        public static double Distance(Vector3 a, Vector3 b)
        {
            return (a - b).Magnitude();
        }

        // Calculate the angle between two vectors in radians.
        public static double Angle(Vector3 a, Vector3 b)
        {
            double dot = a * b;
            double magA = a.Magnitude();
            double magB = b.Magnitude();
            // Ensure no division by zero
            if (magA == 0 || magB == 0)
                throw new InvalidOperationException("Cannot calculate the angle with a zero vector.");
            double cosTheta = dot / (magA * magB);
            // Ensure the value is within -1 and 1 to account for any doubleing point errors
            cosTheta = Math.Max(-1, Math.Min(1, cosTheta));
            return Math.Acos(cosTheta);
        }

        // Squared norm of the vector
        public double NormSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        public static double DistanceSquared(Vector3 v1, Vector3 v2)
        {
            double dx = v1.X - v2.X;
            double dy = v1.Y - v2.Y;
            double dz = v1.Z - v2.Z;
            return dx * dx + dy * dy + dz * dz;
        }

        // Override ToString() for easy debugging and display.
        public override string ToString()
        {
            // Using String.Format instead of string interpolation for compatibility.
            return String.Format("({0}, {1}, {2})", X, Y, Z);
        }
    }
}