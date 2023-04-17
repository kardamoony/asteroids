using Asteroids.UILayer.Initialization;
using Asteroids.UILayer.MVVM;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization
{
    public class UIInitializer : IInitializer<UIView, IUIContext>
    {
        private readonly IInitializationHandler<UIView, IUIContext> _handler;

        public UIInitializer(IInitializationHandler<UIView, IUIContext>[] handlers)
        {
            _handler = handlers[0];
            
            for (var i = 0; i < handlers.Length - 1; i++)
            {
                handlers[i].Next = handlers[i + 1];
            }
        }
        
        public void InitializeObject(UIView @object, IUIContext context)
        {
            _handler.HandleInitialization(@object, context);
        }

        public void DeinitializeObject(UIView @object, bool dispose)
        {
            _handler.HandleDeinitialization(@object);
        }
    }
}