using Asteroids.SimulationLayer.Providers;

namespace Asteroids.MetaLayer.Views.ScoreView
{
    public class ScoreModel
    {
        public IPlayerScoreProvider ScoreProvider { get; private set; }
        
        public ScoreModel(IPlayerScoreProvider scoreProvider)
        {
            ScoreProvider = scoreProvider;
        }
    }
}
