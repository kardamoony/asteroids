namespace Asteroids.ServiceLayer.Settings.Parsers
{
    public class BoolSettingsConverter : SettingsConverter<bool>
    {
        public override bool Convert(string value)
        {
            return bool.Parse(value);
        }
    }
}