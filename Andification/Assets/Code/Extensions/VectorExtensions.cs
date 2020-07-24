using UnityEngine;

namespace Andification.Runtime.Extensions
{
    public static class VectorExtensions
    {
        public static void Clamp(ref this Vector3 v, Vector3 min, Vector3 max)
        {
            v.x = Mathf.Clamp(v.x, min.x, max.x);
            v.y = Mathf.Clamp(v.y, min.y, max.y);
            v.z = Mathf.Clamp(v.z, min.z, max.z);
        }

        public static void Clamp(ref this Vector2 v, Vector3 min, Vector3 max)
        {
            v.x = Mathf.Clamp(v.x, min.x, max.x);
            v.y = Mathf.Clamp(v.y, min.y, max.y);
        }
        
        public static Vector3 XZtoXYZ(this Vector2 self)
        {
            return new Vector3(self.x, 0, self.y);
        }

        public static Vector2 XYZtoXZ(this Vector3 self)
        {
            return new Vector2(self.x, self.z);
        }

        public static Vector3 ZeroY(this Vector3 self)
        {
            return new Vector3(self.x, 0, self.z);
        }

        public static float Sum(this Vector2 self)
        {
            return self.x + self.y;
        }

        public static float Sum(this Vector3 self)
        {
            return self.x + self.y + self.z;
        }

        public static float Average(this Vector2 self)
        {
            return (self.x + self.y) / 2;
        }

        public static float Average(this Vector3 self)
        {
            return (self.x + self.y + self.z) / 3;
        }

        public static bool ShorterThan(this Vector2 self, float distance)
        {
            return self.sqrMagnitude < (distance * distance);
        }

        public static bool ShorterThan(this Vector3 self, float distance)
        {
            return self.sqrMagnitude < (distance * distance);
        }

        public static string ToLongString(this Vector2 self)
        {
            return $"({self.x}, {self.y})";
        }

        public static string ToLongString(this Vector3 self)
        {
            return $"({self.x}, {self.y}, {self.z})";
        }
    }
}