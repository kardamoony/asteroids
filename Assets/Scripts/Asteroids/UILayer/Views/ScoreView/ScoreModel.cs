using Asteroids.SimulationLayer.Providers;

namespace Asteroids.UILayer.Views.ScoreView
{
    public class ScoreModel
    {
        public IPlayerScoreProvider ScoreProvider { get; }
        public IPlayerAttemptsProvider AttemptsProvider { get; }

        public ScoreModel(IPlayerScoreProvider scoreProvider, IPlayerAttemptsProvider attemptsProvider)
        {
            ScoreProvider = scoreProvider;
            AttemptsProvider = attemptsProvider;
        }
    }
}
