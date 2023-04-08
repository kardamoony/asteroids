using System;
using System.Globalization;

namespace Asteroids.ServiceLayer.Settings.Converters
{
    public class TimeSpanSettingConverter : SettingsConverter<TimeSpan>
    {
        public override TimeSpan Convert(string value)
        {
            return TimeSpan.Parse(value, CultureInfo.InvariantCulture);
        }
    }
}