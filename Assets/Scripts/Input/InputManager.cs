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

    public Vector2 GetMousePosition()
    {
        if (_playerControls.FirstPerson.MousePosition.enabled) return _playerControls.FirstPerson.MousePosition.ReadValue<Vector2>();
        return Vector2.zero;
    }

    public Vector2 GetMousePositionCentered()
    {
        if (!_playerControls.FirstPerson.MousePosition.enabled) return Vector2.zero;
        Vector2 pos = _playerControls.FirstPerson.MousePosition.ReadValue<Vector2>();

        pos.x -= Screen.width * 0.5f;
        pos.y -= Screen.height * 0.5f;

        return pos;
    }

    public bool GetMousePositionEnabled()
    {
        return _playerControls.FirstPerson.MousePosition.enabled;
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
            _playerControls.FirstPerson.MousePosition.Enable();
            return true;
        }
        else
        {
            if (!_playerControls.FirstPerson.Look.enabled)
            {
                return false;
            }
            _playerControls.FirstPerson.Look.Disable();
            _playerControls.FirstPerson.MousePosition.Disable();
            return true;
        }
    }

    public bool InteractedThisFrame()
    {
        if (_playerControls.FirstPerson.Interact.enabled) return _playerControls.FirstPerson.Interact.WasPressedThisFrame();
        return false;
    }

    public bool LeftClickThisFrame()
    {
        if (_playerControls.FirstPerson.LeftClick.enabled) return _playerControls.FirstPerson.LeftClick.WasPressedThisFrame();
        return false;
    }

    public bool RightClickThisFrame()
    {
        if (_playerControls.FirstPerson.RightClick.enabled) return _playerControls.FirstPerson.RightClick.WasPressedThisFrame();
        return false;
    }

    public bool LeftHoldThisFrame()
    {
        if (_playerControls.FirstPerson.LeftClick.enabled) return _playerControls.FirstPerson.LeftClick.IsPressed();
        return false;
    }

    public bool RightHoldThisFrame()
    {
        if (_playerControls.FirstPerson.RightClick.enabled) return _playerControls.FirstPerson.RightClick.IsPressed();
        return false;
    }

    public bool LeftReleaseThisFrame()
    {
        if (_playerControls.FirstPerson.LeftClick.enabled) return _playerControls.FirstPerson.LeftClick.WasReleasedThisFrame();
        return false;
    }

    public bool RightReleaseThisFrame()
    {
        if (_playerControls.FirstPerson.RightClick.enabled) return _playerControls.FirstPerson.RightClick.WasReleasedThisFrame();
        return false;
    }
}
