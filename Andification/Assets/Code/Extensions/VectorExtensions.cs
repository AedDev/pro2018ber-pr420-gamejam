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
    }
}