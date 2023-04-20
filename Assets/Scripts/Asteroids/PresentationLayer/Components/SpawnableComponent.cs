using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.PresentationLayer.Components
{
    public class SpawnableComponent : EntityComponent<ISpawnable>
    {
        protected override void OnContextSet()
        {
            gameObject.SetActive(false);
            Context.OnPositionSet += SetPosition;
        }

        protected override void OnContextWillBeCleared()
        {
            Context.OnPositionSet -= SetPosition;
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }
    }
}