using System.Collections.Generic;
using UnityEngine;

public class DeckInstance
{
    public List<CardInstance> DrawPile = new();
    public List<CardInstance> DiscardPile = new();
    public List<CardInstance> BanishPile = new();

    public DeckInstance(DeckData deckData)
    {
        foreach (var cardData in deckData.Deck)
        {
            DrawPile.Add(new CardInstance(cardData));
        }

        Shuffle(DrawPile);
    }

    public void Shuffle(List<CardInstance> pile)
    {
        for (int i = pile.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (pile[i], pile[j]) = (pile[j], pile[i]);
        }
    }

    public void Discard(CardInstance card) => DiscardPile.Add(card);
    public void Banish(CardInstance card) => BanishPile.Add(card);

    public void ReshuffleDiscard()
    {
        DrawPile.Clear();
        DrawPile.AddRange(DiscardPile);
        DiscardPile.Clear();
        Shuffle(DrawPile);
    }

    public CardInstance Draw()
    {
        if (DrawPile.Count == 0)
            ReshuffleDiscard();

        if (DrawPile.Count == 0) return null;

        var card = DrawPile[0];
        DrawPile.RemoveAt(0);
        return card;
    }
}
