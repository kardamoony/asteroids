using Generated;
using UnityEngine;

namespace Asteroids.SimulationLayer.Settings
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Asteroids/Settings/Player")]
    public class PlayerSettings : ScriptableObject, IPlayerSettings
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _angularSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _brake;
        [SerializeField] private int _initialHealth;
        [SerializeField] private int _damage;

        [Space] 
        [SerializeField] private AssetId _projectileId;

        public float Speed => _speed;
        public float AngularSpeed => _angularSpeed;
        public float Acceleration => _acceleration;
        public float Deceleration => _deceleration;
        public float Brake => _brake;
        public int InitialHealth => _initialHealth;
        public int Damage => _damage;
        public AssetId ProjectileId => _projectileId;
    }
}