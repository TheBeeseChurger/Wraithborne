using UnityEngine;

public class ExampleInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string text = "Press E to interact with cube.";

    public void Interact()
    {
        Debug.Log("You interacted with me!");
    }

    public string GetPromptText()
    {
        return text;
    }
}
