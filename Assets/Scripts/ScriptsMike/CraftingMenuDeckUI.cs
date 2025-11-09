using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CraftingMenuDeckUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject CardPrefab;
    private Transform DeckContainer;
    private CraftingManager CraftingManager;
    private Deck PlayerDeck;
    private Dictionary<Card, CraftingCardUI> _cardToUIMap = new Dictionary<Card, CraftingCardUI>();

    public void Initialize(CraftingManager manager, Deck deck)
    {
        CraftingManager = manager;
        PlayerDeck = deck;
        DeckContainer = this.transform;

        Debug.Log($"Initialized deck {deck.cards}");
    }

    public void DisplayDeck()
    {
        if (CardPrefab == null)
        {
            Debug.LogError($"Card Prefab is not set in {this.name}");
            return;
        }

        for (int i = 0; i < PlayerDeck.cards.Count; i++)
        {
            GameObject cardGO = Instantiate(CardPrefab, DeckContainer);
            CraftingCardUI cardUI = cardGO.GetComponent<CraftingCardUI>();
            if (cardUI != null)
            {
                Card card = PlayerDeck.cards[i];
                cardUI.Initialize(card, CraftingManager);
                Debug.Log($"Instanciating {card.cardName}");
                _cardToUIMap[card] = cardUI;
            }
            else
            {
                Debug.LogError("The assigned Card Prefab does not have a CardUI component on it");
            }
        }
    }
}
