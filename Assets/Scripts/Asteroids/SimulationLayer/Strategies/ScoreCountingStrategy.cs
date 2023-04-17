using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Providers;

namespace Asteroids.SimulationLayer.Strategies
{
    public class ScoreCountingStrategy : IEntityStrategy<IScoreProducer>, IPlayerScoreProvider
    {
        private readonly uint _playerId;
        public uint Score { get; private set; }

        public ScoreCountingStrategy(uint playerId)
        {
            _playerId = playerId;
        }

        public void Execute(IScoreProducer entity, float deltaTime)
        {
            if (entity.ScoreReceiver is IPlayer player && _playerId.Equals(player.Id) && player.Destructable.Health > 0)
            {
                var score = Score + entity.Score;

                if (score >= Score)
                {
                    Score = score;
                }
                
                entity.Score = 0;
            }
        }
    }
}