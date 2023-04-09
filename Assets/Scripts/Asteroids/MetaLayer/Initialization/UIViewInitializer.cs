using System;
using Asteroids.MetaLayer.MVVM;
using Asteroids.SimulationLayer.Initialization;

namespace Asteroids.MetaLayer.Initialization
{
    public class UIViewInitializer : IInitializer<UIView, UIViewModel>
    {
        public event Action<UIView> OnObjectInitialized;
        public event Action<UIView> OnObjectDenitialized;
        
        public void InitializeObject(UIView @object, UIViewModel context)
        {
            
        }

        public void DeinitializeObject(UIView @object)
        {
            
        }
    }
}