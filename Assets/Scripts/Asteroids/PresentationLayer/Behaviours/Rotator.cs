using UnityEngine;

namespace Asteroids.PresentationLayer.Behaviours
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _axis;
        [SerializeField] private float _speed;

        private Transform _transform;

        private void Update()
        {
            _transform.Rotate(_axis, _speed * Time.deltaTime);
        }

        private void Awake()
        {
            _transform = transform;
        }
    }
}