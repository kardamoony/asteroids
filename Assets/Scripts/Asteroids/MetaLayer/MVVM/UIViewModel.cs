namespace Asteroids.MetaLayer.MVVM
{
    public abstract class UIViewModel{}
    
    public abstract class UIViewModel<TModel> : UIViewModel
    {
        protected TModel Model { get; private set; }

        public void Activate(TModel model)
        {
            Model = model;
            OnActivated();
        }

        protected virtual void OnActivated(){}
    }
}