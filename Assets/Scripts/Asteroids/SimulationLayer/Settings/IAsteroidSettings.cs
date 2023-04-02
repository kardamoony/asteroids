namespace Asteroids.SimulationLayer.Settings
{
    public interface IAsteroidSettings
    {
        float SpawnDelay { get; }
        int MaxCount { get; }
        
        int Damage { get; }
        int Health { get; }
        
        float Speed { get; }
        
        string AsteroidAssetId { get; }
    }
}