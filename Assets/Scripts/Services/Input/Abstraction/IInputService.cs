using System;
using UnityEngine;

namespace Services.Input.Abstraction
{
    public interface IInputService
    {
        event Action<Vector2> OnFingerDown;
        event Action<Vector2> OnFingerDrag;
        event Action<Vector2> OnFingerUp;

        bool IsActive { get; }

        void Enable();
        void Disable();
    }
}