using System.Collections;
using UnityEngine;

public class TestCutscene : MonoBehaviour
{
    [SerializeField]
    private CinemachineCustomFeeder feeder;
    [SerializeField]
    private GameObject focusObject;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit something: " + other.name);
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            player.CanMove = false;
            InputManager.Instance.EnableMouseInput(false);
            StartCoroutine(Cutscene(player));
        }
    }

    private IEnumerator Cutscene(PlayerController player)
    {
        yield return new WaitForSeconds(3);
        player.CanMove = true;
        InputManager.Instance.EnableMouseInput(true);
    }
}
