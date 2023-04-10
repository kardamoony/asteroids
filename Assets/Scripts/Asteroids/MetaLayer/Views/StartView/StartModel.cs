using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.MetaLayer.Views.StartView
{
    public class StartModel
    {
        public readonly IInitializationStrategy Strategy;
        
        public StartModel(IInitializationStrategy initializationStrategy)
        {
            Strategy = initializationStrategy;
        }
    }
}