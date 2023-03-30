using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.PresentationLayer.Components
{
    public abstract class EntityComponentBase : MonoBehaviour, IEntityComponent
    {
        public abstract void ClearContext();
    }
}