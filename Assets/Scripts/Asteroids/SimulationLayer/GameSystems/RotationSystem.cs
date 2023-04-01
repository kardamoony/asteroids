using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class RotationSystem : InputReadingSystem<IRotatable>, IUpdateSystem
    {
        private readonly IInputBasedEntityStrategy<IRotatable> _strategy;

        public RotationSystem(IInputBasedEntityStrategy<IRotatable> strategy)
        {
            _strategy = strategy;
        }

        public void Update(float deltaTime)
        {
            EntitiesInputMap.Update();
            EntitiesInputMap.Foreach((rotatable, input) => _strategy.Execute(rotatable, input, deltaTime));
        }
    }
}
