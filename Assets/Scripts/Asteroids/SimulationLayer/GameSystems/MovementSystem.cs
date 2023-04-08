﻿using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Strategies;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class MovementSystem : EntityContextSystem<IMovable, IInputProvider>, IFixedUpdateSystem
    {
        private readonly IContextEntityStrategy<IMovable, IInputProvider> _strategy;

        public MovementSystem( IContextEntityStrategy<IMovable, IInputProvider> strategy)
        {
            _strategy = strategy;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            EntitiesContextMap.Update();
            EntitiesContextMap.Foreach((entity, input) => _strategy.Execute(entity, input, fixedDeltaTime));
        }
    }
}