using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player combatant with energy management and card playing
/// </summary>
public class Player : Combatant
{
    [Header("Player Settings")]
    [SerializeField] private int energyPerTurn = 3;

    private Dictionary<ElementType, int> currentEnergy = new Dictionary<ElementType, int>();

    public event Action<Dictionary<ElementType, int>> OnEnergyChanged;

    public Dictionary<ElementType, int> CurrentEnergy => currentEnergy;

    protected override void Awake()
    {
        base.Awake();
        InitializeEnergy();
    }

    private void InitializeEnergy()
    {
        currentEnergy.Clear();
        // Initialize all element types to 0
        foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
        {
            currentEnergy[element] = 0;
        }
    }

    /// <summary>
    /// Start player's turn - restore energy
    /// </summary>
    public void StartTurn()
    {
        // Restore Neutral energy at the start of turn
        currentEnergy[ElementType.Neutral] = energyPerTurn;
        
        Debug.Log($"Player turn started. Energy: {energyPerTurn} Neutral");
        OnEnergyChanged?.Invoke(currentEnergy);
    }

    /// <summary>
    /// End player's turn - clear remaining energy
    /// </summary>
    public void EndTurn()
    {
        // Clear all energy at end of turn
        foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
        {
            currentEnergy[element] = 0;
        }
        
        Debug.Log("Player turn ended. All energy cleared.");
        OnEnergyChanged?.Invoke(currentEnergy);
    }

    /// <summary>
    /// Attempt to play a card
    /// </summary>
    public bool TryPlayCard(CardTemplate card, Combatant target)
    {
        if (card == null)
        {
            Debug.LogWarning("Cannot play null card!");
            return false;
        }

        // Check if we have enough energy
        if (!CanAffordCard(card))
        {
            Debug.LogWarning($"Not enough energy to play {card.cardName}!");
            return false;
        }

        // Spend the energy
        SpendEnergy(card.energyCost);

        // Execute card effects
        ExecuteCardEffects(card, target);

        Debug.Log($"Played card: {card.cardName}");
        return true;
    }

    /// <summary>
    /// Check if player can afford to play this card
    /// </summary>
    private bool CanAffordCard(CardTemplate card)
    {
        // Check specific element costs
        if (card.energyCost.specificCosts != null)
        {
            foreach (var cost in card.energyCost.specificCosts)
            {
                if (!currentEnergy.ContainsKey(cost.elementType) || 
                    currentEnergy[cost.elementType] < cost.amount)
                {
                    return false;
                }
            }
        }

        // Check generic cost
        if (card.energyCost.genericCost > 0)
        {
            int totalAvailable = 0;
            int usedForSpecific = 0;

            if (card.energyCost.specificCosts != null)
            {
                foreach (var cost in card.energyCost.specificCosts)
                {
                    usedForSpecific += cost.amount;
                }
            }

            foreach (var energy in currentEnergy.Values)
            {
                totalAvailable += energy;
            }

            return (totalAvailable - usedForSpecific) >= card.energyCost.genericCost;
        }

        return true;
    }

    /// <summary>
    /// Spend energy for a card
    /// </summary>
    private void SpendEnergy(EnergyCost cost)
    {
        // Spend specific costs first
        if (cost.specificCosts != null)
        {
            foreach (var elementCost in cost.specificCosts)
            {
                currentEnergy[elementCost.elementType] -= elementCost.amount;
            }
        }

        // Spend generic cost from any available energy
        int remainingGeneric = cost.genericCost;
        foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
        {
            if (remainingGeneric <= 0) break;

            int available = currentEnergy[element];
            int toSpend = Mathf.Min(available, remainingGeneric);
            currentEnergy[element] -= toSpend;
            remainingGeneric -= toSpend;
        }

        OnEnergyChanged?.Invoke(currentEnergy);
    }

    /// <summary>
    /// Execute all effects on a card
    /// </summary>
    private void ExecuteCardEffects(CardTemplate card, Combatant target)
    {
        foreach (var effect in card.effects)
        {
            ExecuteEffect(effect, target);
        }
    }

    /// <summary>
    /// Execute a single card effect
    /// </summary>
    private void ExecuteEffect(CardEffect effect, Combatant target)
    {
        switch (effect.effectType)
        {
            case CardEffectType.Damage:
                if (target != null)
                {
                    target.TakeDamage(effect.effectValue);
                }
                break;

            case CardEffectType.Heal:
                Heal(effect.effectValue);
                break;

            case CardEffectType.Block:
                // TODO: Implement block system later
                Debug.Log($"Player gained {effect.effectValue} block (not implemented yet)");
                break;

            default:
                Debug.LogWarning($"Effect type {effect.effectType} not implemented yet!");
                break;
        }
    }

    public override void ResetCombatant()
    {
        base.ResetCombatant();
        InitializeEnergy();
    }
}
