using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BoardConfig", menuName = "2048/BoardConfig")]
    public sealed class BoardConfig : ScriptableObject
    {
        public float BoardWidth => _boardWidth;
        public float SpawnPositionY => _spawnPositionY;
        public float SpawnPositionZ => _spawnPositionZ;
        public float LaunchForce => _launchForce;
        public float MinMergeImpulse => _minMergeImpulse;
        public float CubeStopCheckDelay => _cubeStopCheckDelay;
        public int MaxCubesOnBoard => _maxCubesOnBoard;
        public float DragSensitivity => _dragSensitivity;
        public float MaxDragOffsetX => _maxDragOffsetX;
        public float MergeJumpForce => _mergeJumpForce;
        public float MergeJumpHeight => _mergeJumpHeight;
        public float DeathLineZ => _deathLineZ;
       

        [Header("Board Settings")]
        [SerializeField] private float _boardWidth = 4f;

        [Header("Launch Settings")]
        [SerializeField] private float _launchForce = 15f;

        [Header("Cube Settings")]
        [SerializeField] private float _cubeStopCheckDelay = 0.5f;
        [SerializeField] private int _maxCubesOnBoard = 64;

        [Header("Spawn Settings")]
        [SerializeField] private float _spawnPositionY = 0.5f;
        [SerializeField] private float _spawnPositionZ = 0f;
        [SerializeField] private float _dragSensitivity = 1f;
        [SerializeField] private float _maxDragOffsetX = 1.5f;

        [Header("Merge Settings")]
        [SerializeField] private float _mergeJumpForce = 7f;
        [SerializeField] private float _mergeJumpHeight = 1f;
        [SerializeField] private float _minMergeImpulse = 2f;

        [Header("Death Line")]
        [SerializeField] private float _deathLineZ = 0f;
    }
}