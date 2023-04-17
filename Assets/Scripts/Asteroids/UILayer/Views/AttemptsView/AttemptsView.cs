using System.Collections.Generic;
using System.Linq;
using Asteroids.UILayer.MVVM;
using UnityEngine;

namespace Asteroids.UILayer.Views.AttemptsView
{
    public class AttemptsView : UIView<AttemptsViewModel, AttemptsModel>
    {
        [SerializeField] private Transform _layoutTransform;
        [SerializeField] private string _attemptPrefabId;
        
        private List<Transform> _attempts;
        private int _currentAttemptsCount;

        protected override void OnInitialized()
        {
            _attempts = new List<Transform>();
            _currentAttemptsCount = ViewModel.AttemptsLeft;

            for (var i = 0; i < _currentAttemptsCount; i++)
            {
                ViewModel.InstantiateWidget(_attemptPrefabId, HandleWidgetAdded);
            }
        }

        protected override void OnDeinitialized()
        {
            var childTransforms = _layoutTransform.GetComponentsInChildren<Transform>().ToList();

            foreach (var t in childTransforms)
            {
                if (t.Equals(_layoutTransform))
                {
                    continue;
                }
                
                ViewModel.DestroyWidget(t.gameObject);
            }
            
            _attempts.Clear();
        }

        private void UpdateTries()
        {
            if (_currentAttemptsCount == ViewModel.AttemptsLeft)
            {
                return;
            }
            
            if (_attempts.Count < ViewModel.AttemptsLeft)
            {
                for (var i = _attempts.Count - ViewModel.AttemptsLeft; i > 0; i--)
                {
                    ViewModel.InstantiateWidget(_attemptPrefabId, HandleWidgetAdded);
                }
            }

            if (_currentAttemptsCount < ViewModel.AttemptsLeft)
            {
                foreach (var widget in _attempts)
                {
                    widget.gameObject.SetActive(true);
                }
            }
            else
            {
                for (var i = ViewModel.AttemptsLeft; i < _attempts.Count; i++)
                {
                    _attempts[i].gameObject.SetActive(false);
                }
            }

            _currentAttemptsCount = ViewModel.AttemptsLeft;
        }

        private void HandleWidgetAdded(Transform t)
        {
            _attempts.Add(t);
            t.SetParent(_layoutTransform);
        }

        private void Update()
        {
            if (!IsShown) return;
            UpdateTries();
        }
    }
}
