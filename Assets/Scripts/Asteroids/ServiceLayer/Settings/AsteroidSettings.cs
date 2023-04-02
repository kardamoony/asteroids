using Asteroids.SimulationLayer.Settings;
using UnityEngine;

namespace Asteroids.ServiceLayer.Settings
{
    [CreateAssetMenu(fileName = "AsteroidSettings", menuName = "Asteroids/Settings/Asteroid")]
    public class AsteroidSettings : ScriptableObject, IAsteroidSettings
    {
        [SerializeField] private int _spawnDelay;
        [SerializeField] private int _maxCount;
        
        [Space]
        [SerializeField] private int _damage;
        [SerializeField] private int _health;
        
        [Space]
        [SerializeField] private Vector2 _speedRange;

        [SerializeField, HideInInspector] private string _asteroidAssetId;

        public string AsteroidAssetPropertyName => nameof(_asteroidAssetId);

        public float SpawnDelay => _spawnDelay;
        public int MaxCount => _maxCount;
        public int Damage => _damage;
        public int Health => _health;
        public float Speed => Random.Range(_speedRange.x, _speedRange.y);
        public string AsteroidAssetId => _asteroidAssetId;
    }
}