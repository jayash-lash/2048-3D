using UnityEngine;
using Zenject;

namespace Services.Input.Common
{
    /// <summary>
    /// Bridges Unity's Update loop with MobileInputService.
    /// </summary>
    public class InputServiceTicker : MonoBehaviour
    {
        [Inject] private readonly MobileInputService _inputService;

        private void Update()
        {
            _inputService.Tick();
        }
    }
}