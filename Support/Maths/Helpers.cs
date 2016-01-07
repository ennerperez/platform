using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support.Maths
{
    public static class Helpers
    {
        public const float RadianPI = 57.29578f; // 180.0 / Math.PI
        public const float DegreePI = 0.01745329f; // Math.PI / 180.0
        public const float TwoPI = 6.28319f; // Math.PI * 2

        private static readonly object randomLock = new object();
        private static readonly Random random = new Random();

        public static int Random(int max)
        {
            lock (randomLock)
            {
                return random.Next(max + 1);
            }
        }

        public static int Random(int min, int max)
        {
            lock (randomLock)
            {
                return random.Next(min, max + 1);
            }
        }

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
            return new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian));
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
            return (float)Math.Atan2(direction.Y, direction.X);
        }

        public static float Vector2ToDegree(Vector2 direction)
        {
            return RadianToDegree(Vector2ToRadian(direction));
        }

        public static float LookAtRadian(Vector2 pos1, Vector2 pos2)
        {
            return (float)Math.Atan2(pos2.Y - pos1.Y, pos2.X - pos1.X);
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
            return (float)Math.Sqrt(Math.Pow(pos2.X - pos1.X, 2) + Math.Pow(pos2.Y - pos1.Y, 2));
        }

        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }
    }
}
