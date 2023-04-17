using System;
using Asteroids.UILayer.MVVM;

namespace Asteroids.UILayer.Views.GameoverView
{
    public class GameoverViewModel : UIViewModel<GameoverModel>
    {
        public event Action<string> OnGameOver;

        public void ExitToMenu()
        {
            Model.GameplayInitStrategy.Deinitialize();
        }

        protected override void OnInitialized()
        {
            Model.AttemptsProvider.OnPlayerGameOver += HandleGameOver;
        }

        protected override void OnDeinitialized()
        {
            Model.AttemptsProvider.OnPlayerGameOver -= HandleGameOver;
        }

        private void HandleGameOver(uint playerId)
        {
            OnGameOver?.Invoke(Model.ScoreProvider.Score.ToString("G"));
        }
    }
}