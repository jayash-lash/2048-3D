using Cube;
using UnityEngine;

namespace Services.Factory.Abstraction
{
    public interface ICubeFactory
    {
        CubeBehaviour Spawn(Vector3 position, int value);
        CubeBehaviour SpawnMerged(Vector3 position, int value);
    }
}