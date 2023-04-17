using Asteroids.UILayer.MVVM;

namespace Asteroids.UILayer.Views.StartView
{
    public class StartViewModel : UIViewModel<StartModel>
    {
        public void StartGameplay()
        {
            Model.Strategy.Initialize();
        }
    }
}