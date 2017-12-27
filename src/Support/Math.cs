using System;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

    public static partial class Math
    {
        public const float RadianPI = 57.29578f; // 180.0 / Math.PI
        public const float DegreePI = 0.01745329f; // Math.PI / 180.0
        public const float TwoPI = 6.28319f; // Math.PI * 2

        public static float RadianToDegree(float radian)
        {
            return radian * RadianPI;
        }

        public static float DegreeToRadian(float degree)
        {
            return degree * DegreePI;
        }

        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2((float)System.Math.Cos(radian), (float)System.Math.Sin(radian));
        }

        public static Vector2 RadianToVector2(float radian, float length)
        {
            return RadianToVector2(radian) * length;
        }

        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(DegreeToRadian(degree));
        }

        public static Vector2 DegreeToVector2(float degree, float length)
        {
            return RadianToVector2(DegreeToRadian(degree), length);
        }

        public static float Vector2ToRadian(Vector2 direction)
        {
            return (float)System.Math.Atan2(direction.Y, direction.X);
        }

        public static float Vector2ToDegree(Vector2 direction)
        {
            return RadianToDegree(Vector2ToRadian(direction));
        }

        public static float LookAtRadian(Vector2 pos1, Vector2 pos2)
        {
            return (float)System.Math.Atan2(pos2.Y - pos1.Y, pos2.X - pos1.X);
        }

        public static Vector2 LookAtVector2(Vector2 pos1, Vector2 pos2)
        {
            return RadianToVector2(LookAtRadian(pos1, pos2));
        }

        public static float LookAtDegree(Vector2 pos1, Vector2 pos2)
        {
            return RadianToDegree(LookAtRadian(pos1, pos2));
        }

        public static float Distance(Vector2 pos1, Vector2 pos2)
        {
            return (float)System.Math.Sqrt(System.Math.Pow(pos2.X - pos1.X, 2) + System.Math.Pow(pos2.Y - pos1.Y, 2));
        }

        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        public struct Vector2
        {
            private float x, y;

            public float X
            {
                get
                {
                    return x;
                }
                set
                {
                    x = value;
                }
            }

            public float Y
            {
                get
                {
                    return y;
                }
                set
                {
                    y = value;
                }
            }

            public static readonly Vector2 Empty = new Vector2();

            public Vector2(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public override bool Equals(object obj)
            {
                if (obj is Vector2 v)
                    return v.x == x && v.y == y;

                return false;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override string ToString()
            {
                return String.Format("X={0}, Y={1}", x, y);
            }

            public static bool operator ==(Vector2 u, Vector2 v)
            {
                return u.x == v.x && u.y == v.y;
            }

            public static bool operator !=(Vector2 u, Vector2 v)
            {
                return !(u == v);
            }

            public static Vector2 operator +(Vector2 u, Vector2 v)
            {
                return new Vector2(u.x + v.x, u.y + v.y);
            }

            public static Vector2 operator -(Vector2 u, Vector2 v)
            {
                return new Vector2(u.x - v.x, u.y - v.y);
            }

            public static Vector2 operator *(Vector2 u, float a)
            {
                return new Vector2(a * u.x, a * u.y);
            }

            public static Vector2 operator /(Vector2 u, float a)
            {
                return new Vector2(u.x / a, u.y / a);
            }

            public static Vector2 operator -(Vector2 u)
            {
                return new Vector2(-u.x, -u.y);
            }

#if (!PORTABLE)

            public static explicit operator System.Drawing.Point(Vector2 u)
            {
                return new System.Drawing.Point((int)System.Math.Round(u.x), (int)System.Math.Round(u.y));
            }

            public static implicit operator Vector2(System.Drawing.Point p)
            {
                return new Vector2(p.X, p.Y);
            }

#endif
        }
    }

#if PORTABLE
    }

#endif
}