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
    }
}