using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLayoutController : MonoBehaviour
{
    public static HandLayoutController Instance;

    [Header("Spawning Settings")]
    [SerializeField] GameObject handCardPrefab;
    [SerializeField] RectTransform spawnTransform;

    [Header("Layout Settings")]
    [SerializeField] float cardSpacing = 120f;
    [SerializeField] float hoverRaise = 40f;
    [SerializeField] float hoverSpread = 35f;
    [SerializeField] float maxFanAngle = 8f;

    private readonly List<HandCardController> _handCards = new();
    private HandCardController _hoveredHandCard;
    private PlayerRuntimeState _owner;

    private RectTransform anchor;

    void Awake() => Instance = this;

    public void Initialize()
    {
        _owner = MatchSession.CurrentMatch.Player;
        _owner.CardAdded += MakeCard;

        anchor = GetComponent<RectTransform>();

        InitialDraw();
    }

    private void InitialDraw()
    {
        _owner.Draw(5);
    }

    private void MakeCard(CardInstance card)
    {
        var uICard = Instantiate<GameObject>(handCardPrefab).GetComponent<HandCardController>();
        uICard.Initialize(card, MatchSession.CurrentMatch.GetCardFrame(card.Data));
        uICard.transform.SetParent(anchor, false);

        _handCards.Add(uICard);
        ReturnCard(uICard, spawnTransform.position);
    }

    private void ReturnCard(HandCardController card, Vector3 startingPos)
    {
        card.transform.position = startingPos;
        RefreshLayout();
    }

    public void SetHovered(HandCardController card)
    {
        _hoveredHandCard = card;
        RefreshLayout();
    }

    public void RefreshLayout()
    {
        int count = _handCards.Count;
        if (count == 0) return;

        float totalWidth = (count - 1) * cardSpacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < count; i++)
        {
            var card = _handCards[i];

            float x = startX + cardSpacing * i;
            float y = 0;
            float t = -(i - (count - 1) / 2f);
            float rotAngle = t * maxFanAngle;
            if (_hoveredHandCard != null)
            {
                int hoveredIndex = _handCards.IndexOf(_hoveredHandCard);

                if (card == _hoveredHandCard)
                {
                    y += hoverRaise;
                    rotAngle = 0;
                }
                else
                {
                    int dir = Mathf.Clamp(i - hoveredIndex, -1, 1);
                    x += dir * hoverSpread * Mathf.Abs(i - hoveredIndex);
                }
            }

            Vector2 targetPos = new(x, y);
            card.SetTargetPosition(targetPos);
            card.SetTargetRotation(rotAngle);
        }
    }
}
