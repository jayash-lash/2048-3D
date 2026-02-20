using DG.Tweening;
using StateMachine.Abstraction;
using StateMachine.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _playButton;
        [SerializeField] private TextMeshProUGUI _hintTMP;
        [SerializeField] private CanvasGroup _panelCanvasGroup;
        private IGameStateMachine _stateMachine;
        
        [Inject]
        private void Construct(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Awake()
        {
            _panel.SetActive(false);
            _hintTMP.alpha = 0f;

            _playButton.onClick.AddListener(OnPlayClicked);
            MainMenuState.OnMainMenu += Show;
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayClicked);
            MainMenuState.OnMainMenu -= Show;
        }

        private void Show()
        {
            _panel.SetActive(true);
            _panelCanvasGroup.alpha = 1f;
            _panelCanvasGroup.interactable = true;
            _panelCanvasGroup.blocksRaycasts = true;
        }

        private void OnPlayClicked()
        {
            _panelCanvasGroup.interactable = false;
            _panelCanvasGroup.blocksRaycasts = false;

            _panelCanvasGroup.DOFade(0f, 0.4f).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    _panel.SetActive(false);
                    ShowHintThenStart();
                });
        }

        private void ShowHintThenStart()
        {
            _hintTMP.text = "Score as high \nas you can without \ncrossing the skull line!";
            _hintTMP.alpha = 0f;
            _hintTMP.gameObject.SetActive(true);

            RectTransform rect = _hintTMP.rectTransform;

            Vector2 startPos = rect.anchoredPosition;
            Vector2 endPos = startPos + Vector2.up * 250f;

            rect.anchoredPosition = startPos;

            var fadeInDuration = 0.5f;
            var moveDuration = 4f;

            Sequence seq = DOTween.Sequence();

            seq.Append(_hintTMP.DOFade(1f, fadeInDuration).SetEase(Ease.OutQuad));
            seq.Append(rect.DOAnchorPos(endPos, moveDuration).SetEase(Ease.OutCubic));
            seq.Join(_hintTMP.DOFade(0f, moveDuration).SetEase(Ease.InQuad));

            DOVirtual.DelayedCall(fadeInDuration + 1f, () =>
            {
                _stateMachine.Enter<SpawnState>();
            });

            seq.OnComplete(() =>
            {
                _hintTMP.gameObject.SetActive(false);
                rect.anchoredPosition = startPos;
            });
        }
    }
}