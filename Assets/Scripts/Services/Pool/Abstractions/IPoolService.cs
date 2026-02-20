using Services.Pool.Abstractions.Common;
using UnityEngine;

namespace Services.Pool.Abstractions
{
    public interface IPoolService
    {
        /// <summary>
        /// Check if pool with specified gameObject exists.
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        bool IsPoolCreated(GameObject prefab);

        /// <summary>
        /// Create pool based on prefab.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="startSize"></param>
        /// <param name="increaseSizeBy"></param>
        void CreatePool(GameObject prefab, int startSize, int increaseSizeBy = 0);

        /// <summary>
        /// Instantiate object from pool.
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        PoolObject InstantiateFromPool(GameObject prefab);

        /// <summary>
        /// Clean pool of concrete object.
        /// </summary>
        /// <param name="prefab"></param>
        void DisposePool(GameObject prefab);
    }
}
