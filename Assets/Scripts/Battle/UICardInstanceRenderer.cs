using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICardInstanceRenderer : MonoBehaviour
{
    [SerializeField] Image frameRenderer;
    [SerializeField] Image artworkRenderer;

    [SerializeField] TextMeshProUGUI cardName;
    [SerializeField] TextMeshProUGUI costAmount;
    [SerializeField] TextMeshProUGUI descriptionBox;

    private CardInstance _cardInstance;

    public void Initialize(CardInstance cardInstance, Sprite frame)
    {
        _cardInstance = cardInstance;

        artworkRenderer.sprite = _cardInstance.Data.Artwork;
        frameRenderer.sprite = frame;

        costAmount.text = _cardInstance.currentCost.ToString();
        cardName.text = _cardInstance.Data.CardName;
        descriptionBox.text = _cardInstance.mainText;
    }
}
