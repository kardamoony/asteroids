using System;
using System.Collections.Generic;
using Asteroids.CoreLayer.Factories;
using Asteroids.UILayer.MVVM;

namespace Asteroids.UILayer.UISystem
{
    public class UISystem : IUISystem
    {
        private readonly IObjectsFactory<UIView> _uiFactory;
        private Dictionary<Type, UIView> _views = new Dictionary<Type, UIView>();

        public UISystem(IObjectsFactory<UIView> uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public bool TryGetView<TView>(out TView view) where TView : UIView
        {
            var key = typeof(TView);
            
            if (_views.ContainsKey(key))
            {
                view = _views[key] as TView;
                return true;
            }

            view = null;
            return false;
        }

        public void AddView<TView>(string viewId, bool show, params object[] args) where TView : UIView
        {
            if (_views.ContainsKey(typeof(TView)))
            {
                throw new ArgumentException($"[{GetType().Name}] view {nameof(TView)} already added");
            }
            
            _uiFactory.Get<TView>(viewId, view =>
            {
                view.UISystem = this;
                
                if (show)
                {
                    view.Show();
                }
                else
                {
                    view.Hide();
                }
                
                _views.Add(typeof(TView), view);
                
            }, args);
        }

        public void RemoveView(Type viewType)
        {
            if (_views.TryGetValue(viewType, out var view))
            {
                view.Hide(() =>
                {
                    view.UISystem = null;
                    _views.Remove(viewType);
                    _uiFactory.Release(view, true);
                });
            }
        }
    }
}