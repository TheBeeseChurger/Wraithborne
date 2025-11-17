using System.Collections.Generic;
using UnityEngine;

public class PlayerRuntimeState
{
    public DeckInstance Deck;
    public List<CardInstance> Hand = new();
    public List<CardInstance> OnBoard = new();

    public const int MAX_HAND_SIZE = 10;

    public PlayerRuntimeState(DeckData deckData)
    {
        Deck = new DeckInstance(deckData);
    }
}
