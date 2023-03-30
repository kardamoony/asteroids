using System;

namespace Asteroids.SimulationLayer.Entities
{
    public abstract class Entity : IEntity
    {
        public IEntityView EntityView { get; private set; }

        public bool Initialized => EntityView != null;
        public DateTime InitializationTime { get; set; }
        
        public TimeSpan LifeTimeSpan { get; }
        
        public void Initialize(IEntityView entityView)
        {
            EntityView = entityView;
        }

        public void Denitialize()
        {
            EntityView = null;
        }

        protected Entity()
        {
            LifeTimeSpan = default;
        }

        protected Entity(TimeSpan lifeTimeSpan)
        {
            LifeTimeSpan = lifeTimeSpan;
        }
    }
}