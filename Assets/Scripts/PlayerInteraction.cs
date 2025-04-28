using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInteraction : MonoBehaviour
{
    private InputActions inputActions;
    private bool isInVehicle = false;

    private void Awake()
    {
        inputActions = new InputActions();
        SwitchToPlayerControls();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vehicle") && !isInVehicle)
        {
            SwitchToVehicleControls();
            isInVehicle = true;
            Debug.Log("Entered vehicle");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Vehicle") && isInVehicle)
        {
            SwitchToPlayerControls();
            isInVehicle = false;
            Debug.Log("Exited vehicle");
        }
    }

    private void SwitchToPlayerControls()
    {
        inputActions.Vehicle.Disable();
        inputActions.Player.Enable();
        GetComponent<PlayerController>().enabled = true;
    }

    private void SwitchToVehicleControls()
    {
        inputActions.Player.Disable();
        inputActions.Vehicle.Enable();
        GetComponent<PlayerController>().enabled = false;
    }

    private void OnDestroy()
    {
        inputActions.Dispose();
    }
}