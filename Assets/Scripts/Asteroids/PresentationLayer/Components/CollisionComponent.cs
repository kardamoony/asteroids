using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.PresentationLayer.Components
{
    [RequireComponent(typeof(Collider))]
    public class CollisionComponent : EntityComponent<ICollidable>
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (!Initialized) return;
            Context.HandleCollisionEnter(collision);
        }

        private void OnCollisionExit(Collision other)
        {
            if (!Initialized) return;
            Context.HandleCollisionExit(other);
        }

        private void OnCollisionStay(Collision collisionInfo)
        {
            if (!Initialized) return;
            Context.HandleCollisionStay(collisionInfo);
        }
    }
}
