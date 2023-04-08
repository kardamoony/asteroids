using System;
using Asteroids.SimulationLayer.Settings;
using Generated;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public sealed class PlayerEntity : Entity, IPlayer, IMovable, IRotatable, ICollidable, IDestructable, ISpawner
    {
        public event Action <string, GameObject> OnSpawned;
        
        //spawner
        public ISpawner Spawner => this;
        public float SpawnDelay { get; } = 0.5f; //TODO: move in settings 
        public int MaxCount { get; } = 20; //TODO: move in settings
        
        //movable
        public IMovable Movable => this;
        public float Speed { get; private set; }
        public float Velocity { get; set; }
        
        //rotatable
        public IRotatable Rotatable => this;
        public float RotationAngle { get; set; }
        public float AngularSpeed { get; private set; }
        
        //collidable
        public ICollidable Collidable => this;
        public int Damage { get; private set;}
        
        //destructable
        public IDestructable Destructable => this;
        public int Health { get; private set; }
        
        public PlayerEntity(ISettingsProvider settingsProvider, TimeSpan lifetime) : base(settingsProvider, lifetime)
        {
        }
        
        public void InvokeSpawnedEvent(GameObject gameObject, string assetId)
        {
            OnSpawned?.Invoke(assetId, gameObject);
        }

        public void HandleCollisionEnter(ICollidable other)
        {
            Health -= other.Damage; 
        }

        public void HandleCollisionExit(ICollidable other){}

        public void HandleCollisionStay(ICollidable other){}

        protected override void InitializeInternal()
        {
            Speed = SettingsProvider.GetValue<float>(Player.Speed);
            AngularSpeed = SettingsProvider.GetValue<float>(Player.AngularSpeed);
            Health = SettingsProvider.GetValue<int>(Player.Health);
            Damage = SettingsProvider.GetValue<int>(Player.Damage);
        }
    }
}
