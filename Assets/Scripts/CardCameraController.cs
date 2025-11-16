using UnityEngine;

public class CardCameraController : MonoBehaviour
{
    [SerializeField]
    private float camSpeed = 5.0f;
    [SerializeField]
    private float dampeningTime = 0.1f;
    [SerializeField]
    private Transform camTrans;
    [SerializeField]
    private float maxOffset = 1f;

    private Vector3 _currVelocity = Vector3.zero;
    private Vector3 _currDirection = Vector3.zero;
    private Vector3 _currPosition = Vector3.zero;

    private InputManager _inputManager;

    private const float MOUSE_MULT = 0.3f;

    private void Awake()
    {
        _inputManager = InputManager.Instance;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        if (camTrans == null || _inputManager == null) Destroy(this);
    }

    void Update()
    {
        Vector2 input = _inputManager.GetPlayerMovement();
        Vector3 targetDirection = new(input.x, 0f, input.y);

        _currDirection = Vector3.SmoothDamp(_currDirection, targetDirection, ref _currVelocity, dampeningTime);

        Vector3 final = _currDirection * camSpeed;
        final *= Time.deltaTime;

        _currPosition += final;

        Vector2 posInput = _inputManager.GetMousePositionCentered();
        Vector2 posNormalized = new(posInput.x / (Screen.width * 0.5f), posInput.y / (Screen.height * 0.5f));
        Vector3 posOffset = new(posNormalized.x, 0f, posNormalized.y);
        posOffset *= maxOffset;

        Vector3 offsetFinal = Vector3.Lerp(_currPosition, posOffset, MOUSE_MULT);
        offsetFinal.y = 10f;
        camTrans.position = offsetFinal;
    }
}
