using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine.Assertions;
using System.Dynamic;

[System.Serializable]
public class Deck : List<Card>
{
    public Deck() { }
    public string PrintDeckContent(int minLine = 0, int maxLine = int.MaxValue)
    {
        StringBuilder builder = new StringBuilder();
        int limit = Math.Min(maxLine, Count);
        Assert.IsTrue(minLine <= limit);
        for (int i = 0; i < limit; i++)
        {
            builder.AppendLine($"  {i}. {this[i].Title}");
        }
        return builder.ToString();
    }

    public void AddCard(Card card)
    {
        Assert.IsNotNull(card);
        this.Add(card);
    }

    public void RemoveCard(int index)
    {
        this.RemoveAt(index);
    }

    public void FillDeckWithPlaceholderCards()
    {
        this.Clear();
        for (int i = 0; i < 10; i++)
        {
            Card card = new Card("Placeholder Card", "Temp.");
            this.Add(card);
        }
    }
}
