namespace Asteroids.PresentationLayer.Extensions
{
    public static class FloatExtensions
    {
        public static float Remap01(this float value, float min, float max)
        {
            return (value - min) / (max - min);
        }
    }
}