using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.SimulationLayer.Strategies
{
    public class Rotation : IContextEntityStrategy<IRotatable, IInputProvider>
    {
        public void Execute(IRotatable entity, IInputProvider context, float deltaTime)
        {
            entity.RotationAngle = Mathf.Abs(context.HorizontalAxis) > float.Epsilon
                ? context.HorizontalAxis * entity.AngularSpeed * deltaTime
                : 0f;
        }
    }
}