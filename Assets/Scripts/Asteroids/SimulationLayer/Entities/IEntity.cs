using System;

namespace Asteroids.SimulationLayer.Entities
{
    public interface IEntity
    {
        IEntityView EntityView { get; }
        
        bool Initialized { get; }
        
        DateTime InitializationTime { get; set; }
        TimeSpan LifeTimeSpan { get; }

        void Initialize(IEntityView entityView);
        void Denitialize();
    }
}