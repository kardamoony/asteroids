using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class RotationSystem : InputReadingSystem<IRotatable>, IUpdateSystem
    {
        private readonly IEntityStrategy<IRotatable> _strategy;

        public RotationSystem(IEntityStrategy<IRotatable> strategy)
        {
            _strategy = strategy;
            EntitiesInputMap = new EntitiesInputMap<IRotatable>();
        }

        public void Update(float deltaTime)
        {
            EntitiesInputMap.AddPending();
            EntitiesInputMap.RemovePending();

            EntitiesInputMap.Foreach((rotatable, input) => _strategy.Execute(rotatable, input, deltaTime));
        }
    }
}
