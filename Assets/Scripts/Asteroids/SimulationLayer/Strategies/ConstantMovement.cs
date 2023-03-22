using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Strategies
{
    public class ConstantMovement : IEntityStrategy<IMovable>
    {
        public void Execute(IMovable entity, IInputProvider inputProvider, float deltaTime)
        {
            entity.Velocity = entity.Speed * inputProvider.VerticalAxis;
        }
    }
}