using Services.Pool.Abstractions.Common;
using UnityEngine;

namespace Services.Pool.Abstractions.Extension
{
    public static class PoolServiceExtension
    {
        public static PoolObject InstantiateFromPool(this IPoolService poolService, GameObject prefab, Vector3 position)
        {
            var poolObject = poolService.InstantiateFromPool(prefab);
            poolObject.Transform.position = position;
            return poolObject;
        }
        
        public static PoolObject InstantiateFromPool(this IPoolService poolService, GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var poolObject = poolService.InstantiateFromPool(prefab, position);
            poolObject.Transform.rotation = rotation;
            return poolObject;
        }
        
        public static PoolObject InstantiateFromPool(this IPoolService poolService, GameObject prefab, Transform parent)
        {
            var poolObject = poolService.InstantiateFromPool(prefab);
            poolObject.Transform.parent = parent;
            return poolObject;
        }
        
        public static PoolObject InstantiateFromPool(this IPoolService poolService, GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            var poolObject = poolService.InstantiateFromPool(prefab, position, rotation);
            poolObject.Transform.parent = parent;
            return poolObject;
        }
    }
}


