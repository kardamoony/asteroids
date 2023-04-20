using UnityEngine;

namespace Asteroids.SimulationLayer.Entities
{
    public interface IExplodable
    {
        string ExplosionAssetId { get; }
        Vector3 ExplosionPosition { get; }
        bool Explode { get; set; }
    }
}