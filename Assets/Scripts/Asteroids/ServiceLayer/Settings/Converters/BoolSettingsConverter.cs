namespace Asteroids.ServiceLayer.Settings.Converters
{
    public class BoolSettingsConverter : SettingsConverter<bool>
    {
        public override bool Convert(string value)
        {
            return bool.Parse(value);
        }
    }
}