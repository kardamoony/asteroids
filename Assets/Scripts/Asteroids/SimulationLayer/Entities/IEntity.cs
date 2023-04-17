using System;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public interface IEntity
    {
        event Action<IEntity, GameObject> OnInitialized;
        event Action<IEntity, GameObject> OnDeinitialized;

        IEntityView EntityView { get; }
        IEntity Owner { get; set; }
        
        bool Initialized { get; }
        
        DateTime InitializationTime { get; set; }
        TimeSpan LifeTimeSpan { get; }

        void Initialize(IEntityView entityView);
        void Denitialize();
    }
}