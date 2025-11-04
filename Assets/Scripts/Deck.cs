using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[System.Serializable]
public class Deck
{
    public List<Card> cards = new List<Card>();

    // Populate with mock cards
    public Deck()
    {
        cards.Add(new Card("One", 8, 5));
        cards.Add(new Card("Two", 5, 2));
        cards.Add(new Card("Three", 6, 9));
        cards.Add(new Card("Four", 4, 4));
        cards.Add(new Card("Five", 7, 3));
    }

    public void DrawCard(Hand hand)
    {
        if (cards.Count == 0)
        {
            return;
        }

        if (hand.cardsInHand.Count >= hand.maxHandSize)
        {
            return;
        }

        int lastIndex = cards.Count - 1;
        Card topCard = cards[lastIndex];
        cards.RemoveAt(lastIndex);

        // Add the card to the hand
        hand.cardsInHand.Add(topCard);
        return;
    }
}
