using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public bool CanMove { get; set; } = true;

    [SerializeField]
    private float playerSpeed = 5.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float movementSmoothTime = 0.1f;

    private CharacterController _controller;
    private Transform _camTrans;

    private Vector3 _playerVelocity;
    private Vector3 _currentVelocity = Vector3.zero;
    private Vector3 _currentDirection = Vector3.zero;

    private InputManager _inputManager;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputManager = InputManager.Instance;
        _camTrans = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Debug.Log("Grounded: " + _controller.isGrounded);
        if (_controller.isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0;
        }

        // Apply gravity
        if (_controller.isGrounded!) _playerVelocity.y += gravityValue;

        if (!CanMove)
        {
            Vector3 tDir = Vector3.zero;
            _currentDirection = Vector3.SmoothDamp(_currentDirection, tDir, ref _currentVelocity, movementSmoothTime);
            Vector3 fMove = (_currentDirection * playerSpeed) + (_playerVelocity.y * Vector3.up);
            _controller.Move(fMove * Time.deltaTime);
            return;
        }

        // Read input
        Vector2 input = _inputManager.GetPlayerMovement();
        Vector3 targetDirection = new(input.x, 0, input.y);
        targetDirection = _camTrans.forward * targetDirection.z + _camTrans.right * targetDirection.x;
        targetDirection.y = 0;
        targetDirection.Normalize();

        // Smooth the horizontal movement
        _currentDirection = Vector3.SmoothDamp(_currentDirection, targetDirection, ref _currentVelocity, movementSmoothTime);

        // Combine horizontal and vertical movement
        Vector3 finalMove = (_currentDirection * playerSpeed) + (_playerVelocity.y * Vector3.up);
        _controller.Move(finalMove * Time.deltaTime);
    }
}
