using Cube;
using DG.Tweening;
using ScriptableObjects;
using Services.Board.Abstraction;
using Services.Factory.Abstraction;
using Services.Merge.Abstraction;
using Services.Score.Abstraction;
using Services.Score.Common;
using Services.VFX.Abstraction;
using UnityEngine;

namespace Services.Merge.Common
{
    public class CubeMergeService : ICubeMergeService
    {
        private readonly BoardConfig _boardConfig;
        private readonly IScoreService _scoreService;
        private readonly IVFXService _vfxService;
        private readonly ICubeFactory _cubeFactory;
        private readonly IScorePopupService _scorePopupService;
        private readonly IBoardService _boardService;

        public CubeMergeService(BoardConfig boardConfig, IScoreService scoreService, IVFXService vfxService, IScorePopupService scorePopupService, ICubeFactory cubeFactory, IBoardService boardService)
        {
            _boardConfig = boardConfig;
            _scoreService = scoreService;
            _vfxService = vfxService;
            _cubeFactory = cubeFactory;
            _scorePopupService = scorePopupService;
            _boardService = boardService;
        }

        public bool TryMerge(CubeBehaviour cubeA, CubeBehaviour cubeB, float impulse)
        {
            if (cubeA.IsMerging || cubeB.IsMerging) return false;

            if (!CanMerge(cubeA, cubeB, impulse))
                return false;

            cubeA.IsMerging = true;
            cubeB.IsMerging = true;

            ExecuteMerge(cubeA, cubeB);
            return true;
        }

        private bool CanMerge(CubeBehaviour cubeA, CubeBehaviour cubeB, float impulse)
        {
            if (cubeA.Value != cubeB.Value)
                return false;

            if (impulse < _boardConfig.MinMergeImpulse)
                return false;

            return true;
        }

        private void ExecuteMerge(CubeBehaviour cubeA, CubeBehaviour cubeB)
        {
            var mergedValue = cubeA.Value + cubeB.Value;
            var mergePosition = (cubeA.CachedTransform.position + cubeB.CachedTransform.position) / 2f;
            var incomingVelocity = cubeA.Rigidbody.linearVelocity;

            cubeA.ReturnToPool();
            cubeB.ReturnToPool();

            var mergedCube = _cubeFactory.SpawnMerged(mergePosition, mergedValue);
            mergedCube.Rigidbody.isKinematic = true;

            if (mergePosition.z > _boardConfig.DeathLineZ)
                _boardService.RegisterMergedBehindLine(mergedCube);

            DOVirtual.DelayedCall(0.15f, () =>
            {
                if (mergedCube == null || mergedCube.PoolObject.IsInsidePool) return;

                mergedCube.Rigidbody.isKinematic = false;

                Vector3 bounceDirection = new Vector3(0, _boardConfig.MergeJumpHeight, incomingVelocity.normalized.z);

                mergedCube.Rigidbody.AddForce(bounceDirection * _boardConfig.MergeJumpForce, ForceMode.Impulse);
            });

            _scorePopupService.Show(mergedValue, mergePosition);
            _scoreService.AddScore(mergedValue);
            _vfxService.PlayMergeVFX(mergePosition);
        }
    }
}