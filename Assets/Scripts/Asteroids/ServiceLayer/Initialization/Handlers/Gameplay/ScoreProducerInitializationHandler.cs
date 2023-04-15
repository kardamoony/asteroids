using Asteroids.PresentationLayer.Components;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.ServiceLayer.Initialization.Handlers.Gameplay
{
    public class ScoreProducerInitializationHandler : IInitializationHandler<IEntity, IEntityComponent>
    {
        public IInitializationHandler<IEntity, IEntityComponent> Next { get; set; }
        
        public void HandleInitialization(IEntity @object, IEntityComponent context)
        {
            if (@object is IScoreProducer scoreProducer && context is ScoreComponent)
            {
                IoC.Locator.Instance.Resolver.Resolve<ScoreCountingSystem>().Register(scoreProducer);
                return;
            }
            
            Next?.HandleInitialization(@object, context);
        }

        public void HandleDeinitialization(IEntity @object)
        {
            if (@object is IScoreProducer scoreProducer)
            {
                IoC.Locator.Instance.Resolver.Resolve<ScoreCountingSystem>().Unregister(scoreProducer);
            }
            
            Next?.HandleDeinitialization(@object);
        }
    }
}