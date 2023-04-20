using System;
using Asteroids.SimulationLayer.Settings;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public class ExplosionEntity : Entity, ISpawnable
    {
        public event Action<Vector3> OnPositionSet;
        
        public ExplosionEntity(ISettingsProvider settingsProvider, TimeSpan lifeTimeSpan) : base(settingsProvider, lifeTimeSpan)
        {
        }
        
        public void SetPosition(Vector3 position)
        {
            OnPositionSet?.Invoke(position);
        }

        protected override void InitializeInternal(){}
    }
}