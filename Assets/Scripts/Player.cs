using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Character
{
    [Header("Turn Settings")]
    [SerializeField] public int drawsPerTurn = 3;
    public int drawsRemaining;

    // --- Card Piles ---
    public Deck actionDrawPile = new Deck();
    public Deck energyDrawPile = new Deck();
    public Hand hand = new Hand();
    public List<Card> exhaustPile = new List<Card>();

    // --- Resources ---
    private Dictionary<ElementType, int> _currentEnergy = new Dictionary<ElementType, int>();
    public event Action<Dictionary<ElementType, int>> OnEnergyChanged;
    public Dictionary<ElementType, int> CurrentEnergy => _currentEnergy;

    public int currentBlock;
    public event Action<int> OnBlockChanged;

    // --- Initialization ---

    protected override void Awake()
    {
        base.Awake();
        InitializeEnergy();
        currentBlock = 0;
    }

    public void Setup(int startingHealth, int newMaxHealth, List<Card> initialActionDeck, List<Card> initialEnergyDeck)
    {
        base.Setup(startingHealth, newMaxHealth);

        actionDrawPile.Initialize(initialActionDeck);
        energyDrawPile.Initialize(initialEnergyDeck);

        hand.ClearHand();
        exhaustPile.Clear();

        InitializeEnergy();
        currentBlock = 0;
        OnBlockChanged?.Invoke(currentBlock);
    }

    private void InitializeEnergy()
    {
        CurrentEnergy.Clear();
        foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
        {
            _currentEnergy[element] = 0;
        }
        OnEnergyChanged?.Invoke(new Dictionary<ElementType, int>(_currentEnergy));
    }

    // --- Turn Management ---

    public void StartTurn()
    {
        currentBlock = 0;
        OnBlockChanged?.Invoke(currentBlock);

        drawsRemaining = drawsPerTurn;
        Debug.Log($"Player turn started. {drawsRemaining} draws remaining.");
    }

    public void DrawCard(CardType deckType)
    {
        if (drawsRemaining <= 0)
        {
            Debug.LogWarning("No draws remaining for this turn.");
            return;
        }

        Card drawnCard = null;
        if (deckType == CardType.Action)
        {
            drawnCard = actionDrawPile.DrawCard();
        }
        else
        {
            drawnCard = energyDrawPile.DrawCard();
        }

        if (drawnCard != null)
        {
            hand.AddCard(drawnCard);
            drawsRemaining--;
            Debug.Log($"Drew '{drawnCard.cardName}'. {drawsRemaining} draws remaining.");
        }
        else
        {
            Debug.LogWarning($"Could not draw card from {deckType} deck (empty).");
        }
    }

    public void EndTurn()
    {
        // Clear all temporary energy
        InitializeEnergy();
        Debug.Log("Player turn ended. Unplayed cards remain in hand.");
    }

    // --- Energy & Card Logic ---

    private int GetTotalEnergyAvailableForGeneric(EnergyCost cost)
    {
        int totalAvailable = 0;
        foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
        {
            int availableForGeneric = _currentEnergy.ContainsKey(element) ? _currentEnergy[element] : 0;

            var specificCostForElement = cost.specificCosts?.FirstOrDefault(ec => ec.elementType == element);
            if (specificCostForElement.HasValue)
            {
                availableForGeneric -= specificCostForElement.Value.amount;
            }

            if (availableForGeneric > 0)
            {
                totalAvailable += availableForGeneric;
            }
        }
        return totalAvailable;
    }

    public bool TrySpendEnergy(EnergyCost cost)
    {
        // Check specific costs
        if (cost.specificCosts != null)
        {
            foreach (var elementCost in cost.specificCosts)
            {
                if (!_currentEnergy.ContainsKey(elementCost.elementType) || _currentEnergy[elementCost.elementType] < elementCost.amount)
                {
                    return false;
                }
            }
        }

        // Check generic cost
        if (cost.genericCost > 0)
        {
            if (GetTotalEnergyAvailableForGeneric(cost) < cost.genericCost)
            {
                return false;
            }
        }

        // If affordable, proceed to Spend

        if (cost.specificCosts != null)
        {
            foreach (var elementCost in cost.specificCosts)
            {
                _currentEnergy[elementCost.elementType] -= elementCost.amount;
            }
        }

        // Spend generic cost from the most abundant element
        int remainingGeneric = cost.genericCost;
        while (remainingGeneric > 0)
        {
            ElementType? mostAbundantElement = null;
            int maxAmount = 0;

            foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
            {
                // We only consider the energy that is currently in our pool for spending
                if (_currentEnergy.ContainsKey(element) && _currentEnergy[element] > maxAmount)
                {
                    maxAmount = _currentEnergy[element];
                    mostAbundantElement = element;
                }
            }

            if (mostAbundantElement == null || maxAmount == 0)
            {
                Debug.LogError("TrySpendEnergy failed: No energy available for generic cost, but passed affordability check.");
                // This case should ideally not be reached if the affordability check is correct.
                // Break to prevent an infinite loop.
                break;
            }

            int toSpend = Mathf.Min(remainingGeneric, _currentEnergy[mostAbundantElement.Value]);
            _currentEnergy[mostAbundantElement.Value] -= toSpend;
            remainingGeneric -= toSpend;
        }

        OnEnergyChanged?.Invoke(new Dictionary<ElementType, int>(_currentEnergy));
        return true;
    }

    public void PlayCard(Card card)
    {
        if (hand.RemoveCard(card))
        {
            exhaustPile.Add(card);
        }
    }

    public void GainEnergy(ElementType type, int amount)
    {
        if (amount <= 0) return;
        if (!_currentEnergy.ContainsKey(type)) _currentEnergy[type] = 0;

        _currentEnergy[type] += amount;

        OnEnergyChanged?.Invoke(new Dictionary<ElementType, int>(_currentEnergy));
    }

    // --- Block & Health Logic ---

    public void GainBlock(int amount)
    {
        if (amount <= 0) return;
        currentBlock += amount;
        OnBlockChanged?.Invoke(currentBlock);
    }

    public override void TakeDamage(int amount)
    {
        int damageToHealth = amount - currentBlock;
        currentBlock = Mathf.Max(0, currentBlock - amount);
        OnBlockChanged?.Invoke(currentBlock);

        if (damageToHealth > 0)
        {
            base.TakeDamage(damageToHealth);
        }
    }

    // --- Report Battle End ---

    private List<Card> GetRemainingCardsOfType(CardType type, Deck drawPile)
    {
        var allCards = new List<Card>();
        allCards.AddRange(drawPile.cards);
        allCards.AddRange(hand.cardsInHand.Where(c => c.cardType == type));
        return allCards;
    }

    public List<Card> GetAllRemainingActionCards()
    {
        return GetRemainingCardsOfType(CardType.Action, actionDrawPile);
    }

    public List<Card> GetAllRemainingEnergyCards()
    {
        return GetRemainingCardsOfType(CardType.Energy, energyDrawPile);
    }
}
