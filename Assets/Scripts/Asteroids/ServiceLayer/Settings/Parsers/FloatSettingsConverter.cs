namespace Asteroids.ServiceLayer.Settings.Parsers
{
    public class FloatSettingsConverter : SettingsConverter<float>
    {
        public override float Convert(string value)
        {
            return float.Parse(value);
        }
    }
}