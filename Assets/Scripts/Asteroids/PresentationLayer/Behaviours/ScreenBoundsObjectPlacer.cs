using System;
using Asteroids.PresentationLayer.Enums;
using Asteroids.PresentationLayer.Extensions;
using Asteroids.PresentationLayer.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.PresentationLayer.Behaviours
{
    public class ScreenBoundsObjectPlacer : ObjectPlacer
    {
        [SerializeField] private float _yZeroCoord;
        
        private Vector3 _viewCenter;
        
        private float _minX;
        private float _maxX;
        
        private float _minZ;
        private float _maxZ;

        private bool _valuesCached;
        
        public override void Place(Transform t)
        {
            if (!_valuesCached)
            {
                CacheValues();
            }

            var border = RandomHelper.GetRandomBorder();
            PlaceOnBorder(t, border);
        }

        private void PlaceOnBorder(Transform t, ScreenBorder border)
        {
            switch (border)
            {
                case ScreenBorder.Top:
                {
                    PositionOnTop(t);
                    LookAtHorizontalAxis(t);
                    break;
                }
                case ScreenBorder.Bottom:
                {
                    PositionOnBottom(t);
                    LookAtHorizontalAxis(t);
                    break;
                }
                case ScreenBorder.Left:
                {
                    PositionOnLeft(t);
                    LookAtVerticalAxis(t);
                    break;
                }
                case ScreenBorder.Right:
                {
                    PositionOnRight(t);
                    LookAtVerticalAxis(t);
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException($"[{GetType().Name}]: {border} value is out of range");
                }
            }
        }

        private void PositionOnTop(Transform t)
        {
            t.position = new Vector3(Random.Range(_minX, _maxX), _yZeroCoord, _maxZ);
        }

        private void PositionOnBottom(Transform t)
        {
            t.position = new Vector3(Random.Range(_minX, _maxX), _yZeroCoord, _minZ);
        }

        private void PositionOnLeft(Transform t)
        {
            t.position = new Vector3(_minX, _yZeroCoord, Random.Range(_minZ, _maxZ));
        }

        private void PositionOnRight(Transform t)
        {
            t.position = new Vector3(_maxX, _yZeroCoord, Random.Range(_minZ, _maxZ));
        }

        private void LookAtHorizontalAxis(Transform t)
        {
            t.LookAt(new Vector3(Random.Range(_minX, _maxX), _yZeroCoord, _viewCenter.z));
        }
        
        private void LookAtVerticalAxis(Transform t)
        {
            t.LookAt(new Vector3(_viewCenter.x, _yZeroCoord, Random.Range(_minZ, _maxZ)));
        }
        
        private void CacheValues()
        {
            var mainCamera = Camera.main;

            _viewCenter = mainCamera.GetTopDownWorldViewCenter(_yZeroCoord);
            var limits = mainCamera.GetTopDownViewLimits();

            _minX = limits.xMin;
            _maxX = limits.xMax;

            _minZ = limits.zMin;
            _maxZ = limits.zMax;

            _valuesCached = true;
        }
    }
}