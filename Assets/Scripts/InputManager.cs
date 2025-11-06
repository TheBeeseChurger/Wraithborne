using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get { return _instance; }
    }

    private PlayerControls _playerControls;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else
        {
            _instance = this;
        }

        _playerControls = new();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        if (_playerControls.FirstPerson.Movement.enabled) return _playerControls.FirstPerson.Movement.ReadValue<Vector2>();
        return Vector2.zero;
    }

    public Vector2 GetMouseDelta()
    {
        if (_playerControls.FirstPerson.Look.enabled) return _playerControls.FirstPerson.Look.ReadValue<Vector2>();
        return Vector2.zero;
    }

    public bool GetMouseDeltaEnabled()
    {
        return _playerControls.FirstPerson.Look.enabled;
    }

    public bool EnableMouseInput(bool enable)
    {
        if (enable)
        {
            if (_playerControls.FirstPerson.Look.enabled)
            {
                return false;
            }
            _playerControls.FirstPerson.Look.Enable();
            return true;
        }
        else
        {
            if (!_playerControls.FirstPerson.Look.enabled)
            {
                return false;
            }
            _playerControls.FirstPerson.Look.Disable();
            return true;
        }
    }

    public bool InteractedThisFrame()
    {
        if (_playerControls.FirstPerson.Interact.enabled) return _playerControls.FirstPerson.Interact.triggered;
        return false;
    }
}
