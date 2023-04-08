using System;
using Asteroids.SimulationLayer.Settings;

namespace Asteroids.SimulationLayer.Entities
{
    public abstract class Entity : IEntity
    {
        protected readonly ISettingsProvider SettingsProvider;

        public event Action<IEntity> OnInitialized;
        public event Action<IEntity> OnDeinitialized;
        
        public IEntityView EntityView { get; private set; }

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
            OnInitialized?.Invoke(this);
        }

        public void Denitialize()
        {
            EntityView = null;
            OnDeinitialized?.Invoke(this);
        }

        protected abstract void InitializeInternal();
    }
}