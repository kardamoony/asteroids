using Asteroids.SimulationLayer.Settings;
using UnityEngine;

namespace Asteroids.ServiceLayer.Settings
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Asteroids/Settings/Player")]
    public class PlayerSettings : ScriptableObject, IPlayerSettings
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _angularSpeed;
        
        [Space]
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _brake;
        
        [Space]
        [SerializeField] private int _initialHealth;
        [SerializeField] private int _damage;
        
        [SerializeField, HideInInspector] private string _projectileId;

        public string ProjectileIdProperty => nameof(_projectileId);

        public float Speed => _speed;
        public float AngularSpeed => _angularSpeed;
        public float Acceleration => _acceleration;
        public float Deceleration => _deceleration;
        public float Brake => _brake;
        public int InitialHealth => _initialHealth;
        public int Damage => _damage;
        public string ProjectileId => _projectileId;
    }
}