using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public interface IEntityView
    {
        IEnumerable<IEntityComponent> GetComponents();
        GameObject GameObject { get; }
    }
}