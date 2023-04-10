using Asteroids.MetaLayer.MVVM;

namespace Asteroids.MetaLayer.Views.StartView
{
    public class StartViewModel : UIViewModel<StartModel>
    {
        private bool _gameplayInitialized;
        
        public void StartGameplay()
        {
            if (_gameplayInitialized)
            {
                return;
            }

            _gameplayInitialized = true;
            
            Model.Strategy.Initialize();
        }

        protected override void OnActivated()
        {
            _gameplayInitialized = false;
        }
    }
}