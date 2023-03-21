using UnityEngine;

namespace Asteroids.CoreLayer.Input
{
    public class RawInputProvider : MonoBehaviour, IInputProvider
    {
        private const string VerticalAxisName = "Vertical";
        private const string HorizontalAxisName = "Horizontal";
        
        public float VerticalAxis { get; private set; }
        public float HorizontalAxis { get; private set; }
        public bool Fire { get; private set; }
        public bool Action { get; private set; }

        private void Update()
        {
            //TODO: separate input mapping
            VerticalAxis = UnityEngine.Input.GetAxisRaw(VerticalAxisName);
            HorizontalAxis = UnityEngine.Input.GetAxisRaw(HorizontalAxisName);
            Fire = UnityEngine.Input.GetKey(KeyCode.Space);
            Action = UnityEngine.Input.GetKey(KeyCode.LeftShift);
        }
    }
}
