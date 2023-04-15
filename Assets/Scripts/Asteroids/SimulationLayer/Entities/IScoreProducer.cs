namespace Asteroids.SimulationLayer.Entities
{
    public interface IScoreProducer
    {
        uint Score { get; set; }
        IEntity ScoreReceiver { get; }
    }
}