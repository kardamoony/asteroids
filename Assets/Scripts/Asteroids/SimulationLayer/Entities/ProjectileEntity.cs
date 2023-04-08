using System;
using Asteroids.SimulationLayer.Settings;
using Generated;

namespace Asteroids.SimulationLayer.Entities
{
    public sealed class ProjectileEntity : Entity, IProjectile, IMovable, ICollidable, IDestructable
    {
        public IMovable Movable => this;
        public float Speed { get; private set; }
        public float Velocity { get; set; }
        public int Health { get; private set; }
        public int Damage { get; private set; }

        public ProjectileEntity(ISettingsProvider settingsProvider, TimeSpan lifeTime) : base(settingsProvider, lifeTime)
        {
        }

        public void HandleCollisionEnter(ICollidable other)
        {
            Health = 0;
        }

        public void HandleCollisionExit(ICollidable other){}

        public void HandleCollisionStay(ICollidable other){}
        
        protected override void InitializeInternal()
        {
            Speed = SettingsProvider.GetValue<float>(Projectile.Speed);
            Health = SettingsProvider.GetValue<int>(Projectile.Health);
            Damage = SettingsProvider.GetValue<int>(Projectile.Damage);
        }
    }
}