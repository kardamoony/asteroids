using Asteroids.IoC;
using Asteroids.MetaLayer.Initialization;
using Asteroids.MetaLayer.MVVM;
using Asteroids.MetaLayer.Views.ScoreView;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Meta
{
    public class ScoreViewInitializationHandler : IInitializationHandler<UIView, IUIContext>
    {
        public IInitializationHandler<UIView, IUIContext> Next { get; set; }
        
        public void HandleInitialization(UIView @object, IUIContext context)
        {
            if (@object is ScoreView scoreView)
            {
                var model = Locator.Instance.Resolver.Resolve<ScoreModel>();
                scoreView.Initialize(model);
                scoreView.SetParent(context.Parent);
                return;
            }
            
            Next?.HandleInitialization(@object, context);
        }

        public void HandleDeinitialization(UIView @object)
        {
            if (@object is ScoreView scoreView)
            {
                scoreView.Deinitialize();
                return;
            }
            
            Next?.HandleDeinitialization(@object);
        }
    }
}