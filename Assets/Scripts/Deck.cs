using System.Collections.Generic;
using UnityEngine;
using System.Text;

[System.Serializable]
public class Deck
{
    public List<Card> cards = new List<Card>();

    public void Initialize(List<Card> initialCards)
    {
        cards.Clear();
        if (initialCards != null)
        {
            cards.AddRange(initialCards);
        }
        Shuffle();
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public Card DrawCard()
    {
        if (cards.Count == 0)
        {
            return null;
        }

        Card drawnCard = cards[0];
        cards.RemoveAt(0);

        return drawnCard;
    }

    public void AddCard(Card card)
    {
        if (card != null)
        {
            cards.Add(card);
        }
    }

    public string PrintDeckContent()
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < cards.Count; i++)
        {
            builder.AppendLine($"  {i}. {cards[i].cardName}");
        }
        return builder.ToString();
    }

    public int CardCount => cards.Count;
}
