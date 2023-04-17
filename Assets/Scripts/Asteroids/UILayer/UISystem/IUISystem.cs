using System;
using Asteroids.UILayer.MVVM;

namespace Asteroids.UILayer.UISystem
{
    public interface IUISystem
    {
        bool TryGetView<TView>(out TView view) where TView : UIView;
        void AddView<TView>(string viewId, bool show, params object[] args) where TView : UIView;
        public void RemoveView(Type viewType);
    }
}