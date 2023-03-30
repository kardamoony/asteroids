using UnityEngine;

namespace Asteroids.PresentationLayer.Behaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class ScreenBoundariesEnforcer : MonoBehaviour
    {
        private Rigidbody _rb;

        private float _minX;
        private float _maxX;
        
        private float _minZ;
        private float _maxZ;

        private void FixedUpdate()
        {
            var pos = _rb.position;

            pos.x = GetCoord(pos.x, _minX, _maxX);
            pos.z = GetCoord(pos.z, _minZ, _maxZ);

            _rb.position = pos;
        }

        private float GetCoord(float current, float min, float max)
        {
            if (current < max && current > min)
            {
                return current;
            }

            return current > max ? min : max;
        }

        private void CacheValues()
        {
            _rb = GetComponent<Rigidbody>();
            
            var mainCamera = Camera.main;

            var topRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.farClipPlane));
            var bottomLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.farClipPlane));
            
            _minX = bottomLeftCorner.x;
            _maxX = topRightCorner.x;

            _minZ = bottomLeftCorner.z;
            _maxZ = topRightCorner.z;
        }

        private void Awake()
        {
            CacheValues();
        }
    }
}
