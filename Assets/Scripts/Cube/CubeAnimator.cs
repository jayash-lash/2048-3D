using DG.Tweening;
using UnityEngine;

namespace Cube
{
    /// <summary>
    /// Handles all DOTween animations for a cube.
    /// </summary>
    public class CubeAnimator : MonoBehaviour
    {
        [Header("Spawn Animation")]
        [SerializeField] private float _spawnDuration = 0.3f;
        [SerializeField] private Ease _spawnEase = Ease.OutBack;
        
        [Header("Launch Animation")]
        [SerializeField] private float _squishDuration = 0.1f;
        [SerializeField] private Vector3 _squishScale = new Vector3(1.2f, 0.8f, 1.2f);

        private Transform _cachedTransform;
        private Sequence _currentSequence;
        private float _targetScale = 1f;
        private void Awake()
        {
            _cachedTransform = transform;
        }

        public void PlaySpawn(float targetScale = 1f)
        {
            _targetScale = targetScale;
            KillCurrent();
            _cachedTransform.localScale = Vector3.zero;
            _currentSequence = DOTween.Sequence().Append(_cachedTransform.DOScale(Vector3.one * targetScale, _spawnDuration).SetEase(_spawnEase));
        }
        

        public void PlayLaunch()
        {
            KillCurrent();
            _currentSequence = DOTween.Sequence().Append(_cachedTransform.DOScale(_squishScale * _targetScale, _squishDuration).SetEase(Ease.OutQuad)).Append(_cachedTransform.DOScale(Vector3.one * _targetScale, _squishDuration).SetEase(Ease.InQuad));
        }

        public void ResetScale()
        {
            KillCurrent();
            _cachedTransform.localScale = Vector3.one;
        }

        private void KillCurrent()
        {
            _currentSequence?.Kill();
            _currentSequence = null;
        }
    }
}