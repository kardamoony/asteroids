using Asteroids.IoC;
using Asteroids.UILayer.Initialization;
using Asteroids.UILayer.MVVM;
using Asteroids.UILayer.Views.AttemptsView;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Meta
{
    public class AttemptsViewInitializationHandler : IInitializationHandler<UIView, IUIContext>
    {
        public IInitializationHandler<UIView, IUIContext> Next { get; set; }
        
        public void HandleInitialization(UIView @object, IUIContext context)
        {
            if (@object is AttemptsView attemptsView)
            {
                var model = Locator.Instance.Resolver.Resolve<AttemptsModel>();
                attemptsView.Initialize(model);
                attemptsView.SetParent(context.Parent);
                return;
            }
            
            Next?.HandleInitialization(@object, context);
        }

        public void HandleDeinitialization(UIView @object)
        {
            if (@object is AttemptsView attemptsView)
            {
                attemptsView.Deinitialize();
                return;
            }
            
            Next.HandleDeinitialization(@object);
        }
    }
}