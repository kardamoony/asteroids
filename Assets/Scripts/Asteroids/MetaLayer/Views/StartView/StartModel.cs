using Asteroids.MetaLayer.MVVM;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.MetaLayer.Views.StartView
{
    public class StartModel : UIModel
    {
        public readonly IInitializationStrategy Strategy;
        
        public StartModel(IInitializationStrategy initializationStrategy)
        {
            Strategy = initializationStrategy;
        }
    }
}