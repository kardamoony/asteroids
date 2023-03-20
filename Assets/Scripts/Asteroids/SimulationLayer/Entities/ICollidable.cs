using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public interface ICollidable
    {
        void HandleCollisionEnter(Collision collision);
        void HandleCollisionExit(Collision collision);
        void HandleCollisionStay(Collision collision);
    }
}