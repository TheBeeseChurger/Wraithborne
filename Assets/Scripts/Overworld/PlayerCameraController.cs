using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera gameplayCamera;
    [SerializeField]
    private CinemachineCamera cutsceneCamera;

    private Transform _cutsceneTransform;
    private bool _isInitalized = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var obj = new GameObject("CutsceneFollowTransform");
        obj.transform.parent = null;
        _cutsceneTransform = obj.transform;
        cutsceneCamera.LookAt = _cutsceneTransform;

        cutsceneCamera.Priority = 5;
        gameplayCamera.Priority = 10;

        if (!_isInitalized)
        {
            gameplayCamera.enabled = false;
            cutsceneCamera.enabled = false;
        }

        if (OverworldManager.Instance != null)
        {
            OverworldManager.Instance.Register(this);

            if (OverworldManager.Instance.hasSavedPostion) Load(OverworldManager.Instance.overworldPlayerRotation);

            if (!_isInitalized)
            {
                _isInitalized = true;

                gameplayCamera.enabled = true;
                cutsceneCamera.enabled = true;
            }
        }
    }

    private void Load(Quaternion rotation)
    {
        gameplayCamera.ForceCameraPosition(gameplayCamera.transform.position, rotation);

        _isInitalized = true;

        gameplayCamera.enabled = true;
        cutsceneCamera.enabled = true;
    }

    public void StartCutsceneOverride(Transform targetTransform)
    {
        _cutsceneTransform.position = targetTransform.position;
        _cutsceneTransform.parent = targetTransform;
        InputManager.Instance.EnableMouseInput(false);
        gameplayCamera.Priority.Value = 5;
        cutsceneCamera.Priority.Value = 10;
    }

    public void EndCutsceneOverride()
    {
        cutsceneCamera.Priority.Value = 5;
        gameplayCamera.Priority.Value = 10;
        _cutsceneTransform.parent = null;
        InputManager.Instance.EnableMouseInput(true);
    }

    public Quaternion TriggerSave()
    {
        return gameplayCamera.transform.rotation;
    }
}
