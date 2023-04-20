using System;
using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public interface ISpawnable
    {
        event Action<Vector3> OnPositionSet; 
        void SetPosition(Vector3 position);
    }
}