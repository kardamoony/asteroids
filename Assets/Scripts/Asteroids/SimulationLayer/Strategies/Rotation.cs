using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.SimulationLayer.Strategies
{
    public class Rotation : IEntityStrategy<IRotatable>
    {
        public void Execute(IRotatable entity, IInputProvider inputProvider, float deltaTime)
        {
            entity.RotationAngle = Mathf.Abs(inputProvider.HorizontalAxis) > float.Epsilon
                ? inputProvider.HorizontalAxis * entity.AngularSpeed * deltaTime
                : 0f;
        }
    }
}