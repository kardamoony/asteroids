namespace Asteroids.CoreLayer.Input
{
    public interface IInputProvider
    {
        float VerticalAxis { get; }
        float HorizontalAxis { get; }
        bool Fire { get; }
        bool Action { get; }
    }
}
