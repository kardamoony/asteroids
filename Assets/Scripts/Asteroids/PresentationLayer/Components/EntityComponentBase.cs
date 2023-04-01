using Asteroids.PresentationLayer.Views;
using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.PresentationLayer.Components
{
    [RequireComponent(typeof(EntityView))]
    public abstract class EntityComponentBase : MonoBehaviour, IEntityComponent
    {
        public abstract void ClearContext();
    }
}