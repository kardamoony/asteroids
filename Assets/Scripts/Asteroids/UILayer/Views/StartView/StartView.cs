using System;
using Asteroids.UILayer.MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UILayer.Views.StartView
{
    public class StartView : UIView<StartViewModel, StartModel>
    {
        [SerializeField] private Button _startButton;

        protected override void OnShown()
        {
            _startButton.onClick.AddListener(OnStartClicked);
        }

        protected override void OnHidden()
        {
            _startButton.onClick.RemoveAllListeners();
        }

        private void OnStartClicked()
        {
            Hide();
            ViewModel.StartGameplay();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnStartClicked();
            }
        }
    }
}