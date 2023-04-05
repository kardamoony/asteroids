using Asteroids.CoreLayer.Factories;
using Asteroids.SimulationLayer.DataHolders;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Initialization;
using Generated;
using UnityEngine;

namespace Asteroids.SimulationLayer.Strategies
{
    public class PlayerRespawnStrategy : IContextEntityStrategy<IPlayer, PlayerTriesInfo>
    {
        private readonly IEntityInitializer _initializer;
        private readonly IObjectsFactory<GameObject> _factory;
        
        public PlayerRespawnStrategy(IEntityInitializer initializer, IObjectsFactory<GameObject> factory)
        {
            _initializer = initializer;
            _factory = factory;
        }
        
        public void Execute(IPlayer entity, PlayerTriesInfo context, float deltaTime)
        {
            if (entity.Destructable.Health < 1 && context.CountLeft > 0)
            {
                _factory.Get<IEntityView>(AssetId.Player.ToString(), o =>
                {
                    //entity.
                });
            }
        }
    }
}