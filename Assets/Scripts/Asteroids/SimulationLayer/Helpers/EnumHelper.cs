using System;

namespace Asteroids.SimulationLayer.Helpers
{
    public static class EnumHelper
    {
        public static string ToFullString(this Enum @enum)
        {
            return @enum.GetType().Name + "." + @enum;
        }
    }
}