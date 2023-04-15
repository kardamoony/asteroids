using Asteroids.MetaLayer.MVVM;
using TMPro;
using UnityEngine;

namespace Asteroids.MetaLayer.Views.ScoreView
{
    public class ScoreView : UIView<ScoreViewModel, ScoreModel>
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void Update()
        {
            if (ViewModel.TryUpdateScoreText(out var score))
            {
                _scoreText.text = score;
            }
        }
    }
}