using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float sensitivityX = 100f;
    public float sensitivityY = 100f;
    public float zoomSensitivity = 20f;
    public float minDistance = 5f;
    public float maxDistance = 25f;
    public float smoothTime = 0.3f;

    private Vector2 _lookInput;
    private float _zoomInput;
    private Vector3 _cameraVelocity;
    private float _distanceFromTarget;

    private PlayerControls _controls;

    void Awake()
    {
        _controls = new PlayerControls();

        // Подписываемся на события ввода
        _controls.Camera.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        _controls.Camera.Zoom.performed += ctx => _zoomInput = ctx.ReadValue<float>();
    }

    void OnEnable()
    {
        _controls.Camera.Enable();
    }

    void OnDisable()
    {
        _controls.Camera.Disable();
    }

    void LateUpdate()
    {
        RotateCamera();
        ZoomCamera();
    }

    void RotateCamera()
    {
        if (_lookInput != Vector2.zero)
        {
            float rotationX = _lookInput.x * sensitivityX * Time.deltaTime;
            float rotationY = _lookInput.y * sensitivityY * Time.deltaTime;

            transform.RotateAround(target.position, Vector3.up, rotationX);
            transform.RotateAround(target.position, transform.right, -rotationY);
        }
    }

    void ZoomCamera()
    {
        _distanceFromTarget -= _zoomInput * zoomSensitivity * Time.deltaTime;
        _distanceFromTarget = Mathf.Clamp(_distanceFromTarget, minDistance, maxDistance);

        Vector3 newPosition = target.position - transform.forward * _distanceFromTarget;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _cameraVelocity, smoothTime);
    }
}