using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "VFXConfig", menuName = "2048/VFXConfig")]
    public sealed class VFXConfig : ScriptableObject
    {
        public GameObject MergeVFXPrefab => _mergeVFXPrefab;
        public int MergeVFXPoolSize => _mergeVFXPoolSize;
        public float MergeVFXDuration => _mergeVFXDuration;
        public GameObject SpawnVFXPrefab => _spawnVFXPrefab;
        public int SpawnVFXPoolSize => _spawnVFXPoolSize;
        public GameObject ScorePopupPrefab => _scorePopupPrefab;
        public int ScorePopupPoolSize => _scorePopupPoolSize;

        [Header("Merge VFX")]
        [SerializeField] private GameObject _mergeVFXPrefab;
        [SerializeField] private int _mergeVFXPoolSize = 10;
        [SerializeField] private float _mergeVFXDuration = 1f;

        [Header("Spawn VFX")]
        [SerializeField] private GameObject _spawnVFXPrefab;
        [SerializeField] private int _spawnVFXPoolSize = 5;
        [Header("Point Message")]
        [SerializeField] private GameObject _scorePopupPrefab;
        [SerializeField] private int _scorePopupPoolSize = 5;
       
        
        
    }
}