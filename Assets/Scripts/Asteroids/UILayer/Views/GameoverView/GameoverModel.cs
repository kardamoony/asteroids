using Asteroids.SimulationLayer.Initialization;
using Asteroids.SimulationLayer.Providers;

namespace Asteroids.UILayer.Views.GameoverView
{
    public class GameoverModel
    {
        public IPlayerAttemptsProvider AttemptsProvider { get; }
        public IPlayerScoreProvider ScoreProvider { get; }
        
        public IInitializationStrategy GameplayInitStrategy { get; }
        
        public GameoverModel(IPlayerAttemptsProvider attemptsProvider, 
            IPlayerScoreProvider scoreProvider, 
            IInitializationStrategy gameplayInitStrategy)
        {
            AttemptsProvider = attemptsProvider;
            ScoreProvider = scoreProvider;
            GameplayInitStrategy = gameplayInitStrategy;
        }
    }
}