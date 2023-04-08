using Asteroids.CoreLayer.AssetsManagement;
using Asteroids.ServiceLayer.Initialization.Strategies;
using Asteroids.ServiceLayer.Settings;
using UnityEngine;

namespace Asteroids.SimulationLayer.Scene
{
    public class InitializationManager : MonoBehaviour
    {
        [SerializeField] private AssetsMap _assetsMap;
        [SerializeField] private GameplaySettings _gameplaySettings;
        
        private void Start()
        {
            new InitializationStrategy(_assetsMap, _gameplaySettings).InitializeGameplay();
        }
    }
}
