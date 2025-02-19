using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 15f;
    
    private Rigidbody _rigidbody;
    private PlayerControls _controls;
    private Vector2 _moveInput;
    private bool _jumpPressed;

    void Awake()
    {
        // Получаем компонент Rigidbody
        _rigidbody = GetComponent<Rigidbody>();
        
        // Создаем экземпляр контроллера
        _controls = new PlayerControls();
        
        // Подписываемся на события ввода
        _controls.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _controls.Player.Jump.performed += ctx => _jumpPressed = true;
    }

    void OnEnable()
    {
        _controls.Enable();
    }

    void OnDisable()
    {
        _controls.Disable();
    }

    void FixedUpdate()
    {
        MoveCharacter();
        JumpCharacter();
    }

    void MoveCharacter()
    {
        if (_moveInput != Vector2.zero)
        {
            Vector3 movement = new Vector3(_moveInput.x, 0f, _moveInput.y);
            _rigidbody.AddRelativeForce(movement * moveSpeed, ForceMode.Force);
        }
    }

    void JumpCharacter()
    {
        if (_jumpPressed && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _jumpPressed = false;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void Update()
{
    if (Input.GetKeyDown(KeyCode.E))
    {
        SwitchToVehicleControl();
    }
    else if (Input.GetKeyDown(KeyCode.Q))
    {
        SwitchToPlayerControl();
    }
}

void SwitchToVehicleControl()
{
    _controls.Player.Disable();
    _controls.Vehicle.Enable();
    _currentMap = "Vehicle";
}

void SwitchToPlayerControl()
{
    _controls.Vehicle.Disable();
    _controls.Player.Enable();
    _currentMap = "Player";
}
}