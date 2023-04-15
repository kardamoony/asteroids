using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class RotationSystem : EntityContextSystem<IRotatable, IInputProvider>, IUpdateSystem
    {
        private readonly IContextEntityStrategy<IRotatable, IInputProvider> _strategy;

        public RotationSystem(IContextEntityStrategy<IRotatable, IInputProvider> strategy)
        {
            _strategy = strategy;
        }

        public void Update(float deltaTime)
        {
            EntitiesContextMap.Update((rotatable, input) => _strategy.Execute(rotatable, input, deltaTime));
        }
    }
}
