using System;
using Asteroids.UILayer.MVVM;

namespace Asteroids.UILayer.Views.ScoreView
{
    public class ScoreViewModel : UIViewModel<ScoreModel>
    {
        public event Action OnPlayerGameOver;
        private uint _previousScore;

        public bool TryUpdateScoreText(out string score)
        {
            if (Model.ScoreProvider.Score != _previousScore)
            {
                _previousScore = Model.ScoreProvider.Score;
                score = _previousScore.ToString("G");
                return true;
            }

            score = string.Empty;
            return false;
        }

        protected override void OnInitialized()
        {
            _previousScore = uint.MaxValue;
            Model.AttemptsProvider.OnPlayerGameOver += HandleGameOver;
        }

        private void HandleGameOver(uint playerId)
        {
            OnPlayerGameOver?.Invoke();
        }
    }
}