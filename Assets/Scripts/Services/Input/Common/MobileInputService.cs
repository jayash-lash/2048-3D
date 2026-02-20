using System;
using Services.Input.Abstraction;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Services.Input.Common
{
    /// <summary>
    /// Handles input via Unity's new Input System (EnhancedTouch).
    /// Works with touch on mobile and simulated touch in editor via Input Debugger.
    /// Tick() must be called every frame by InputServiceTicker.
    /// </summary>
    public class MobileInputService : IInputService
    {
        public event Action<Vector2> OnFingerDown;
        public event Action<Vector2> OnFingerDrag;
        public event Action<Vector2> OnFingerUp;

        public bool IsActive { get; private set; }

        private bool _isTracking;
        private Vector2 _lastPosition;

        public void Enable()
        {
            EnhancedTouchSupport.Enable();
            IsActive = true;
        }

        public void Disable()
        {
            IsActive = false;
            _isTracking = false;
            EnhancedTouchSupport.Disable();
        }

        public void Tick()
        {
            if (!IsActive) return;

            var touches = Touch.activeTouches;
            if (touches.Count == 0) return;

            var touch = touches[0];

            if (EventSystem.current != null &&
                (EventSystem.current.IsPointerOverGameObject(touch.touchId) ||
                 EventSystem.current.IsPointerOverGameObject(-1)))
                return;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _isTracking = true;
                    _lastPosition = touch.screenPosition;
                    OnFingerDown?.Invoke(touch.screenPosition);
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (_isTracking)
                    {
                        Vector2 delta = touch.screenPosition - _lastPosition;
                        OnFingerDrag?.Invoke(delta);
                        _lastPosition = touch.screenPosition;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (_isTracking)
                    {
                        _isTracking = false;
                        OnFingerUp?.Invoke(touch.screenPosition);
                    }
                    break;
            }
        }
    }
}