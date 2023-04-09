using System;

namespace Asteroids.SimulationLayer.Initialization
{
    public interface IInitializer<TObject, in TContext>
    {
        event Action<TObject> OnObjectInitialized;
        event Action<TObject> OnObjectDenitialized;
        
        void InitializeObject(TObject @object, TContext context);
        void DeinitializeObject(TObject @object);
    }
}