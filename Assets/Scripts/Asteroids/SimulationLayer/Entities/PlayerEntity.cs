using System;
using Asteroids.SimulationLayer.Settings;
using Generated;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public sealed class PlayerEntity : Entity, IPlayer, IMovable, IRotatable, ICollidable, IDestructable, ISpawner, IExplodable
    {
        public uint Id { get; }
        
        //ISpawner
        public event Action <string, GameObject> OnSpawned;
        public ISpawner Spawner => this;
        public IEntity SpawnOwner => this;
        public float SpawnDelay { get; } = 0.5f; //TODO: move in settings 
        public int MaxCount { get; } = 20; //TODO: move in settings
        
        //IMovable
        public IMovable Movable => this;
        public float Speed { get; private set; }
        public float Velocity { get; set; }
        
        //IRotatable
        public IRotatable Rotatable => this;
        public float RotationAngle { get; set; }
        public float AngularSpeed { get; private set; }
        
        //ICollidable
        public ICollidable Collidable => this;
        public int Damage { get; private set;}
        public Collision Collision { get; set; }

        //IDestructable
        public IDestructable Destructable => this;
        public int Health { get; private set; }
        
        //IExplodable
        public string ExplosionAssetId { get; private set; }
        public Vector3 ExplosionPosition { get; private set; }
        public bool Explode { get; set; }
        
        public PlayerEntity(uint id, ISettingsProvider settingsProvider, TimeSpan lifetime) : base(settingsProvider, lifetime)
        {
            Id = id;
        }
        
        public void InvokeSpawnedEvent(GameObject gameObject, string assetId)
        {
            OnSpawned?.Invoke(assetId, gameObject);
        }
        
        public void HandleCollisionEnter(ICollidable other)
        {
            Health -= other.Damage;
            
            if (Health < 1)
            {
                Explode = true;
                ExplosionPosition = Collision.transform.position;
            }
        }

        public void HandleCollisionExit(ICollidable other){}

        public void HandleCollisionStay(ICollidable other){}

        protected override void InitializeInternal()
        {
            Speed = SettingsProvider.GetValue<float>(Player.Speed);
            AngularSpeed = SettingsProvider.GetValue<float>(Player.AngularSpeed);
            Health = SettingsProvider.GetValue<int>(Player.Health);
            Damage = SettingsProvider.GetValue<int>(Player.Damage);
            ExplosionAssetId = SettingsProvider.GetValue<string>(Player.ExplosionAsset);
            Explode = false;
        }
    }
}
