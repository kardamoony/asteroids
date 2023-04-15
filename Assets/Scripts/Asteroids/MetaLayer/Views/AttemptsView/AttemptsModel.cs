using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Providers;
using UnityEngine;

namespace Asteroids.MetaLayer.Views.AttemptsView
{
    public class AttemptsModel
    {
        private readonly IPlayerAttemptsProvider _attemptsProvider;
        
        public IObjectsFactory<GameObject> Factory { get; }
        
        public int AttemptsCount => _attemptsProvider.Attempts;

        public AttemptsModel(IObjectsFactory<GameObject> factory, IPlayerAttemptsProvider attemptsProvider)
        {
            Factory = factory;
            _attemptsProvider = attemptsProvider;
        }
    }
}