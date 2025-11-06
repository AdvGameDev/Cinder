using System.Collections.Generic;
using UnityEngine;

public enum CardType { Action, Energy }

[System.Serializable]
public class Card
{
    public string cardName;
    public CardType cardType;
    public string description;

    public EnergyCost energyCost;

    public List<CardEffect> effects;

    public Card()
    {
        effects = new List<CardEffect>();
    }
}
