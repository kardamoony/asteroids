using Asteroids.MetaLayer.MVVM;
using Asteroids.MetaLayer.Views.StartView;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Meta
{
    public class StartViewInitializationHandler : IInitializationHandler<UIView, UIModel>
    {
        public IInitializationHandler<UIView, UIModel> Next { get; set; }
        
        public void HandleInitialization(UIView @object, UIModel context)
        {
            if (@object is StartView startView && context is StartModel model)
            {
                startView.Initialize(model);
                return;
            }
            
            Next?.HandleInitialization(@object, context);
        }

        public void HandleDeinitialization(UIView @object)
        {
            //TODO: deinitialization
        }
    }
}