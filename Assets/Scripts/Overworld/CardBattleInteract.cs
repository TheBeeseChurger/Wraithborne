using UnityEngine;

public class CardBattleInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string text = "Press [E] to initiate a card battle.";

    [SerializeField]
    private DeckData enemyDeck;

    public void Interact()
    {
        OverworldManager.Instance.StartCardBattle(enemyDeck);
    }

    public string GetPromptText()
    {
        return text;
    }
}
