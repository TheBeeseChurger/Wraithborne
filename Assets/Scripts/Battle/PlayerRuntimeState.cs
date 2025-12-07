using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRuntimeState
{
    public DeckInstance Deck;
    public CardInstance HeartCard;
    public List<CardInstance> Hand = new();

    public event Action<CardInstance> CardAdded;

    public const int MAX_HAND_SIZE = 10;

    public PlayerRuntimeState(DeckData deckData)
    {
        this.HeartCard = new CardInstance(deckData.HeartCard);
        Deck = new DeckInstance(deckData);
        SpawnHeart();
    }

    public async void Draw(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var card = Deck.Draw();
            if (card != null)
            {
                Hand.Add(card);
                CardAdded?.Invoke(card);
                await Awaitable.WaitForSecondsAsync(0.5f);
            }
            else Debug.Log("No cards left in deck to draw! Draw() failed!");
        }
    }

    public void Spawn(int handPos, int x, int y)
    {
        if (Hand.Count - 1 >= handPos)
        {
            var card = Hand[handPos];
            Hand.RemoveAt(handPos);
            
            MatchSession.CurrentMatch.Map.SpawnUnit(card, x, y, this);
        }
    }

    private void SpawnHeart()
    {
        MatchSession.CurrentMatch.Map.SpawnHeartUnit(HeartCard, this);
    }

    public (bool, int) IsHandOverfilled()
    {
        return (Hand.Count < MAX_HAND_SIZE, Hand.Count - MAX_HAND_SIZE);
    }
}
