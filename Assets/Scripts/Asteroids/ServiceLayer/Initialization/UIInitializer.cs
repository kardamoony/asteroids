using System;
using Asteroids.MetaLayer.MVVM;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization
{
    public class UIInitializer : IInitializer<UIView, UIModel>
    {
        public event Action<UIView> OnObjectInitialized;
        public event Action<UIView> OnObjectDenitialized;
        
        private readonly IInitializationHandler<UIView, UIModel> _handler;

        public UIInitializer(IInitializationHandler<UIView, UIModel>[] handlers)
        {
            _handler = handlers[0];
            
            for (var i = 0; i < handlers.Length - 1; i++)
            {
                handlers[i].Next = handlers[i + 1];
            }
        }
        
        public void InitializeObject(UIView @object, UIModel context)
        {
            _handler.HandleInitialization(@object, context);
            OnObjectInitialized?.Invoke(@object);
        }

        public void DeinitializeObject(UIView @object)
        {
            _handler.HandleDeinitialization(@object);
            OnObjectDenitialized?.Invoke(@object);
        }
    }
}