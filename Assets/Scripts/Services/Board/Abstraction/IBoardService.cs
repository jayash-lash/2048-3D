using System;
using Cube;

namespace Services.Board.Abstraction
{
    public interface IBoardService
    {
        event Action OnGameOver;

        void RegisterCube(CubeBehaviour cube);
        void UnregisterCube(CubeBehaviour cube);
        void RegisterDeathLineCross(CubeBehaviour cube);
        void RegisterMergedBehindLine(CubeBehaviour cube);
        bool IsGameOver();
        void Reset();
    }
}