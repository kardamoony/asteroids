using Asteroids.UILayer.MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UILayer.Views.GameoverView
{
    public class GameoverView : UIView<GameoverViewModel, GameoverModel>
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _toMenuButton;

        protected override void OnInitialized()
        {
            ViewModel.OnGameOver += HandleOnGameOver;
            _toMenuButton.onClick.AddListener(HandleMenuButtonClicked);
        }

        protected override void OnDeinitialized()
        {
            ViewModel.OnGameOver -= HandleOnGameOver;
            _toMenuButton.onClick.RemoveAllListeners();
        }

        private void HandleOnGameOver(string score)
        {
            _scoreText.text = score;
            Show();
        }

        private void HandleMenuButtonClicked()
        {
            Hide();
            ViewModel.ExitToMenu();
        }
    }
}