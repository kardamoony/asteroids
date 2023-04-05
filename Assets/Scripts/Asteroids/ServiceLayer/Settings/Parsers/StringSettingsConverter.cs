namespace Asteroids.ServiceLayer.Settings.Parsers
{
    public class StringSettingsConverter : SettingsConverter<string>
    {
        public override string Convert(string value)
        {
            return value;
        }
    }
}