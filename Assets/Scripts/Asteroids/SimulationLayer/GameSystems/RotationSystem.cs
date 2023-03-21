using System.Collections.Generic;
using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Models;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class RotationSystem : IUpdateSystem
    {
        private readonly IRotationModel _rotationModel;
        private readonly Dictionary<IRotatable, IInputProvider> _rotatingEntities = new Dictionary<IRotatable, IInputProvider>();

        public RotationSystem(IRotationModel rotationModel)
        {
            _rotationModel = rotationModel;
        }

        public void Update(float deltaTime)
        {
            foreach (var pair in _rotatingEntities)
            {
                _rotationModel.Rotate(pair.Key, pair.Value, deltaTime);
            }
        }

        public void Register(IRotatable rotatable, IInputProvider inputProvider)
        {
            if (_rotatingEntities.ContainsKey(rotatable)) return;
            _rotatingEntities.Add(rotatable, inputProvider);
        }

        public void Unregister(IRotatable rotatable)
        {
            if (!_rotatingEntities.ContainsKey(rotatable)) return;
            _rotatingEntities.Remove(rotatable);
        }
    }
}
