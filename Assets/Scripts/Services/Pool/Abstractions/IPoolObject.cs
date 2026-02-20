using Services.Pool.Abstractions.Common;

namespace Services.Pool.Abstractions
{
    public interface IPoolObject
    {
        /// <summary>
        /// Called after object created and added into pool.
        /// </summary>
        /// <param name="poolObject"></param>
        void Initialize(PoolObject poolObject);

        /// <summary>
        /// Called before object grabbing object from pool.
        /// </summary>
        /// <param name="poolObject"></param>
        void OnReuseObject(PoolObject poolObject);

        /// <summary>
        /// Called after object returned into pool.
        /// </summary>
        /// <param name="poolObject"></param>
        void OnDisposeObject(PoolObject poolObject);
    }
}
