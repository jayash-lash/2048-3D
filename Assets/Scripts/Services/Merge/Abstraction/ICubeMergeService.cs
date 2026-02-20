using Cube;

namespace Services.Merge.Abstraction
{
    public interface ICubeMergeService
    {
        bool TryMerge(CubeBehaviour cubeA, CubeBehaviour cubeB, float impulse);
    }
}