using System;
using Asteroids.SimulationLayer.Settings;
using Generated;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.SimulationLayer.Entities
{
    public class AsteroidEntity : Entity, IMovable, ICollidable, IDestructable, IScoreProducer, IExplodable
    {
        private uint _destructionScore;
        
        //IScoreProducer
        public uint Score { get; set; }
        public IEntity ScoreReceiver { get; private set; }
        
        //IMovable
        public float Speed { get; private set; }
        public float Velocity { get; set; }
        
        //IDestructable
        public int Health { get; private set; }

        //ICollidable
        public int Damage { get; private set; }
        public Collision Collision { get; set; }
        
        //IExplodable
        public string ExplosionAssetId { get; private set; }
        public Vector3 ExplosionPosition { get; private set; }
        public bool Explode { get; set; }

        public AsteroidEntity(ISettingsProvider settingsProvider, TimeSpan lifeTime) : base(settingsProvider, lifeTime)
        {
        }
        
        public void HandleCollisionEnter(ICollidable collision)
        {
            Health -= collision.Damage;
            
            var entity = collision as IEntity;
            ScoreReceiver = entity?.Owner ?? entity;
            Score = _destructionScore;

            if (Health < 1)
            {
                Explode = true;
                ExplosionPosition = Collision.transform.position;
            }
        }

        public void HandleCollisionExit(ICollidable collision){}

        public void HandleCollisionStay(ICollidable collision){}

        protected override void InitializeInternal()
        {
            var speedRange = SettingsProvider.GetValue<Vector2>(Asteroid.SpeedRange);
            Speed = Random.Range(speedRange.x, speedRange.y);
            
            Health = SettingsProvider.GetValue<int>(Asteroid.Health);
            Damage = SettingsProvider.GetValue<int>(Asteroid.Damage);
            ExplosionAssetId = SettingsProvider.GetValue<string>(Asteroid.ExplosionAsset);
            
            _destructionScore = SettingsProvider.GetValue<uint>(Asteroid.Score);
            Explode = false;
        }
    }
}
