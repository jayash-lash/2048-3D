using ScriptableObjects;
using Services.Pool.Abstractions;
using Services.Pool.Abstractions.Common;
using Services.VFX.Abstraction;
using UnityEngine;

namespace Services.VFX.Common
{
    public class VFXService : IVFXService
    {
        private readonly IPoolService _poolService;
        private readonly VFXConfig _vfxConfig;

        public VFXService(IPoolService poolService, VFXConfig vfxConfig)
        {
            _poolService = poolService;
            _vfxConfig = vfxConfig;
        }

        public void PlayMergeVFX(Vector3 position)
        {
            if (_vfxConfig.MergeVFXPrefab == null)
                return;

            var poolObject = _poolService.InstantiateFromPool(_vfxConfig.MergeVFXPrefab);
            poolObject.Transform.position = position;

            ReturnAfterDelay(poolObject, _vfxConfig.MergeVFXDuration);
        }

        public void PlaySpawnVFX(Vector3 position)
        {
            if (_vfxConfig.SpawnVFXPrefab == null)
                return;

            var poolObject = _poolService.InstantiateFromPool(_vfxConfig.SpawnVFXPrefab);
            poolObject.Transform.position = position;

            ReturnAfterDelay(poolObject, _vfxConfig.MergeVFXDuration);
        }

        private void ReturnAfterDelay(PoolObject poolObject, float delay)
        {
            var timer = new VFXTimer(poolObject, delay);
            timer.Start();
        }
    }
}