using UnityEngine;

namespace Asteroids.PresentationLayer.Extensions
{
    public static class CameraExtensions
    {
        public static Vector3 GetTopDownWorldViewCenter(this Camera cam, float yCoord)
        {
            var center = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cam.farClipPlane));
            return new Vector3(center.x, yCoord, center.z);
        }

        public static (float xMin, float xMax, float zMin, float zMax) GetTopDownViewLimits(this Camera cam)
        {
            var topRightCorner = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.farClipPlane));
            var bottomLeftCorner = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.farClipPlane));
            
            var xMin = bottomLeftCorner.x;
            var xMax = topRightCorner.x;

            var zMin = bottomLeftCorner.z;
            var zMax = topRightCorner.z;

            return (xMin, xMax, zMin, zMax);
        }
    }
}