using Asteroids.PresentationLayer.Extensions;
using UnityEngine;

namespace Asteroids.PresentationLayer.Behaviours
{
    public class RotationAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;
        [SerializeField] private float _maxAbsAngle = 2f;
        [SerializeField] private Vector3 _leftAngle = new Vector3(0f, 0f, 20f);
        [SerializeField] private Vector3 _rightAngle = new Vector3(0f, 0f, -20f);

        private Quaternion _leftRotation;
        private Quaternion _rightRotation;

        public void OnRotation(float angle)
        {
            var rotation = angle < 0 ? _leftRotation : _rightRotation;
            var lerpValue = Mathf.Abs(angle).Remap01(0f, _maxAbsAngle);

            _pivot.localRotation = Quaternion.Lerp(Quaternion.identity, rotation, lerpValue);
        }
        
        private void Awake()
        {
            _leftRotation = Quaternion.Euler(_leftAngle);
            _rightRotation = Quaternion.Euler(_rightAngle);
        }
    }
}
