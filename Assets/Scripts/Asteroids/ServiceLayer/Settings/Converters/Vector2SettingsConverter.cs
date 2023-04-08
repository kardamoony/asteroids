using UnityEngine;

namespace Asteroids.ServiceLayer.Settings.Converters
{
    public class Vector2SettingsConverter : SettingsConverter<Vector2>
    {
        public override Vector2 Convert(string value)
        {
            var split = value.Split(" ");
            return new Vector2(float.Parse(split[0]), float.Parse(split[1]));
        }
    }
}