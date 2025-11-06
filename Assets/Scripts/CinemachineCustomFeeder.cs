using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Windows;

[ExecuteAlways]
[SaveDuringPlay]
[AddComponentMenu("Cinemachine/Custom/CinemachineCustomFeeder")]
[RequireComponent(typeof(CinemachinePanTilt))]
public class CinemachineCustomFeeder : CinemachineExtension
{
    [Header("Input Settings")]
    public float mouseSens = 5f;
    public float accelTime = 0.2f;
    public float deaccelTime = 0.2f;

    private Vector2 currentInput;
    private Vector2 velocity;

    private CinemachinePanTilt cmPanTilt;

    protected override void Awake()
    {
        cmPanTilt = GetComponent<CinemachinePanTilt>();
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage != CinemachineCore.Stage.Body || !Application.isPlaying) return;
        if (!InputManager.Instance.GetMouseDeltaEnabled()) return;

        Vector2 rawInput = InputManager.Instance.GetMouseDelta();
        float smoothTime = rawInput.magnitude > 0.01f ? accelTime : deaccelTime;

        currentInput = Vector2.SmoothDamp(currentInput, rawInput, ref velocity, smoothTime);

        float newPan = cmPanTilt.PanAxis.Value + (currentInput.x * mouseSens);
        float newTilt = cmPanTilt.TiltAxis.Value + (-currentInput.y * mouseSens);

        newPan = NormalizePanAngle(newPan, cmPanTilt.PanAxis.Range.x, cmPanTilt.PanAxis.Range.y);
        newTilt = Mathf.Clamp(newTilt, cmPanTilt.TiltAxis.Range.x, cmPanTilt.TiltAxis.Range.y);

        cmPanTilt.PanAxis.Value = newPan;
        cmPanTilt.TiltAxis.Value = newTilt;
    }

    private float NormalizePanAngle(float angle, float min, float max)
    {
        float range = max - min;
        while (angle > max) angle -= range;
        while (angle < min) angle += range;
        return angle;
    }
}
