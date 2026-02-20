using Cube;
using ScriptableObjects;
using Services.Board.Abstraction;
using Services.Factory.Abstraction;
using Services.Pool.Abstractions;
using UnityEngine;

namespace Services.Factory.Common
{
    public class CubeFactory : ICubeFactory
    {
        private readonly IPoolService _poolService;
        private readonly CubeConfig _cubeConfig;

        public CubeFactory(IPoolService poolService, CubeConfig cubeConfig)
        {
            _poolService = poolService;
            _cubeConfig = cubeConfig;
        }

        public CubeBehaviour Spawn(Vector3 position, int value)
        {
            return SpawnInternal(position, value);
        }

        public CubeBehaviour SpawnMerged(Vector3 position, int value)
        {
            return SpawnInternal(position, value);
        }

        private CubeBehaviour SpawnInternal(Vector3 position, int value)
        {
            var poolObject = _poolService.InstantiateFromPool(_cubeConfig.CubePrefab);
    
            var cube = poolObject.GameObject.GetComponent<CubeBehaviour>();
            
            cube.Rigidbody.position = position;
            cube.Rigidbody.rotation = Quaternion.identity;
            poolObject.Transform.position = position;
    
            cube.SetValue(value);
            return cube;
        }
    }
}