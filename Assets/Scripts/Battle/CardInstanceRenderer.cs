using TMPro;
using UnityEngine;

public class CardInstanceRenderer : MonoBehaviour, IPreviewable
{
    [SerializeField] CardSpriteRenderer frameRenderer;
    [SerializeField] CardSpriteRenderer artworkRenderer;

    [SerializeField] TextMeshProUGUI cardName;
    [SerializeField] TextMeshProUGUI costAmount;
    [SerializeField] TextMeshProUGUI descriptionBox;

    private CardInstance _cardInstance;

    // This is a temp variable
    [Header("To be deleted variables")]
    public CardData cardData;
    public FrameMaker frameMaker;

    private void Start()
    {
        if (cardData == null) return;

        // This is temp assignment
        _cardInstance = new CardInstance(cardData);

        artworkRenderer.SetSprite(_cardInstance.Data.Artwork);
        frameRenderer.SetSprite(frameMaker.PickFrame(cardData));

        costAmount.text = _cardInstance.currentCost.ToString();
        cardName.text = _cardInstance.Data.CardName;
        descriptionBox.text = _cardInstance.mainText;
    }

    public void Initialize(CardInstance cardInstance, Sprite frame)
    {
        _cardInstance = cardInstance;

        artworkRenderer.SetSprite(_cardInstance.Data.Artwork);
        frameRenderer.SetSprite(frame);

        costAmount.text = _cardInstance.currentCost.ToString();
        cardName.text = _cardInstance.Data.CardName;
        descriptionBox.text = _cardInstance.mainText;
    }

    public CardInstance GetCardInstance()
    {
        return _cardInstance;
    }
}
