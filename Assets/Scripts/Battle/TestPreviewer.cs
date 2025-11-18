using UnityEngine;

public class TestPreviewer : MonoBehaviour, IPreviewable
{
    public CardData cardData;
    public CardInstance cardInstance;
    public CardInstance GetCardInstance()
    {
        return cardInstance;
    }

    private void Start()
    {
        cardInstance = new CardInstance(cardData);
    }
}
