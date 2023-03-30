using Asteroids.CoreLayer.AssetsManagement;
using Asteroids.ServiceLayer.Initialization.Strategies;
using Asteroids.SimulationLayer.Settings;
using UnityEngine;

namespace Asteroids.SimulationLayer.Scene
{
    public class InitializationManager : MonoBehaviour
    {
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private AssetsMap _assetsMap;
        

        /*[ContextMenu("CreateAsteroid")]
        private void CreateAsteroid()
        {
            IoC.Instance.Resolver.Resolve<IObjectsFactory<GameObject>>()
                .Get<IEntityView>(AssetId.Asteroid.ToString(), o =>
                {
                    var asteroid = new Asteroid(2);
                    IoC.Instance.Resolver.Resolve<IEntityInitializer>().InitializeEntity(asteroid, o);
                });
        }*/

       
        private void Start()
        {
            new InitializationStrategy(_assetsMap, _playerSettings).InitializeGameplay();
        }
    }
}
