using System;
using Cube.Launcher.Abstraction;
using ScriptableObjects;
using UnityEngine;

namespace Cube.Launcher
{
    public class CubeLauncher : ICubeLauncher
    {
        public event Action<CubeBehaviour> OnActiveCubeChanged;
        
        private readonly BoardConfig _boardConfig;

        private CubeBehaviour _activeCube;
        private float _currentOffsetX;

        public CubeLauncher(BoardConfig boardConfig)
        {
            _boardConfig = boardConfig;
        }

        public void SetActiveCube(CubeBehaviour cube)
        {
            _activeCube = cube;
            _currentOffsetX = _activeCube.Rigidbody.position.x;
            _activeCube.Rigidbody.isKinematic = true;
            _activeCube.Rigidbody.constraints = RigidbodyConstraints.None;
            OnActiveCubeChanged?.Invoke(cube);
        }
        
        public void MoveCubeHorizontal(float deltaX)
        {
            if (_activeCube == null)
                return;

            float worldDelta = deltaX / Screen.width * _boardConfig.BoardWidth;

            _currentOffsetX = Mathf.Clamp(_currentOffsetX + worldDelta, -_boardConfig.MaxDragOffsetX, _boardConfig.MaxDragOffsetX);

            var position = _activeCube.CachedTransform.position;
            position.x = _currentOffsetX;
            _activeCube.CachedTransform.position = position;
        }

        public void Launch()
        {
            if (_activeCube == null) return;

            _activeCube.Rigidbody.isKinematic = false;
            
            _activeCube.Animator.PlayLaunch();
            _activeCube.Rigidbody.AddForce(Vector3.forward * _boardConfig.LaunchForce, ForceMode.Impulse);
        }
    }
}