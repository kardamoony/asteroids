using UnityEngine;

namespace Asteroids.PresentationLayer.Components
{
    public abstract class EntityComponent<T> : MonoBehaviour
    {
        protected T Context { get; private set; }
        protected bool Initialized { get; private set; }

        public void SetContext(T context)
        {
            Context = context;
            Initialized = context != null;
        }

        public void ClearContext()
        {
            Context = default;
            Initialized = false;
        }
    }
}
