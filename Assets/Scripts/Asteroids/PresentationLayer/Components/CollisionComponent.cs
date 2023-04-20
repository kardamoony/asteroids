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
            Context.Collision = collision;
            var other = collision.gameObject.GetComponent<CollisionComponent>();
            Context.HandleCollisionEnter(other.Context);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!Initialized) return;
            var other = collision.gameObject.GetComponent<CollisionComponent>();
            Context.HandleCollisionExit(other.Context);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!Initialized) return;
            var other = collision.gameObject.GetComponent<CollisionComponent>();
            Context.HandleCollisionStay(other.Context);
        }
    }
}
