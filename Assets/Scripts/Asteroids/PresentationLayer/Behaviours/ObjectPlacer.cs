using UnityEngine;

namespace Asteroids.PresentationLayer.Behaviours
{
    public class ObjectPlacer : MonoBehaviour
    {
        private Transform _cachedTransform;
        protected Transform CachedTransform => _cachedTransform ? _cachedTransform : _cachedTransform = CacheTransform();
        
        public virtual void Place(Transform t)
        {
            t.position = CachedTransform.position;
            t.rotation = CachedTransform.rotation;
        }

        private Transform CacheTransform()
        {
            return transform;
        }
    }
}