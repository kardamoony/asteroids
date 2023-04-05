namespace Asteroids.ServiceLayer.Settings.Parsers
{
    public class IntSettingsConverter : SettingsConverter<int>
    {
        public override int Convert(string value)
        {
            return int.Parse(value);
        }
    }
}