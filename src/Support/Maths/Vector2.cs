using System;

#if (!PORTABLE)

using System.Drawing;

#endif

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

        namespace Maths
        {
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
                    if (obj is Vector2)
                    {
                        Vector2 v = (Vector2)obj;
                        return v.x == x && v.y == y;
                    }

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

            public static explicit operator Point(Vector2 u)
            {
                return new Point((int)Math.Round(u.x), (int)Math.Round(u.y));
            }

            public static implicit operator Vector2(Point p)
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