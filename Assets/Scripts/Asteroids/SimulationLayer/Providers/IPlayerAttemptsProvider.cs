using System;

namespace Asteroids.SimulationLayer.Providers
{
    public interface IPlayerAttemptsProvider
    {
        event Action<uint> OnPlayerGameOver; 
        int Attempts { get; }
    }
}