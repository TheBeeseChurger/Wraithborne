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

    private CharacterController controller;
    private Transform camTrans;

    private Vector3 playerVelocity;
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 currentDirection = Vector3.zero;

    private InputManager inputManager;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        camTrans = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        // Apply gravity
        if (controller.isGrounded!) playerVelocity.y += gravityValue;

        if (!CanMove)
        {
            Vector3 tDir = Vector3.zero;
            currentDirection = Vector3.SmoothDamp(currentDirection, tDir, ref currentVelocity, movementSmoothTime);
            Vector3 fMove = (currentDirection * playerSpeed) + (playerVelocity.y * Vector3.up);
            controller.Move(fMove * Time.deltaTime);
            return;
        }

        // Read input
        Vector2 input = inputManager.GetPlayerMovement();
        Vector3 targetDirection = new(input.x, 0, input.y);
        targetDirection = camTrans.forward * targetDirection.z + camTrans.right * targetDirection.x;
        targetDirection.y = 0;
        targetDirection.Normalize();

        // Smooth the horizontal movement
        currentDirection = Vector3.SmoothDamp(currentDirection, targetDirection, ref currentVelocity, movementSmoothTime);

        // Combine horizontal and vertical movement
        Vector3 finalMove = (currentDirection * playerSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }
}
