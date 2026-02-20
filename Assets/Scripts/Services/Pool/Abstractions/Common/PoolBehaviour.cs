using System;
using UnityEngine;

namespace Services.Pool.Abstractions.Common
{
    public abstract class PoolBehaviour : MonoBehaviour, IPoolObject
    {
        public virtual Type ComponentType => GetType();
        public PoolObject PoolObject { get; private set; }
        
        public void ReturnToPool() => PoolObject?.ReturnToPool();
        
        public virtual void Initialize(PoolObject poolObject) => PoolObject = poolObject;
        
        public virtual void OnReuseObject(PoolObject poolObject) 
        { 
            /* do nothing */ 
        }
        
        public virtual void OnDisposeObject(PoolObject poolObject) 
        { 
            /* do nothing */ 
        }
        
        public void Destroy() => PoolObject.ReturnToPool();
        
        protected virtual void OnDestroy()
        {
            OnDisposeObject(PoolObject);
        }
    }
}