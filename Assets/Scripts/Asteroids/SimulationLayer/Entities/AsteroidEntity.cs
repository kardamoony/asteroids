using System;
using Asteroids.SimulationLayer.Settings;
using Generated;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.SimulationLayer.Entities
{
    public class AsteroidEntity : Entity, IMovable, ICollidable, IDestructable
    {
        public float Speed { get; private set; }
        public float Velocity { get; set; }
        
        public int Health { get; private set; }
        
        public int Damage { get; private set; }

        public AsteroidEntity(ISettingsProvider settingsProvider, TimeSpan lifeTime) : base(settingsProvider, lifeTime)
        {
        }
        
        public void HandleCollisionEnter(ICollidable collision)
        {
            Health -= collision.Damage;
        }

        public void HandleCollisionExit(ICollidable collision){}

        public void HandleCollisionStay(ICollidable collision){}

        protected override void InitializeInternal()
        {
            var speedRange = SettingsProvider.GetValue<Vector2>(Asteroid.SpeedRange);
            Speed = Random.Range(speedRange.x, speedRange.y);
            
            Health = SettingsProvider.GetValue<int>(Asteroid.Health);
            Damage = SettingsProvider.GetValue<int>(Asteroid.Damage);
        }
    }
}
