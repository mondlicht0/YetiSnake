using UnityEngine;
using UnityEngine.InputSystem;
using YetiSnake.Utilities;

namespace YetiSnake.Input
{
    [DefaultExecutionOrder(-1)]
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler Instance { get; private set; }

        public delegate void StartTouch(Vector2 position, float time);
        public delegate void EndTouch(Vector2 position, float time);

        public event StartTouch OnStartTouch;
        public event EndTouch OnEndTouch;

        private Controls _controls;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            else
            {
                Destroy(Instance);
                Instance = this;
            }

            _controls = new Controls();
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Start()
        {
            _controls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
            _controls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        }

        private void StartTouchPrimary(InputAction.CallbackContext context)
        {
            if (OnStartTouch != null)
            {
                OnStartTouch(Utils.ScreenToWorld(Camera.main, _controls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
            }
        }

        private void EndTouchPrimary(InputAction.CallbackContext context)
        {
            if (OnEndTouch != null)
            {
                OnEndTouch(Utils.ScreenToWorld(Camera.main, _controls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
            }
        }

        public Vector2 PrimaryPosition()
        {
            return Utils.ScreenToWorld(Camera.main, _controls.Touch.PrimaryPosition.ReadValue<Vector2>());
        }
    }
}
