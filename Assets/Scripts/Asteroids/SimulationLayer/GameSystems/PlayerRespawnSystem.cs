using Asteroids.SimulationLayer.DataHolders;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class PlayerRespawnSystem : ContextSystem<IPlayer, PlayerTriesInfo>, IUpdateSystem
    {
        private readonly IContextEntityStrategy<IPlayer, PlayerTriesInfo> _strategy;
        
        public PlayerRespawnSystem(IContextEntityStrategy<IPlayer, PlayerTriesInfo> strategy)
        {
            _strategy = strategy;
        }

        public void Update(float deltaTime)
        {
            EntitiesContextMap.Update();
            
            EntitiesContextMap.Foreach((player, tries) =>
            {
                _strategy.Execute(player, tries, deltaTime);
            });
        }
    }
}