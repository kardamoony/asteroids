using System;
using Asteroids.SimulationLayer.Settings;
using Generated;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public class Player : Entity, IPlayer, IMovable, IRotatable, ICollidable, ISpawner
    {
        public event Action <string, GameObject> OnSpawned;
        
        //spawner
        public ISpawner Spawner => this;
        public AssetId SpawnedAssetId { get; }
        public float SpawnDelay { get; } = 0.5f; //TODO: move in settings 
        public int EntitesMaxCount { get; } = 20; //TODO: move in settings
        
        //movable
        public IMovable Movable => this;
        public float Speed { get; }
        public float Velocity { get; set; }
        
        //rotatable
        public IRotatable Rotatable => this;
        public float RotationAngle { get; set; }
        public float AngularSpeed { get; }
        
        //collidable
        public ICollidable Collidable => this;
        public int Health { get; set; }
        public int Damage { get; }
        
        public Player(IPlayerSettings settingsProvider)
        {
            Speed = settingsProvider.Speed;
            AngularSpeed = settingsProvider.AngularSpeed;
            Health = settingsProvider.InitialHealth;
            Damage = settingsProvider.Damage;
            
            SpawnedAssetId = settingsProvider.ProjectileId;
        }
        
        public void InvokeSpawnedEvent(GameObject gameObject)
        {
            OnSpawned?.Invoke(SpawnedAssetId.ToString(), gameObject);
        }

        public void HandleCollisionEnter(ICollidable other)
        {
            Health -= other.Damage; 
        }

        public void HandleCollisionExit(ICollidable other){}

        public void HandleCollisionStay(ICollidable other){}
        
    }
}
