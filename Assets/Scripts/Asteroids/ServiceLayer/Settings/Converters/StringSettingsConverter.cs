namespace Asteroids.ServiceLayer.Settings.Converters
{
    public class StringSettingsConverter : SettingsConverter<string>
    {
        public override string Convert(string value)
        {
            return value;
        }
    }
}