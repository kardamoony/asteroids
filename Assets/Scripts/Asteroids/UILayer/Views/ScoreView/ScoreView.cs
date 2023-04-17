using Asteroids.UILayer.MVVM;
using TMPro;
using UnityEngine;

namespace Asteroids.UILayer.Views.ScoreView
{
    public class ScoreView : UIView<ScoreViewModel, ScoreModel>
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        protected override void OnInitialized()
        {
            ViewModel.OnPlayerGameOver += HandleGameOver;
        }

        protected override void OnDeinitialized()
        {
            ViewModel.OnPlayerGameOver -= HandleGameOver;
        }

        private void HandleGameOver()
        {
            Hide();
        }

        private void Update()
        {
            if (ViewModel.TryUpdateScoreText(out var score))
            {
                _scoreText.text = score;
            }
        }
    }
}