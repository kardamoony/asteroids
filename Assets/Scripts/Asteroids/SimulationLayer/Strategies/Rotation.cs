using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.Strategies
{
    public class Rotation : IContextEntityStrategy<IRotatable, IInputProvider>
    {
        public void Execute(IRotatable entity, IInputProvider context, float deltaTime)
        {
            entity.RotationAngle = context.HorizontalAxis * entity.AngularSpeed * deltaTime;
        }
    }
}