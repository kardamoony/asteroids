using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.PresentationLayer.Components
{
    public class LocalRotationComponent : EntityComponent<IRotatable>
    {
        private Transform _transform;

        private void Update()
        {
            if (!Initialized)
            {
                return;
            }

            _transform.Rotate(_transform.up, Context.RotationAngle, Space.Self);
        }
        
        private void Awake()
        {
            _transform = transform;
        }
    }
}
