using System;
using System.Collections.Generic;
using Cube;
using ScriptableObjects;
using Services.Board.Abstraction;

namespace Services.Board.Common
{
    public class BoardService : IBoardService
    {
        private readonly BoardConfig _boardConfig;
        private readonly HashSet<CubeBehaviour> _activeCubes = new();
        private readonly HashSet<CubeBehaviour> _crossedCubes = new();

        public bool IsFull => _activeCubes.Count >= _boardConfig.MaxCubesOnBoard;

        public event Action OnGameOver;

        public BoardService(BoardConfig boardConfig)
        {
            _boardConfig = boardConfig;
        }

        public void RegisterCube(CubeBehaviour cube)
        {
            _activeCubes.Add(cube);
        }

        public void UnregisterCube(CubeBehaviour cube)
        {
            _activeCubes.Remove(cube);
            _crossedCubes.Remove(cube);
        }

        public void RegisterDeathLineCross(CubeBehaviour cube)
        {
            if (_crossedCubes.Contains(cube))
            {
                OnGameOver?.Invoke();
                return;
            }

            _crossedCubes.Add(cube);
        }

        public void RegisterMergedBehindLine(CubeBehaviour cube)
        {
            _crossedCubes.Add(cube);
        }

        public bool IsGameOver()
        {
            return IsFull;
        }

        public void Reset()
        {
            var cubesToReturn = new List<CubeBehaviour>(_activeCubes);
    
            foreach (var cube in cubesToReturn)
            {
                if (!cube.PoolObject.IsInsidePool)
                    cube.ReturnToPool();
            }

            _activeCubes.Clear();
            _crossedCubes.Clear();
        }
    }
}