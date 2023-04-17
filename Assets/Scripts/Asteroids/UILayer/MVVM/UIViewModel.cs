namespace Asteroids.UILayer.MVVM
{
    public abstract class UIViewModel{}
    
    public abstract class UIViewModel<TModel> : UIViewModel
    {
        protected TModel Model { get; private set; }
        public bool Initialized { get; private set; }

        public void Initialize(TModel model)
        {
            Model = model;
            OnInitialized();
            Initialized = true;
        }

        public void Deinitialize()
        {
            Initialized = false;
            OnDeinitialized();
        }

        protected virtual void OnInitialized(){}
        protected virtual void OnDeinitialized(){}
    }
}