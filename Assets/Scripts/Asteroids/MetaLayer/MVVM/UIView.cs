using UnityEngine;

namespace Asteroids.MetaLayer.MVVM
{
    public abstract class UIView : MonoBehaviour
    {
        public void SetParent(Transform parent)
        {
            gameObject.transform.SetParent(parent);
        }
    }
    
    public abstract class UIView<TViewModel, TModel> : UIView where TViewModel : UIViewModel<TModel>, new()
    {
        protected TViewModel ViewModel;
        protected bool Initialized { get; private set; }

        public void Initialize(TModel model)
        {
            ViewModel = new TViewModel();
            ViewModel.Activate(model);
            Initialized = true;
        }

        public void Deinitialize()
        {
            ViewModel = null;
            Initialized = false;
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            OnShown();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnHidden();
        }

        protected virtual void OnShown(){}
        protected virtual void OnHidden(){}
    }
}