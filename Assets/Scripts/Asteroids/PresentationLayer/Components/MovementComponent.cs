using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.PresentationLayer.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementComponent : EntityComponent<IMovable>
    {
        private Transform _transform;
        private Rigidbody _rb;

        private void FixedUpdate()
        {
            if (!Initialized)
            {
                return;
            }

            _rb.velocity = _transform.forward * Context.Velocity;
        }

        private void Awake()
        {
            _transform = transform;
            _rb = GetComponent<Rigidbody>();
        }
    }
}
