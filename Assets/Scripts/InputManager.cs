using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get { return _instance; }
    }

    private PlayerControls playerControls;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else
        {
            _instance = this;
        }

        playerControls = new();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        if (playerControls.FirstPerson.Movement.enabled) return playerControls.FirstPerson.Movement.ReadValue<Vector2>();
        return Vector2.zero;
    }

    public Vector2 GetMouseDelta()
    {
        if (playerControls.FirstPerson.Look.enabled) return playerControls.FirstPerson.Look.ReadValue<Vector2>();
        return Vector2.zero;
    }

    public bool EnableMouseInput(bool enable)
    {
        if (enable)
        {
            if (playerControls.FirstPerson.Look.enabled)
            {
                return false;
            }
            playerControls.FirstPerson.Look.Enable();
            return true;
        }
        else
        {
            if (!playerControls.FirstPerson.Look.enabled)
            {
                return false;
            }
            playerControls.FirstPerson.Look.Disable();
            return true;
        }
    }

    public bool InteractedThisFrame()
    {
        if (playerControls.FirstPerson.Interact.enabled) return playerControls.FirstPerson.Interact.triggered;
        return false;
    }
}
