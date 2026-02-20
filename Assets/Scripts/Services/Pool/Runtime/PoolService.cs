using System;
using System.Collections.Generic;
using Services.Pool.Abstractions;
using Services.Pool.Abstractions.Common;
using Services.Pool.Runtime.Common;
using UnityEngine;
using Zenject;

namespace Services.Pool.Runtime
{
    public sealed class PoolService : IPoolService
    {
        private readonly Dictionary<int, PoolQueue> _poolMap = new();
        private readonly DiContainer _container;
        
        public PoolService(DiContainer container)
        {
            _container = container;
        }
        
        public bool IsPoolCreated(GameObject prefab)
        {
            return _poolMap.ContainsKey(prefab.GetInstanceID());
        }
        
        /// <summary>
        /// Create pool using certain prefab.
        /// </summary>
        /// <param name="prefab">Pool prefab.</param>
        /// <param name="startSize">Initial pool size.</param>
        /// <param name="increaseSizeBy">Increase size if pool was ended on amount.</param>
        /// <exception cref="Exception">thrown exception if pool already exist.</exception>
        public void CreatePool(GameObject prefab, int startSize, int increaseSizeBy = 0)
        {
            if (prefab == null)
                throw new ArgumentException($"Parameter {nameof(prefab)} cannot be null.");
            if (startSize <= 0)
                throw new ArgumentException($"Parameter {nameof(startSize)} should be greater then zero.");
            if (increaseSizeBy < 0)
                throw new ArgumentException($"Parameter {nameof(increaseSizeBy)} cannot be less then zero.");
            
            var poolKey = prefab.GetInstanceID();
            if (_poolMap.ContainsKey(poolKey))
                throw new Exception($"[PoolManager] Pool {prefab.name} already exist.");
            
            var pool = new PoolQueue(prefab, startSize, increaseSizeBy, _container);
            _poolMap.Add(poolKey, pool);
        }
        
        /// <summary>
        /// Grabbing object from pool.
        /// </summary>
        /// <param name="prefab">Prefab was being used for creating pool.</param>
        /// <returns>Return container for pool object</returns>
        /// <exception cref="Exception">thrown exception if pool not exist.</exception>
        public PoolObject InstantiateFromPool(GameObject prefab)
        {
            if (prefab == null)
                throw new ArgumentException($"Parameter {nameof(prefab)} cannot be null.");
            
            var poolKey = prefab.GetInstanceID();
            if (!_poolMap.ContainsKey(poolKey))
                throw new Exception($"[PoolManager] Pool {prefab.name} not found.");
            
            return _poolMap[poolKey].GetPoolObject();
        }
        
        public void DisposePool(GameObject prefab)
        {
            var poolKey = prefab.GetInstanceID();
            if (!_poolMap.ContainsKey(poolKey))
                throw new Exception($"[PoolManager] Pool {prefab.name} not found.");
            
            _poolMap[poolKey].DisposePool();
            _poolMap.Remove(poolKey);
        }
    }
}
