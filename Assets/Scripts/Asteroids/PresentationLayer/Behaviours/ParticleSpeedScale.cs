using UnityEngine;

namespace Asteroids.PresentationLayer.Behaviours
{
    public class ParticleSpeedScale : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private ParticleSystem[] _particles;

        [Space] 
        [SerializeField] private float _scaleIncreaseSpeed = 0.1f;
        [SerializeField] private float _scaleDecreaseSpeed = 0.02f;

        private ParticleSystemRenderer[] _renderers;
        private float[] _scales;
        private float _speedSquared;

        private void Update()
        {
            var speedSquared = _rb.velocity.sqrMagnitude;

            if (speedSquared < float.Epsilon)
            {
                for (var i = 0; i < _scales.Length; i++)
                {
                    _renderers[i].lengthScale = 0f;
                }

                _speedSquared = speedSquared;
                
                return;
            }

            if (speedSquared < _speedSquared)
            {
                for (var i = 0; i < _scales.Length; i++)
                {
                    _renderers[i].lengthScale = Mathf.Max(0f, _renderers[i].lengthScale - _scaleDecreaseSpeed * Time.deltaTime);
                }

                _speedSquared = speedSquared;
                
                return;
            }
            
            for (var i = 0; i < _scales.Length; i++)
            {
                _renderers[i].lengthScale = Mathf.Min(_scales[i], _renderers[i].lengthScale + _scaleIncreaseSpeed * Time.deltaTime);
            }

            _speedSquared = speedSquared;
        }

        private void Awake()
        {
            _renderers = new ParticleSystemRenderer[_particles.Length];
            _scales = new float[_particles.Length];

            for (var i = 0; i < _particles.Length; i++)
            {
                _renderers[i] = _particles[i].GetComponent<ParticleSystemRenderer>();
                _scales[i] = _renderers[i].lengthScale;
                _renderers[i].lengthScale = 0f;
            }
        }
    }
}