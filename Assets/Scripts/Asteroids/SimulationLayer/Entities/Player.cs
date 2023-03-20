using Asteroids.SimulationLayer.Settings;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public class Player : IPlayer, IMovable, IRotatable, ICollidable
    {
        public IPlayerSettings Settings { get; }
        public IMovable Movable => this;
        public IRotatable Rotatable => this;
        public ICollidable Collidable => this;
        
        public Vector3 Translation { get; set; }
        public float RotationAngle { get; set; }
        public float AngularSpeed { get; }
        public float Speed { get; }
        public float Velocity { get; set; }

        public Player(IPlayerSettings settingsProvider)
        {
            Settings = settingsProvider;
            Speed = settingsProvider.Speed;
            AngularSpeed = settingsProvider.AngularSpeed;
        }

        public void HandleCollisionEnter(Collision collision)
        {
            Debug.Log("CollisionEnter " + collision.gameObject.name);
        }

        public void HandleCollisionExit(Collision collision)
        {
            Debug.Log("CollisionExit " + collision.gameObject.name);
        }

        public void HandleCollisionStay(Collision collision)
        {
            Debug.Log("CollisionStay " + collision.gameObject.name);
        }
    }
}
