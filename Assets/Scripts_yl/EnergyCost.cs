using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Energy cost data structure
/// Supports generic costs (any element) and element-specific costs
/// </summary>
[Serializable]
public struct EnergyCost
{
    public int genericCost;
    public List<ElementCost> specificCosts;

    /// <summary>
    /// Get total energy cost amount
    /// </summary>
    public int TotalCost
    {
        get
        {
            int total = genericCost;
            if (specificCosts != null)
            {
                foreach (var cost in specificCosts)
                {
                    total += cost.amount;
                }
            }
            return total;
        }
    }

    /// <summary>
    /// Check if this energy cost can be afforded
    /// </summary>
    public bool CanAfford(Dictionary<ElementType, int> availableEnergy)
    {
        // Check specific element costs
        if (specificCosts != null)
        {
            foreach (var cost in specificCosts)
            {
                if (!availableEnergy.ContainsKey(cost.elementType) || 
                    availableEnergy[cost.elementType] < cost.amount)
                {
                    return false;
                }
            }
        }

        // Check generic cost (need to calculate remaining energy)
        if (genericCost > 0)
        {
            int totalAvailable = 0;
            int usedForSpecific = 0;

            // Calculate energy already used for specific costs
            if (specificCosts != null)
            {
                foreach (var cost in specificCosts)
                {
                    usedForSpecific += cost.amount;
                }
            }

            // Calculate total available energy
            foreach (var energy in availableEnergy.Values)
            {
                totalAvailable += energy;
            }

            return (totalAvailable - usedForSpecific) >= genericCost;
        }

        return true;
    }
}

/// <summary>
/// Element-specific energy cost
/// </summary>
[Serializable]
public struct ElementCost
{
    [Tooltip("Element type")]
    public ElementType elementType;
    
    [Tooltip("Amount required")]
    public int amount;

    public ElementCost(ElementType type, int amt)
    {
        elementType = type;
        amount = amt;
    }
}
