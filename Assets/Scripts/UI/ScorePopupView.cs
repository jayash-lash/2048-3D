using DG.Tweening;
using Services.Pool.Abstractions.Common;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScorePopupView : PoolBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreTMP;
        [SerializeField] private float _moveDuration = 0.6f;
        [SerializeField] private float _moveUpDistance = 150f;
        [SerializeField] private float _fadeDuration = 0.3f;

        private Camera _mainCamera;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _rectTransform = GetComponent<RectTransform>();
        }

        public override void OnReuseObject(PoolObject poolObject)
        {
            _scoreTMP.color = Color.white;
        }

        public void Show(int score, Vector3 worldPosition)
        {
            _scoreTMP.text = $"+{score}";
            _scoreTMP.color = Color.white;

            var screenPos = (Vector2)_mainCamera.WorldToScreenPoint(worldPosition);
            _rectTransform.position = screenPos;

            _rectTransform.DOMove(screenPos + Vector2.up * _moveUpDistance, _moveDuration).SetEase(Ease.OutCubic);

            _scoreTMP.DOFade(0f, _fadeDuration).SetDelay(_moveDuration - _fadeDuration).OnComplete(ReturnToPool);
        }
    }
}