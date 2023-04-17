using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Asteroids.UILayer.Animations
{
    public class UIScaleAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Vector3 _scaleTo = new Vector3(0.9f, 0.9f, 0.9f);
        [SerializeField] private float _duration = 0.05f;
        [SerializeField] private Ease _ease = Ease.OutCirc;
        
        private Tweener _tweener;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _tweener.PlayForward();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _tweener.PlayBackwards();
        }
        
        private void Awake()
        {
            _tweener = transform.DOScale(_scaleTo, _duration).SetAutoKill(false).SetEase(_ease);
        }

        private void OnDestroy()
        {
            _tweener?.Kill();
        }
    }
}