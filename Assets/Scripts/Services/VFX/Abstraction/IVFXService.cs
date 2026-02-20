using UnityEngine;

namespace Services.VFX.Abstraction
{
    public interface IVFXService
    {
        void PlayMergeVFX(Vector3 position);
        void PlaySpawnVFX(Vector3 position);
    }
}