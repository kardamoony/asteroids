using System;
using System.Collections.Generic;
using Asteroids.CoreLayer.Interfaces;
using Asteroids.SimulationLayer.Entities;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class PlayerMovementSystem : IFixedUpdateSystem
    {
        private readonly HashSet<IPlayer> _players = new HashSet<IPlayer>();
        private readonly IInputProvider _inputProvider;

        public PlayerMovementSystem(IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
        }

        public void Register(IPlayer player)
        {
            _players.Add(player);
        }

        public void Unregister(IPlayer player)
        {
            _players.Remove(player);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            foreach (var player in _players)
            {
                Move(player, fixedDeltaTime);
            }
        }

        private void Move(IPlayer player, float fixedDeltaTime)
        {
            var input = _inputProvider.VerticalAxis;
            var currentVelocity = player.Movable.Velocity;
            
            if (input > 0 && currentVelocity < player.Movable.Speed)
            {
                player.Movable.Velocity = Math.Min(currentVelocity + player.Settings.Acceleration * fixedDeltaTime, player.Movable.Speed);
                return;
            }
            
            if (currentVelocity > 0)
            {
                var decelerationRate = input < 0 ? player.Settings.Brake : player.Settings.Deceleration;
                player.Movable.Velocity = Math.Max(0f, currentVelocity - decelerationRate * fixedDeltaTime);
            }
        }
    }
}
