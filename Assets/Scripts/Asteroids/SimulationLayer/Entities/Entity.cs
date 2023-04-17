using System;
using Asteroids.SimulationLayer.Settings;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public abstract class Entity : IEntity
    {
        protected readonly ISettingsProvider SettingsProvider;

        public event Action<IEntity, GameObject> OnInitialized;
        public event Action<IEntity, GameObject> OnDeinitialized;
        
        public IEntityView EntityView { get; private set; }
        
        public IEntity Owner { get; set; }

        public bool Initialized => EntityView != null;
        public DateTime InitializationTime { get; set; }
        
        public TimeSpan LifeTimeSpan { get; }
        
        protected Entity(ISettingsProvider settingsProvider, TimeSpan lifeTimeSpan)
        {
            SettingsProvider = settingsProvider;
            LifeTimeSpan = lifeTimeSpan;
        }
        
        public void Initialize(IEntityView entityView)
        {
            InitializeInternal();
            EntityView = entityView;
            OnInitialized?.Invoke(this, EntityView.GameObject);
        }

        public void Denitialize()
        {
            OnDeinitialized?.Invoke(this, EntityView.GameObject);
            EntityView = null;
        }

        protected abstract void InitializeInternal();
    }
}