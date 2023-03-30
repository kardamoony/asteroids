using System;
using Asteroids.SimulationLayer.Settings;
using Generated;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public class Player : Entity, IPlayer, IMovable, IRotatable, ICollidable, ISpawner
    {
        public event Action <GameObject> OnSpawned;
        
        public IMovable Movable => this;
        public IRotatable Rotatable => this;
        public ICollidable Collidable => this;
        public ISpawner Spawner => this;
        
        public AssetId SpawnedAssetId { get; }

        public float RotationAngle { get; set; }
        public float AngularSpeed { get; }
        public float Speed { get; }
        public float Velocity { get; set; }
        
        public int Health { get; set; }
        public int Damage { get; }

        public float SpawnDelay { get; } = 0.5f;
        public void InvokeSpawnedEvent(GameObject gameObject)
        {
            OnSpawned?.Invoke(gameObject);
        }

        public Player(IPlayerSettings settingsProvider)
        {
            Speed = settingsProvider.Speed;
            AngularSpeed = settingsProvider.AngularSpeed;
            Health = settingsProvider.InitialHealth;
            Damage = settingsProvider.Damage;
            
            SpawnedAssetId = settingsProvider.ProjectileId;
        }

        public void HandleCollisionEnter(ICollidable other)
        {
            Health -= other.Damage; 
        }

        public void HandleCollisionExit(ICollidable other){}

        public void HandleCollisionStay(ICollidable other){}
        
    }
}
