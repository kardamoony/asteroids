using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.SimulationLayer.Models
{
    public class Rotation : IRotationModel
    {
        public void Rotate(IRotatable rotatable, IInputProvider inputProvider, float deltaTime)
        {
            rotatable.RotationAngle = Mathf.Abs(inputProvider.HorizontalAxis) > float.Epsilon
                ? inputProvider.HorizontalAxis * rotatable.AngularSpeed * deltaTime
                : 0f;
        }
    }
}