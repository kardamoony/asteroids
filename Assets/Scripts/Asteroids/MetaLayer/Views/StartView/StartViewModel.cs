using Asteroids.MetaLayer.MVVM;

namespace Asteroids.MetaLayer.Views.StartView
{
    public class StartViewModel : UIViewModel<StartModel>
    {
        public void StartGameplay()
        {
            Model.Strategy.Initialize();
        }
    }
}