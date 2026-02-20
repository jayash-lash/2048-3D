using System.Collections.Generic;
using Services.Pool.Abstractions.Common;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Services.Pool.Runtime.Common
{
    public class PoolQueue
    {
        private readonly GameObject _prefab;
        private readonly int _increaseSizeBy;
        private readonly DiContainer _container;
        private readonly Queue<PoolObject> _pool = new();
        private readonly Transform _poolParent;
        
        public PoolQueue(GameObject prefab, int startSize, int increaseSizeBy, DiContainer container)
        {
            _prefab = prefab;
            _increaseSizeBy = increaseSizeBy;
            _container = container;
            
            var poolParentGO = new GameObject($"Pool_{prefab.name}");
            _poolParent = poolParentGO.transform;
            
            for (int i = 0; i < startSize; i++)
            {
                CreatePoolObject();
            }
        }
        
        public PoolObject GetPoolObject()
        {
            if (_pool.Count == 0)
            {
                for (int i = 0; i < _increaseSizeBy; i++)
                {
                    CreatePoolObject();
                }
                
                if (_pool.Count == 0)
                {
                    CreatePoolObject();
                }
            }
            
            var poolObject = _pool.Dequeue();
            poolObject.Initialize();
            return poolObject;
        }
        
        private void CreatePoolObject()
        {
            var instance = Object.Instantiate(_prefab, _poolParent);
            var poolObject = new PoolObject(instance, _container, _poolParent);
            
            poolObject.OnDestroyed += () => _pool.Enqueue(poolObject);
            
            _pool.Enqueue(poolObject);
        }
        
        public void DisposePool()
        {
            while (_pool.Count > 0)
            {
                var poolObject = _pool.Dequeue();
                Object.Destroy(poolObject.GameObject);
            }
            
            if (_poolParent != null)
            {
                Object.Destroy(_poolParent.gameObject);
            }
        }
    }
}
