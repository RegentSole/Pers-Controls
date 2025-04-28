using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    // События для передачи данных
    public event System.Action<Vector2> OnMove;
    public event System.Action OnJump;
    public event System.Action<Vector2> OnLook;
    public event System.Action<float> OnZoom;

    private InputActions _inputActions;

    private void Awake()
    {
        _inputActions = new InputActions();
        
        // Player
        _inputActions.Player.Move.performed += ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
        _inputActions.Player.Jump.performed += ctx => OnJump?.Invoke();
        
        // Camera
        _inputActions.Camera.Look.performed += ctx => OnLook?.Invoke(ctx.ReadValue<Vector2>());
        _inputActions.Camera.Zoom.performed += ctx => OnZoom?.Invoke(ctx.ReadValue<float>());
    }

    private void OnEnable() => _inputActions.Enable();
    private void OnDisable() => _inputActions.Disable();
}