using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class CinemachineCustomFeeder : MonoBehaviour
{
    [Header("Input Settings")]
    public float mouseSens = 5f;
    public float accelTime = 0.2f;
    public float deaccelTime = 0.2f;

    private Vector2 currentInput;
    private Vector2 velocity;

    private CinemachinePanTilt cmPanTilt;

    private bool PanTilt = false;

    private void Awake()
    {
        if (TryGetComponent<CinemachinePanTilt>(out cmPanTilt)) PanTilt = true;
        else PanTilt = false;
    }

    void Update()
    {
        Vector2 input = InputManager.Instance.GetMouseDelta();

        if (PanTilt)
        {
            float smoothTime = input.magnitude > 0.01f ? accelTime : deaccelTime;
            currentInput = Vector2.SmoothDamp(currentInput, input, ref velocity, smoothTime);

            var panRange = cmPanTilt.PanAxis.Range;
            var tiltRange = cmPanTilt.TiltAxis.Range;

            float newX = cmPanTilt.PanAxis.Value + (currentInput.x * mouseSens);
            float newY = cmPanTilt.TiltAxis.Value + (-currentInput.y * mouseSens);

            if (newX > panRange.x) newX -= 360;
            else if (newX < panRange.y) newX += 360;
            newY = Mathf.Clamp(newY, tiltRange.x, tiltRange.y);

            cmPanTilt.PanAxis.Value = newX;
            cmPanTilt.TiltAxis.Value = newY;
        }
    }
}
