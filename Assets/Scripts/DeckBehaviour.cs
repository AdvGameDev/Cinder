using System.Collections.Generic;
using UnityEngine;

public class DeckBehaviour : MonoBehaviour
{
    public Deck deck;
    public Hand hand;

    private void Start()
    {
        if (deck == null) deck = new Deck();
        if (hand == null) hand = new Hand();
    }

    private void OnMouseDown()
    {
        deck.DrawCard(hand);
    }
}
