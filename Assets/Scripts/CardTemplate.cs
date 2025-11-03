using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Card Template ScriptableObject
/// Used to define card base data, similar to Slay the Spire's card system
/// </summary>
[CreateAssetMenu(fileName = "New Card", menuName = "Card System/Card Template")]
public class CardTemplate : ScriptableObject
{
    [Header("Basic Info")]
    [Tooltip("Card name")]
    public string cardName;

    [Tooltip("Card description")]
    [TextArea(3, 6)]
    public string description;

    [Header("Energy Cost")]
    [Tooltip("Energy required to play this card")]
    public EnergyCost energyCost;

    [Header("Card Effects")]
    [Tooltip("List of card effects")]
    public List<CardEffect> effects = new List<CardEffect>();

    [Header("Visual Elements")]
    [Tooltip("Card artwork/illustration")]
    public Sprite cardArt;

    [Tooltip("Card's primary element (for visual categorization)")]
    public ElementType primaryElement = ElementType.None;

    /// <summary>
    /// Get the full card description (including all effects)
    /// </summary>
    public string GetFullDescription()
    {
        if (!string.IsNullOrEmpty(description))
            return description;

        // If no hardcoded description, auto-generate
        string auto = "";
        foreach (var effect in effects)
        {
            auto += effect.GetEffectDescription() + "\n";
        }
        return auto.TrimEnd('\n');
    }

    /// <summary>
    /// Check if this card can be played (based on energy)
    /// </summary>
    /// <param name="availableEnergy">Currently available energy</param>
    /// <returns>Whether the card can be played</returns>
    public bool CanPlay(Dictionary<ElementType, int> availableEnergy)
    {
        return energyCost.CanAfford(availableEnergy);
    }

    /// <summary>
    /// Get the energy cost description text
    /// </summary>
    public string GetCostDescription()
    {
        string costText = "";

        if (energyCost.genericCost > 0)
        {
            costText += $"Generic: {energyCost.genericCost} ";
        }

        if (energyCost.specificCosts != null && energyCost.specificCosts.Count > 0)
        {
            foreach (var cost in energyCost.specificCosts)
            {
                costText += $"{cost.elementType}: {cost.amount} ";
            }
        }

        return string.IsNullOrEmpty(costText) ? "0" : costText.TrimEnd();
    }

    /// <summary>
    /// Validate card data is valid
    /// </summary>
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(cardName))
        {
            cardName = "Unnamed Card";
        }

        if (effects == null)
        {
            effects = new List<CardEffect>();
        }
    }
}
