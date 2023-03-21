namespace Asteroids.CoreLayer.Input
{
    public class ConstantInputProvider : IInputProvider
    {
        public float VerticalAxis { get; set; }
        public float HorizontalAxis { get; set; }
        public bool Fire { get; set; }
        public bool Action { get; set; }
    }
}