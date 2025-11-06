using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera gameplayCamera;
    [SerializeField]
    private CinemachineCamera cutsceneCamera;


    private Transform _cutsceneTransform; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var obj = new GameObject("CutsceneFollowTransform");
        obj.transform.parent = null;
        _cutsceneTransform = obj.transform;
        cutsceneCamera.LookAt = _cutsceneTransform;

        cutsceneCamera.Priority.Value = 5;
        gameplayCamera.Priority.Value = 10;
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
}
