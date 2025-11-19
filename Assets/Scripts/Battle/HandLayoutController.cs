using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLayoutController : MonoBehaviour
{
    public static HandLayoutController Instance;

    [Header("Spawning Settings")]
    [SerializeField] GameObject uICardPrefab;
    [SerializeField] RectTransform spawnTransform;

    [Header("Layout Settings")]
    [SerializeField] float cardSpacing = 120f;
    [SerializeField] float hoverRaise = 40f;
    [SerializeField] float hoverSpread = 35f;
    [SerializeField] float maxFanAngle = 8f;

    private List<UICardInstanceRenderer> _uICards = new();
    private PlayerRuntimeState _owner;

    private RectTransform anchor;

    void Awake() => Instance = this;

    public void Initialize()
    {
        _owner = MatchSession.CurrentMatch.Player;
        _owner.CardAdded += MakeCard;

        anchor = GetComponent<RectTransform>();

        StartCoroutine(InitialDraw());
    }

    IEnumerator InitialDraw()
    {
        while (_uICards.Count < 5)
        {
            yield return new WaitForSeconds(0.5f);
            _owner.Draw(1);
        }
    }

    private void MakeCard(CardInstance card)
    {
        var uICard = Instantiate<GameObject>(uICardPrefab).GetComponent<UICardInstanceRenderer>();
        uICard.Initialize(card, MatchSession.CurrentMatch.GetCardFrame(card.Data));
        uICard.transform.SetParent(anchor, false);

        _uICards.Add(uICard);
        ReturnCard(uICard, spawnTransform.anchoredPosition);
    }

    private void ReturnCard(UICardInstanceRenderer card, Vector2 startingPos)
    {

    }
}
