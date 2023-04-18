using Asteroids.SimulationLayer.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.PresentationLayer.Components
{
    [DisallowMultipleComponent]
    public class RotationComponent : EntityComponent<IRotatable>
    {
        [SerializeField] private UnityEvent<float> _rotationEvent;

        private Transform _transform;

        private void Update()
        {
            if (!Initialized)
            {
                return;
            }

            _transform.Rotate(_transform.up, Context.RotationAngle, Space.Self);
            _rotationEvent?.Invoke(Context.RotationAngle);
        }
        
        private void Awake()
        {
            _transform = transform;
        }
    }
}
