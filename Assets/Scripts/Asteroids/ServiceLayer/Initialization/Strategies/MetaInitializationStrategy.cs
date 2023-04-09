using Asteroids.SimulationLayer.Initialization;
using UnityEngine;

namespace Asteroids.ServiceLayer.Initialization.Strategies
{
    public class MetaInitializationStrategy : IInitializationStrategy
    {
        private Transform _uiRoot;
        
        public MetaInitializationStrategy(Transform uiRoot)
        {
            _uiRoot = uiRoot;
        }
        
        public void Initialize()
        {
            RegisterDependencies();
            CreateStartView();
        }

        public void Deinitialize()
        {
            
        }

        private void RegisterDependencies()
        {
            
        }

        private void CreateStartView()
        {
            
        }
    }
}