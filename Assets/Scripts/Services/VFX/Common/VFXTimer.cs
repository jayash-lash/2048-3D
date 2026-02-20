using DG.Tweening;
using Services.Pool.Abstractions.Common;

namespace Services.VFX.Common
{
    /// <summary>
    /// Lightweight timer that returns a VFX pool object after a delay.
    /// Uses DOTween instead of coroutines to avoid MonoBehaviour dependency.
    /// </summary>
    public class VFXTimer
    {
        private readonly PoolObject _poolObject;
        private readonly float _delay;

        public VFXTimer(PoolObject poolObject, float delay)
        {
            _poolObject = poolObject;
            _delay = delay;
        }

        public void Start()
        {
            DOVirtual.DelayedCall(_delay, OnComplete, ignoreTimeScale: false);
        }

        private void OnComplete()
        {
            if (!_poolObject.IsInsidePool) 
                _poolObject.ReturnToPool();
        }
    }
}