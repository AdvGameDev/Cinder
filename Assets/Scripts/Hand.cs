using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hand
{
    public event Action<Card> OnCardAdded;
    public event Action<Card> OnCardRemoved;

    public List<Card> cardsInHand = new List<Card>();
    public int maxHandSize = 10;

    public bool AddCard(Card card)
    {
        if (cardsInHand.Count >= maxHandSize)
        {
            Debug.LogWarning("Hand is full, cannot add card.");
            return false;
        }
        cardsInHand.Add(card);
        OnCardAdded?.Invoke(card);
        return true;
    }

    public bool RemoveCard(Card card)
    {
        bool removed = cardsInHand.Remove(card);
        if (removed)
        {
            OnCardRemoved?.Invoke(card);
        }
        return removed;
    }

    public void ClearHand()
    {
        for (int i = cardsInHand.Count - 1; i >= 0; i--)
        {
            RemoveCard(cardsInHand[i]);
        }
    }

    public int CardCount => cardsInHand.Count;
}
