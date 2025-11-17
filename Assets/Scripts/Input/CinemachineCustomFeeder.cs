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

    private Vector2 _currentInput;
    private Vector2 _velocity;

    private CinemachinePanTilt _cmPanTilt;

    protected override void Awake()
    {
        _cmPanTilt = GetComponent<CinemachinePanTilt>();
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage != CinemachineCore.Stage.Body || !Application.isPlaying) return;
        if (!InputManager.Instance.GetMouseDeltaEnabled()) return;

        Vector2 rawInput = InputManager.Instance.GetMouseDelta();
        float smoothTime = rawInput.magnitude > 0.01f ? accelTime : deaccelTime;

        _currentInput = Vector2.SmoothDamp(_currentInput, rawInput, ref _velocity, smoothTime);

        float newPan = _cmPanTilt.PanAxis.Value + (_currentInput.x * mouseSens);
        float newTilt = _cmPanTilt.TiltAxis.Value + (-_currentInput.y * mouseSens);

        newPan = NormalizePanAngle(newPan, _cmPanTilt.PanAxis.Range.x, _cmPanTilt.PanAxis.Range.y);
        newTilt = Mathf.Clamp(newTilt, _cmPanTilt.TiltAxis.Range.x, _cmPanTilt.TiltAxis.Range.y);

        _cmPanTilt.PanAxis.Value = newPan;
        _cmPanTilt.TiltAxis.Value = newTilt;
    }

    private float NormalizePanAngle(float angle, float min, float max)
    {
        float range = max - min;
        while (angle > max) angle -= range;
        while (angle < min) angle += range;
        return angle;
    }
}
