namespace Asteroids.PresentationLayer.Components
{
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
            
            OnContextWillBeCleared();
            
            Context = default;
            Initialized = false;
        }

        protected virtual void OnContextSet(){}
        protected virtual void OnContextWillBeCleared(){}
    }
}
