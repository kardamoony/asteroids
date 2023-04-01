using Asteroids.PresentationLayer.Enums;
using UnityEngine;

namespace Asteroids.PresentationLayer.Helpers
{
    public static class RandomHelper
    {
        public static ScreenBorder GetRandomBorder()
        {
            var rnd = Random.Range(0, 4);
            return (ScreenBorder)rnd;
        }
    }
}