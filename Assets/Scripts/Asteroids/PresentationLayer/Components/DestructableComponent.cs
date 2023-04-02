using Asteroids.SimulationLayer.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.PresentationLayer.Components
{
    public class DestructableComponent : EntityComponent<IDestructable>
    {
        [SerializeField] private UnityEvent _onDestruction;

        private bool _onDestructionInvoked;

        protected override void OnContextSet()
        {
            _onDestructionInvoked = false;
        }

        private void Update()
        {
            if (_onDestructionInvoked || Context.Health > 0)
            {
                return;
            }

            _onDestructionInvoked = true;
            _onDestruction?.Invoke();
        }
    }
}