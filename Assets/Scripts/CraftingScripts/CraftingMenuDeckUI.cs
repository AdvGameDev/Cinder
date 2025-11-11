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
    private Dictionary<Card, CraftingDeckViewCardUI> _cardToUIMap = new Dictionary<Card, CraftingDeckViewCardUI>();
    private List<GameObject> CurrentlyDisplayedCards = new List<GameObject>();

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
            CraftingDeckViewCardUI cardUI = cardGO.GetComponent<CraftingDeckViewCardUI>();
            if (cardUI != null)
            {
                Card card = PlayerDeck.cards[i];
                cardUI.Initialize(card, CraftingManager);
                Debug.Log($"Instanciating {card.cardName}");
                _cardToUIMap[card] = cardUI;
            }
            else
            {
                Debug.LogError("The assigned Card Prefab does not have a CraftingDeckViewCardUI component on it");
            }
        }
    }

    public void AddCardToDisplay(Card card)
    {
        if (CardPrefab == null)
        {
            Debug.LogError($"Card Prefab is not set in {this.name}");
            return;
        }
        GameObject cardGO = Instantiate(CardPrefab, DeckContainer);
        CraftingDeckViewCardUI cardUI = cardGO.GetComponent<CraftingDeckViewCardUI>();
        if (cardUI != null)
        {
            cardUI.Initialize(card, CraftingManager);
            Debug.Log($"Instanciating {card.cardName}");
            _cardToUIMap[card] = cardUI;
        }
        else
        {
            Debug.LogError("The assigned Card Prefab does not have a CraftingDeckViewCardUI component on it");
        }

    }
}
