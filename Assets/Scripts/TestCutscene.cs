using System.Collections;
using UnityEngine;

public class TestCutscene : MonoBehaviour
{
    private PlayerCameraController _playerCameraController;
    [SerializeField]
    private Transform _transform;

    private void Start()
    {
        _playerCameraController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCameraController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit something: " + other.name);
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            // Will be in a Cutscene Manager
            //player.CanMove = false;
            _playerCameraController.StartCutsceneOverride(_transform);

            StartCoroutine(Cutscene(player));
        }
    }

    private IEnumerator Cutscene(PlayerController player)
    {
        yield return new WaitForSeconds(5f);

        // Will be in a Cutscene Manager
        //player.CanMove = true;
        _playerCameraController.EndCutsceneOverride();
    }
}
