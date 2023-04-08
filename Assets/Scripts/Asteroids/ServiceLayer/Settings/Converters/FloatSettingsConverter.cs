namespace Asteroids.ServiceLayer.Settings.Converters
{
    public class FloatSettingsConverter : SettingsConverter<float>
    {
        public override float Convert(string value)
        {
            return float.Parse(value);
        }
    }
}