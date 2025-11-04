using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hand
{
    public List<Card> cardsInHand = new List<Card>();
    public int maxHandSize = 10;
}
