using System;

namespace Asteroids.SimulationLayer.Entities
{
    public interface IEntity
    {
        event Action<IEntity> OnInitialized;
        event Action<IEntity> OnDeinitialized;

        IEntityView EntityView { get; }
        IEntity Owner { get; set; }
        
        bool Initialized { get; }
        
        DateTime InitializationTime { get; set; }
        TimeSpan LifeTimeSpan { get; }

        void Initialize(IEntityView entityView);
        void Denitialize();
    }
}