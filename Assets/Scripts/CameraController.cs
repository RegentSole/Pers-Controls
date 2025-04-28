using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 20f;

    private InputActions inputActions;
    private Vector2 lookInput;
    private float zoomInput;
    private Vector3 currentRotation;
    private float currentZoom = 10f;

    private void Awake()
    {
        inputActions = new InputActions();
        currentRotation = transform.eulerAngles;

        inputActions.Camera.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Camera.Look.canceled += ctx => lookInput = Vector2.zero;
        inputActions.Camera.Zoom.performed += ctx => zoomInput = ctx.ReadValue<float>();
    }

    private void OnEnable() => inputActions.Camera.Enable();
    private void OnDisable() => inputActions.Camera.Disable();

    private void Update()
    {
        // Поворот камеры
        currentRotation.x -= lookInput.y * rotationSpeed * Time.deltaTime;
        currentRotation.y += lookInput.x * rotationSpeed * Time.deltaTime;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -90f, 90f);
        transform.rotation = Quaternion.Euler(currentRotation);

        // Зум
        currentZoom -= zoomInput * zoomSpeed * Time.deltaTime;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        transform.localPosition = new Vector3(0, 0, -currentZoom);
    }
}