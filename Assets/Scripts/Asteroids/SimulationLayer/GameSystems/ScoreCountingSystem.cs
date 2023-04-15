using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class ScoreCountingSystem : SimpleEntitySystem<IScoreProducer>, IUpdateSystem
    {
        private readonly IEntityStrategy<IScoreProducer>_strategy;

        public ScoreCountingSystem(IEntityStrategy<IScoreProducer> strategy)
        {
            _strategy = strategy;
        }
        
        public void Update(float deltaTime)
        {
            Entities.Update(e => _strategy.Execute(e, deltaTime));
        }
    }
}