using Asteroids.PresentationLayer.Behaviours;
using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.PresentationLayer.Components
{
    public class SpawnerComponent : EntityComponent<ISpawner>
    {
        [SerializeField] private ObjectPlacer _objectPlacer;

        protected override void OnContextSet()
        {
            Context.OnSpawned += HandleObjectSpawned;
        }

        protected override void OnContextWillBeCleared()
        {
            Context.OnSpawned -= HandleObjectSpawned;
        }

        private void HandleObjectSpawned(string id, GameObject o)
        {
            if (!_objectPlacer)
            {
                _objectPlacer = gameObject.AddComponent<ObjectPlacer>();
            }
            
            _objectPlacer.Place(o.transform);
        }
    }
}