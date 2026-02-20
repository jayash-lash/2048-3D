using ScriptableObjects;
using Services.Board.Abstraction;
using Services.Merge.Abstraction;
using Services.Pool.Abstractions.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace Cube
{
    [RequireComponent(typeof(CubeAnimator))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class CubeBehaviour : PoolBehaviour
    {
        public int Value { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public CubeAnimator Animator { get; private set; }
        public bool IsMerging { get; set; }
        public Transform CachedTransform { get; private set; }

        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private TextMeshPro[] _faceLabels;

        private float _currentScale = 1f;
        
        private IBoardService _boardService;
        private ICubeMergeService _cubeMergeService;
        private CubeConfig _cubeConfig;

        [Inject]
        private void Construct(IBoardService boardService, ICubeMergeService cubeMergeService, CubeConfig cubeConfig)
        {
            _boardService = boardService;
            _cubeMergeService = cubeMergeService;
            _cubeConfig = cubeConfig;
        }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<CubeAnimator>();
            CachedTransform = transform;
        }

        public override void OnReuseObject(PoolObject poolObject)
        {
            IsMerging = false;
            _boardService.RegisterCube(this);
        }

        public override void OnDisposeObject(PoolObject poolObject)
        {
            Animator.ResetScale();
            _boardService.UnregisterCube(this);
        }

        
        public void SetValue(int value)
        {
            Value = value;

            // 2→scale 1.0, 4→1.25, 8→1.5, 16→1.75 etc.
            var power = (int)Mathf.Log(value, 2) - 1;
            _currentScale = 1f + power * 0.25f;

            Animator.PlaySpawn(_currentScale);
            
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            var visualData = _cubeConfig.GetVisualData(Value);
            _meshRenderer.material = visualData.Material;

            var valueText = Value.ToString();
            foreach (var label in _faceLabels)
            {
                if (label != null)
                    label.text = valueText;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (IsMerging) return;
            if (!collision.gameObject.TryGetComponent<CubeBehaviour>(out var other)) return;
            if (other.IsMerging) return;

            var impulse = collision.impulse.magnitude;
            _cubeMergeService.TryMerge(this, other, impulse);
        }
    }
}