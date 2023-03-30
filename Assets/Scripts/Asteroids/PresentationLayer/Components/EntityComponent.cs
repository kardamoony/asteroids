using UnityEngine;

namespace Asteroids.PresentationLayer.Components
{
    [RequireComponent(typeof(EntityView))]
    public abstract class EntityComponent<T> : EntityComponentBase
    {
        protected T Context { get; private set; }
        protected bool Initialized { get; private set; }

        public void SetContext(T context)
        {
            Context = context;
            Initialized = context != null;
            OnContextSet();
        }

        public override void ClearContext()
        {
            if (!Initialized)
            {
                return;
            }
            
            Context = default;
            Initialized = false;
            OnContextCleared();
        }

        protected virtual void OnContextSet(){}
        protected virtual void OnContextCleared(){}
    }
}
