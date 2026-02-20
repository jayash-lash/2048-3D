using System;
using UnityEngine;
using Zenject;

namespace Services.Pool.Abstractions.Common
{
    [Serializable]
    public sealed class PoolObject
    {
        public Transform Transform { get; }
        public GameObject GameObject { get; }
        public int InstanceId { get; }
        public bool IsInsidePool { get; private set; }
        internal Transform PoolParent { get; }
        
        public event Action OnDestroyed;
        public event Action OnDestroyedOneTime;
        
        private readonly IPoolObject[] _poolObjectScripts;
        private readonly DiContainer _container;
        
        public PoolObject(GameObject objectInstance, DiContainer container, Transform poolParent)
        {
            GameObject = objectInstance;
            InstanceId = objectInstance.GetInstanceID();
            Transform = objectInstance.transform;
            _container = container;
            _poolObjectScripts = objectInstance.GetComponentsInChildren<IPoolObject>(true);
            IsInsidePool = true;
            PoolParent = poolParent;
            
            _container.InjectGameObject(objectInstance);
            
            GameObject.SetActive(false);
            
            foreach (var iPoolObject in _poolObjectScripts)
                iPoolObject.Initialize(this);
        }
        
        public void Initialize()
        {
            GameObject.SetActive(true);
            IsInsidePool = false;
            
            foreach (var iPoolObject in _poolObjectScripts)
                iPoolObject.OnReuseObject(this);
        }
        
        public object Resolve(Type type) => _container.Resolve(type);
        
        public T Resolve<T>() where T : class => _container.Resolve<T>();
        
        /// <summary>
        /// Return object back into pool.
        /// </summary>
        public void ReturnToPool()
        {
            GameObject.SetActive(false);
            Transform.position = Vector3.zero;
            Transform.rotation = Quaternion.identity;
            Transform.SetParent(PoolParent);
            IsInsidePool = true;
            
            foreach (var poolObjectScript in _poolObjectScripts)
                poolObjectScript.OnDisposeObject(this);
            
            OnDestroyed?.Invoke();
            OnDestroyedOneTime?.Invoke();
            OnDestroyedOneTime = delegate { };
        }
    }
}
