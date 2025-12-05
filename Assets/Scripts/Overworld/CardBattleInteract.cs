using UnityEngine;

public class CardBattleInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string startingText = "Press [E] to initiate a card battle.";
    [SerializeField]
    private string ongoingText = "Press [E] to continue card battle.";

    [SerializeField]
    private DeckData enemyDeck;
    [SerializeField]
    private MapData map;

    public void Interact()
    {
        OverworldManager.Instance.StartCardBattle(enemyDeck, map);
    }

    public string GetPromptText()
    {
        if (MatchSession.CurrentMatch == null) return startingText;
        else return ongoingText;
    }
}
