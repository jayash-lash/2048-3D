using Cube.Launcher.Abstraction;
using DG.Tweening;
using Services.Input.Abstraction;
using UnityEngine;
using Zenject;

namespace Cube.Launcher
{
    public class TrajectoryView : MonoBehaviour
    {
        [Header("Arrow")]
        [SerializeField] private SpriteRenderer _arrow;

        [Header("Settings")]
        [SerializeField] private float _startOffset = 1f;
        [SerializeField] private float _appearDuration = 0.4f;
        [SerializeField] private float _hideDuration = 0.25f;
        [SerializeField] private float _maxScaleY = 3f;

        private IInputService _inputService;
        private ICubeLauncher _cubeLauncher;
        private Transform _originTransform;
        private Sequence _sequence;
        private bool _isShowing;

        [Inject]
        private void Construct(IInputService inputService, ICubeLauncher cubeLauncher)
        {
            _inputService = inputService;
            _cubeLauncher = cubeLauncher;
        }

        private void Start()
        {
            _arrow.gameObject.SetActive(false);
            _arrow.transform.localScale = new Vector3(1f, 0f, 1f);

            _inputService.OnFingerDown += OnFingerDown;
            _inputService.OnFingerUp += OnFingerUp;
            _cubeLauncher.OnActiveCubeChanged += OnActiveCubeChanged;
        }

        private void OnDestroy()
        {
            _inputService.OnFingerDown -= OnFingerDown;
            _inputService.OnFingerUp -= OnFingerUp;
            _cubeLauncher.OnActiveCubeChanged -= OnActiveCubeChanged;
        }

        private void Update()
        {
            if (_isShowing && _originTransform != null)
                SyncPosition();
        }

        private void OnActiveCubeChanged(CubeBehaviour cube)
        {
            _originTransform = cube.CachedTransform;
        }

        private void OnFingerDown(Vector2 _)
        {
            if (_originTransform == null) return;
            Show();
        }

        private void OnFingerUp(Vector2 _)
        {
            if (_isShowing) HideAnimated();
        }

        private void Show()
        {
            _isShowing = true;
            _sequence?.Kill();

            _arrow.gameObject.SetActive(true);
            _arrow.transform.localScale = new Vector3(2f, 0f, 1f);

            SyncPosition();

            _sequence = DOTween.Sequence();
            _sequence.Append(_arrow.transform.DOScaleY(_maxScaleY, _appearDuration).SetEase(Ease.OutBack)).Join(_arrow.DOFade(1f, _appearDuration * 0.5f).SetEase(Ease.OutQuad));
        }

        private void HideAnimated()
        {
            _isShowing = false;
            _sequence?.Kill();

            _sequence = DOTween.Sequence();
            _sequence.Append(_arrow.transform.DOScaleY(0f, _hideDuration).SetEase(Ease.InBack)).Join(_arrow.DOFade(0f, _hideDuration).SetEase(Ease.InQuad)).OnComplete(() => _arrow.gameObject.SetActive(false));
        }

        private void SyncPosition()
        {
            var pos = _originTransform.position + Vector3.forward * _startOffset;
            _arrow.transform.position = pos;
            _arrow.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}