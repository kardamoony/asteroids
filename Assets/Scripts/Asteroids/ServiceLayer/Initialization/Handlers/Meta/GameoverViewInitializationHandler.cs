using Asteroids.IoC;
using Asteroids.UILayer.Initialization;
using Asteroids.UILayer.MVVM;
using Asteroids.UILayer.Views.GameoverView;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Meta
{
    public class GameoverViewInitializationHandler : IInitializationHandler<UIView, IUIContext>
    {
        public IInitializationHandler<UIView, IUIContext> Next { get; set; }
        
        public void HandleInitialization(UIView @object, IUIContext context)
        {
            if (@object is GameoverView gameoverView)
            {
                var model = Locator.Instance.Resolver.Resolve<GameoverModel>();
                gameoverView.Initialize(model);
                gameoverView.SetParent(context.Parent);
                return;
            }
            
            Next?.HandleInitialization(@object, context);
        }

        public void HandleDeinitialization(UIView @object)
        {
            if (@object is GameoverView gameoverView)
            {
                gameoverView.Deinitialize();
                return;
            }
            
            Next.HandleDeinitialization(@object);
        }
    }
}