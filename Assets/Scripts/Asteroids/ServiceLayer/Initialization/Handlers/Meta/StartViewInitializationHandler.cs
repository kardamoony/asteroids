using Asteroids.IoC;
using Asteroids.MetaLayer.Initialization;
using Asteroids.MetaLayer.MVVM;
using Asteroids.MetaLayer.Views.StartView;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Meta
{
    public class StartViewInitializationHandler : IInitializationHandler<UIView, UIContext>
    {
        public IInitializationHandler<UIView, UIContext> Next { get; set; }
        
        public void HandleInitialization(UIView @object, UIContext context)
        {
            if (@object is StartView startView && context is StartContext startContext)
            {
                var model = Locator.Instance.Resolver.Resolve<StartModel>();
                startView.Initialize(model);
                startView.SetParent(startContext.Parent);
                return;
            }
            
            Next?.HandleInitialization(@object, context);
        }

        public void HandleDeinitialization(UIView @object)
        {
            if (@object is StartView startView)
            {
                startView.Deinitialize();
                return;
            }
            
            Next?.HandleDeinitialization(@object);
        }
    }
}