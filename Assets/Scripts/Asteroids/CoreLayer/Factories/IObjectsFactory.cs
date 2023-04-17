using System;

namespace Asteroids.CoreLayer.Factories
{
    public interface IObjectsFactory<in TObject>
    {
        void Get<T>(string id, Action<T> callback, params object[] args);
        void Release(TObject obj, bool dispose);
    }
}