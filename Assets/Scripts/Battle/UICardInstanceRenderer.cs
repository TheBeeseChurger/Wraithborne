using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICardInstanceRenderer : MonoBehaviour, IPreviewable
{
    [SerializeField] Image frameRenderer;
    [SerializeField] Image artworkRenderer;

    [SerializeField] TextMeshProUGUI cardName;
    [SerializeField] TextMeshProUGUI costAmount;
    [SerializeField] TextMeshProUGUI descriptionBox;
    [SerializeField] TextMeshProUGUI cardDamage;
    [SerializeField] TextMeshProUGUI cardHealth;

    private CardInstance _cardInstance;

    public void Initialize(CardInstance cardInstance, Sprite frame)
    {
        _cardInstance = cardInstance;

        artworkRenderer.sprite = _cardInstance.Data.Artwork;
        frameRenderer.sprite = frame;

        costAmount.text = _cardInstance.currentCost.ToString();
        cardName.text = _cardInstance.Data.CardName;
        descriptionBox.text = _cardInstance.mainText;

        if (_cardInstance.currentDamage >= 0)
        {
            cardDamage.text = _cardInstance.currentDamage.ToString();
            cardHealth.text = _cardInstance.currentHealth.ToString();
        }
        else
        {
            cardDamage.text = "";
            cardHealth.text = "";
        }
    }

    public CardInstance GetCardInstance()
    {
        return _cardInstance;
    }
}
