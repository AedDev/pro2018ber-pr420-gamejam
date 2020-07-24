using UnityEngine;

namespace Andification.Runtime.Extensions
{
    public static class CameraExtensions
    {
        public static Vector3 WorldToScreenPointInvertedY(this Camera cam, Vector3 position)
        {
            var v = cam.WorldToScreenPoint(position);
            v.y = Screen.height - v.y;

            return v;
        }
    }
}