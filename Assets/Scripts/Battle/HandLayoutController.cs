using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLayoutController : MonoBehaviour
{
    public static HandLayoutController Instance;

    [Header("Spawning Settings")]
    [SerializeField] GameObject handCardPrefab;
    [SerializeField] RectTransform spawnTransform;
    [SerializeField] GameObject worldCardPrefab;
    [SerializeField] Transform worldSpawnTransform;

    [Header("Layout Settings")]
    [SerializeField] float cardSpacing = 120f;
    [SerializeField] float hoverRaise = 40f;
    [SerializeField] float hoverSpread = 35f;
    [SerializeField] float maxFanAngle = 8f;

    private readonly List<HandCardController> _handCards = new();
    private HandCardController _hoveredHandCard;
    private PlayerRuntimeState _owner;

    private bool dragging;

    void Awake() => Instance = this;

    public void Initialize()
    {
        _owner = MatchSession.CurrentMatch.Player;
        _owner.CardAdded += MakeCard;

        if (MatchSession.CurrentMatch.TurnCount == 1) InitialDraw();
        else InitialLoad();
    }

    private void OnDisable()
    {
        _owner.CardAdded -= MakeCard;
    }

    private void InitialDraw()
    {
        _owner.Draw(5);
    }

    private async void InitialLoad()
    {
        foreach (var card in _owner.Hand)
        {
            MakeCard(card);
            await Awaitable.WaitForSecondsAsync(0.5f);
        }
    }

    public int GetIndex(CardInstance card)
    {
        return _owner.Hand.IndexOf(card);
    }

    public bool SpawnOnTile(CardInstance card, TileManager tile)
    {
        // Validation
        if (card.Data.CardType == CardTypes.Ritual) return false;
        if (card.Data.CardType == CardTypes.Heart) return false;
        if (tile.instance.occupant != null) return false;
        //if (card.currentCost > current pulse reserve) return false;

        var x = tile.instance.tileX;
        var y = tile.instance.tileY;
        
        var index = GetIndex(card);

        _owner.Spawn(index, x, y);
        return true;
    }

    public void SwitchToWorld(HandCardController UICardController)
    {
        CardPreviewPanel.Instance.Lock(true);
        GenerateWorldCard(UICardController.GetInstance());
        _handCards.Remove(UICardController);
        Destroy(UICardController.gameObject);

        dragging = false;
        SetHovered(null);
    }

    public void SwitchToUI(WorldCardController worldCardController)
    {
        CardPreviewPanel.Instance.Lock(false);
        var instance = worldCardController.GetInstance();
        CardPreviewPanel.Instance.Hide(instance.instanceID);
        MakeCard(instance);
        Destroy(worldCardController.gameObject);
    }

    public void DeleteWorldCard(WorldCardController worldCardController)
    {
        CardPreviewPanel.Instance.Lock(false);
        var instance = worldCardController.GetInstance();
        CardPreviewPanel.Instance.Hide(instance.instanceID);
        Destroy(worldCardController.gameObject);
    }

    private void GenerateWorldCard(CardInstance card)
    {
        var worldCard = Instantiate<GameObject>(worldCardPrefab).GetComponent<WorldCardController>();
        worldCard.transform.SetParent(worldSpawnTransform);
        worldCard.Initialize(card, MatchSession.CurrentMatch.GetCardFrame(card.Data));
    }

    private void MakeCard(CardInstance card)
    {
        var uICard = Instantiate<GameObject>(handCardPrefab).GetComponent<HandCardController>();
        uICard.Initialize(card, MatchSession.CurrentMatch.GetCardFrame(card.Data));
        uICard.transform.SetParent(spawnTransform, false);

        _handCards.Add(uICard);
        var spawnPos = new Vector3(spawnTransform.position.x - 500f, spawnTransform.position.y, spawnTransform.position.z);
        ReturnCard(uICard, spawnPos);
    }

    private void ReturnCard(HandCardController card, Vector3 startingPos)
    {
        card.transform.position = startingPos;
        RefreshLayout();
    }

    public void SetHovered(HandCardController card)
    {
        if (dragging) return;
        _hoveredHandCard = card;
        RefreshLayout();
    }

    public void SetDragging(bool dragging)
    {
        this.dragging = dragging;

        if (!dragging) RefreshLayout();
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
