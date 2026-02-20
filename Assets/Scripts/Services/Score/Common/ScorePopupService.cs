using ScriptableObjects;
using Services.Pool.Abstractions;
using Services.Score.Abstraction;
using UI;
using UnityEngine;

namespace Services.Score.Common
{
    public class ScorePopupService : IScorePopupService
    {
        private readonly IPoolService _poolService;
        private readonly VFXConfig _vfxConfig;
        private readonly Transform _canvasTransform;

        public ScorePopupService(IPoolService poolService, VFXConfig vfxConfig, Transform canvasTransform)
        {
            _poolService = poolService;
            _vfxConfig = vfxConfig;
            _canvasTransform = canvasTransform;
        }

        public void Show(int score, Vector3 worldPosition)
        {
            if (_vfxConfig.ScorePopupPrefab == null) return;

            var poolObject = _poolService.InstantiateFromPool(_vfxConfig.ScorePopupPrefab);
            poolObject.Transform.SetParent(_canvasTransform, false);

            var popup = poolObject.GameObject.GetComponent<ScorePopupView>();
            popup.Show(score, worldPosition);
        }
    }
}