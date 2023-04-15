namespace Asteroids.ServiceLayer.Settings.Converters
{
    public class UintSettingsConverter : SettingsConverter<uint>
    {
        public override uint Convert(string value)
        {
            return uint.Parse(value);
        }
    }
}