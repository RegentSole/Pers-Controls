using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Единый экземпляр Input Actions
    private InputActions inputActions;
    
    // Ссылка на контроллер персонажа
    [SerializeField] private PlayerController playerController;
    
    private void Awake()
    {
        inputActions = new InputActions();
        
        // По умолчанию включаем карту игрока
        EnablePlayerMap();
    }

    // Включить управление игроком
    public void EnablePlayerMap()
    {
        inputActions.Vehicle.Disable();
        inputActions.Player.Enable();
        playerController.enabled = true;

         Debug.Log("[Input System] Player Action Map activated");
    }

    // Включить управление транспортом
    public void EnableVehicleMap()
    {
        inputActions.Player.Disable();
        inputActions.Vehicle.Enable();
        playerController.enabled = false;

        Debug.Log("[Input System] Vehicle Action Map activated");
    }

    // Для примера: вызов при взаимодействии с транспортом
    public void OnEnterVehicle()
    {
        EnableVehicleMap();
        Debug.Log("Переключение на управление транспортом");
    }

    public void OnExitVehicle()
    {
        EnablePlayerMap();
        Debug.Log("Возврат к управлению персонажем");
    }

    // Добавьте в класс InputManager
private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Vehicle"))
    {
        OnEnterVehicle();
    }
}

private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Vehicle"))
    {
        OnExitVehicle();
    }
}
}