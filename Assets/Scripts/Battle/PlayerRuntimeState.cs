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
    }

    public void Draw(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var card = Deck.Draw();
            if (card != null)
            {
                Hand.Add(card);
                CardAdded?.Invoke(card);
            }
        }
    }

    public (bool, int) IsHandOverfilled()
    {
        return (Hand.Count < MAX_HAND_SIZE, Hand.Count - MAX_HAND_SIZE);
    }
}
