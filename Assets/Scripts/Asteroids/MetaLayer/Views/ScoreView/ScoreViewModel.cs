using Asteroids.MetaLayer.MVVM;

namespace Asteroids.MetaLayer.Views.ScoreView
{
    public class ScoreViewModel : UIViewModel<ScoreModel>
    {
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

        protected override void OnActivated()
        {
            _previousScore = uint.MaxValue;
        }
    }
}