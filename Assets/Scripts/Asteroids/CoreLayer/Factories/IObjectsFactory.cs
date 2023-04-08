using System;

namespace Asteroids.CoreLayer.Factories
{
    public interface IObjectsFactory<in TObject>
    {
        void Get<T>(string id, Action<T> callback);
        void Release(TObject obj);
    }
}