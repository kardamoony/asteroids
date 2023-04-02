using Asteroids.CoreLayer.AssetsManagement;
using Asteroids.ServiceLayer.Initialization.Strategies;
using Asteroids.ServiceLayer.Settings;
using UnityEngine;

namespace Asteroids.SimulationLayer.Scene
{
    public class InitializationManager : MonoBehaviour
    {
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private AsteroidSettings _asteroidSettings;
        [SerializeField] private AssetsMap _assetsMap;
        
        private void Start()
        {
            new InitializationStrategy(_assetsMap, _playerSettings, _asteroidSettings).InitializeGameplay();
        }
    }
}
