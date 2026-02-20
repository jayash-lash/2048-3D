using System;

namespace Cube.Launcher.Abstraction
{
    public interface ICubeLauncher
    {
        event Action<CubeBehaviour> OnActiveCubeChanged;
        void SetActiveCube(CubeBehaviour cube);
        void MoveCubeHorizontal(float deltaX);
        void Launch();
    }
}