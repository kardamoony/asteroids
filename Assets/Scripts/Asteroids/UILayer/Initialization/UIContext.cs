using UnityEngine;

namespace Asteroids.UILayer.Initialization
{
    public struct UIContext : IUIContext
    {
        public Transform Parent { get; set; }
    }
}