namespace Asteroids.CoreLayer.Interfaces
{
    public interface IPool<T>
    {
        T Get();
        void Return(T obj);
    }
}
