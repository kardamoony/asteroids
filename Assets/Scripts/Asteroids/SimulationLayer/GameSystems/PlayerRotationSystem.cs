using System.Collections.Generic;
using Asteroids.CoreLayer.Interfaces;
using Asteroids.SimulationLayer.Entities;
using UnityEngine;

namespace Asteroids.SimulationLayer.GameSystems
{
    public class PlayerRotationSystem : IUpdateSystem
    {
        private readonly HashSet<IPlayer> _players = new HashSet<IPlayer>();
        private readonly IInputProvider _inputProvider;

        public PlayerRotationSystem(IInputProvider inputProvider)
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

        public void Update(float deltaTime)
        {
            foreach (var player in _players)
            {
                Rotate(player, deltaTime);
            }
        }
        
        private void Rotate(IPlayer player, float deltaTime)
        {
            var angle = 0f;
            
            if (Mathf.Abs(_inputProvider.HorizontalAxis) > float.Epsilon)
            {
                angle = _inputProvider.HorizontalAxis * player.Rotatable.AngularSpeed * deltaTime;
            }

            player.Rotatable.RotationAngle = angle;
        }
    }
}
