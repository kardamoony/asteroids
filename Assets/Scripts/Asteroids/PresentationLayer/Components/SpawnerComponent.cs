using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.PresentationLayer.Components
{
    public class SpawnerComponent : EntityComponent<ISpawner>
    {
        private Transform _cachedTransform;
        
        protected override void OnContextSet()
        {
            _cachedTransform = transform;
            Context.OnSpawned += HandleObjectSpawned;
        }

        protected override void OnContextCleared()
        {
            Context.OnSpawned -= HandleObjectSpawned;
        }

        protected virtual void HandleObjectSpawned(string id, GameObject o)
        {
            var t = o.transform;
            
            t.parent = null;
            t.position = _cachedTransform.position;
            t.rotation = _cachedTransform.rotation;
        }
    }
}