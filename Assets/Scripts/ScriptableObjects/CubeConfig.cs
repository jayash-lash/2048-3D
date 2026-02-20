using System;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public struct SpawnWeightData
    {
        public int Value;
        public int Weight;
    }

    [Serializable]
    public struct CubeVisualData
    {
        [SerializeField] private int _value;
        [SerializeField] private Color _color;
        [SerializeField] private Material _material;

        public int Value => _value;
        public Material Material => _material;
    }
    
    [CreateAssetMenu(fileName = "CubeConfig", menuName = "2048/CubeConfig")]
    public sealed class CubeConfig : ScriptableObject
    {
        public GameObject CubePrefab => _cubePrefab;
        public int PoolStartSize => _poolStartSize;
        public int PoolIncreaseSizeBy => _poolIncreaseSizeBy;
        
        [Header("Spawn Weights")]
        [SerializeField] private SpawnWeightData[] _spawnWeights = 
        {
            new() { Value = 2, Weight = 75 },
            new() { Value = 4, Weight = 25 }
        };
        [Header("Pool Settings")]
        [SerializeField] private GameObject _cubePrefab;
        [SerializeField] private int _poolStartSize = 20;
        [SerializeField] private int _poolIncreaseSizeBy = 5;
        [Header("Visual Data per Power-of-2")]
        [SerializeField] private CubeVisualData[] _visualDataList;

        

        public int GetRandomSpawnValue()
        {
            var total = 0;
            foreach (var entry in _spawnWeights)
                total += entry.Weight;

            var roll = UnityEngine.Random.Range(0, total);
            var cumulative = 0;

            foreach (var entry in _spawnWeights)
            {
                cumulative += entry.Weight;
                if (roll < cumulative)
                    return entry.Value;
            }

            return _spawnWeights[0].Value;
        }

        public CubeVisualData GetVisualData(int value)
        {
            foreach (var data in _visualDataList)
            {
                if (data.Value == value)
                    return data;
            }

            return _visualDataList.Length > 0 ? _visualDataList[_visualDataList.Length - 1] : default;
        }
    }
}