using System.Collections.Generic;
using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class EntityLifetimeSystem : IUpdateSystem
    {
        private readonly IObjectsFactory<GameObject> _factory;
        //private Dictionary<>

        public EntityLifetimeSystem(IObjectsFactory<GameObject> factory)
        {
            _factory = factory;
        }
        
        public void Update(float deltaTime)
        {
            
        }

        public void Register(IDestructable destructable)
        {
            
        }
        
        //private void 
    }
}