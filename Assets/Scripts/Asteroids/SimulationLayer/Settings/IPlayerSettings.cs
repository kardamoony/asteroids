﻿namespace Asteroids.SimulationLayer.Settings
{
    public interface IPlayerSettings
    {
        float Speed { get; }
        float AngularSpeed { get; }
        float Acceleration { get; }
        float Deceleration { get; }
        float Brake { get; }
    }
}