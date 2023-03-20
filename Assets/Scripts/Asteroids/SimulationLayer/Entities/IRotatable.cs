namespace Asteroids.SimulationLayer.Entities
{
    public interface IRotatable
    {
        float RotationAngle { get; set; }
        float AngularSpeed { get; }
    }
}