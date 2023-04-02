using System.Collections.Generic;
using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.PresentationLayer.Views
{
    [DisallowMultipleComponent]
    public class EntityView : MonoBehaviour, IEntityView
    {
        [SerializeField] private EntityComponentBase[] _components;

        public GameObject GameObject => gameObject;

        public IEnumerable<IEntityComponent> GetComponents()
        {
            return _components;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _components = GetComponents<EntityComponentBase>();
        }

        [ContextMenu("Collect Components")]
        private void CollectComponents()
        {
            _components = GetComponents<EntityComponentBase>();
        }
#endif
    }
}