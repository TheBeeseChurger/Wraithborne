using System.Collections;
using UnityEngine;

public class TestCutscene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit something: " + other.name);
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            player.CanMove = false;
            StartCoroutine(Cutscene(player));
        }
    }

    private IEnumerator Cutscene(PlayerController player)
    {
        yield return new WaitForSeconds(5f);
        player.CanMove = true;
    }
}
