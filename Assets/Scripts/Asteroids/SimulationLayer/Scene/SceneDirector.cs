using System.Collections.Generic;
using System.Linq;
using Asteroids.SimulationLayer.GameSystems;
using UnityEngine;

namespace Asteroids.SimulationLayer.Scene
{
    public class SceneDirector : MonoBehaviour
    {
        private IUpdateSystem[] _updateSystems;
        private IFixedUpdateSystem[] _fixedUpdateSystems;
        
        private bool _initialized;

        public void Initialize(IEnumerable<IUpdateSystem> updateSystems, IEnumerable<IFixedUpdateSystem> fixedUpdateSystems)
        {
            _updateSystems = updateSystems.ToArray();
            _fixedUpdateSystems = fixedUpdateSystems.ToArray();
            
            _initialized = true;
        }

        private void Update()
        {
            if (!_initialized) return;
        
            foreach (var system in _updateSystems)
            {
                system.Update(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (!_initialized) return;
            
            foreach (var system in _fixedUpdateSystems)
            {
                system.FixedUpdate(Time.fixedDeltaTime);
            }
        }
    }
}
