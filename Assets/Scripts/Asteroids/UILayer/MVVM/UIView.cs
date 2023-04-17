using System;
using Asteroids.UILayer.UISystem;
using UnityEngine;

namespace Asteroids.UILayer.MVVM
{
    public abstract class UIView : MonoBehaviour
    {
        public IUISystem UISystem { get; set; }
        
        public void SetParent(Transform parent)
        {
            gameObject.transform.SetParent(parent);
        }

        public abstract void Show(Action callback = null);
        public abstract void Hide(Action callback = null);
    }
    
    public abstract class UIView<TViewModel, TModel> : UIView where TViewModel : UIViewModel<TModel>, new()
    {
        protected TViewModel ViewModel;
        protected bool Initialized { get; private set; }
        
        protected bool IsShown { get; set; }

        public void Initialize(TModel model)
        {
            ViewModel = new TViewModel();
            ViewModel.Initialize(model);
            OnInitialized();
            Initialized = true;
        }

        public void Deinitialize()
        {
            OnDeinitialized();
            ViewModel = null;
            Initialized = false;
        }
        
        public override void Show(Action callback = null)
        {
            gameObject.SetActive(true);
            OnShown();
            IsShown = true;
            callback?.Invoke();
        }

        public override void Hide(Action callback = null)
        {
            IsShown = false;
            gameObject.SetActive(false);
            OnHidden();
            callback?.Invoke();
        }
        
        protected virtual void OnInitialized(){}
        protected virtual void OnDeinitialized(){}

        protected virtual void OnShown(){}
        protected virtual void OnHidden(){}
    }
}