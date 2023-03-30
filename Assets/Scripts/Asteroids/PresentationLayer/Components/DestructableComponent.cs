using Asteroids.SimulationLayer.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.PresentationLayer.Components
{
    public class DestructableComponent : EntityComponent<IDestructable>
    {
        [SerializeField] private UnityEvent _onDestruction;

        private void Update()
        {
            if (Context.Health > 0)
            {
                return;
            }
            
            _onDestruction?.Invoke();
        }
    }
}