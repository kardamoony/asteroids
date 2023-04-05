using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Strategies
{
    public class ConstantMovement : IContextEntityStrategy<IMovable, IInputProvider>
    {
        public void Execute(IMovable entity, IInputProvider context, float deltaTime)
        {
            entity.Velocity = entity.Speed * context.VerticalAxis;
        }
    }
}