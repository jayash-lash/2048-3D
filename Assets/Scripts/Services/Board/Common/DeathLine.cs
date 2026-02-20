using Cube;
using Services.Board.Abstraction;
using UnityEngine;
using Zenject;

namespace Services.Board.Common
{
    [RequireComponent(typeof(Collider))]
    public class DeathLine : MonoBehaviour
    {
        private IBoardService _boardService;

        [Inject]
        private void Construct(IBoardService boardService)
        {
            _boardService = boardService;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<CubeBehaviour>(out var cube)) return;
            if (cube.IsMerging) return;

            _boardService.RegisterDeathLineCross(cube);
        }
    }
}