using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Card effect type enumeration
/// Can be extended with more effect types based on game needs
/// </summary>
public enum CardEffectType
{
    None = 0,

    // Damage related
    Damage = 1,              // Deal damage
    AOEDamage = 2,           // Area of effect damage

    // Defense related
    Block = 10,              // Gain block/armor

    // Healing related
    Heal = 20,               // Heal

    // Energy related
    GainEnergy = 30,         // Gain energy

    // Card draw related
    DrawCards = 40,          // Draw cards

    // Buff related
    ApplyBuff = 50,          // Apply buff effect
    ApplyDebuff = 51,        // Apply debuff effect

    // Special effects
    Custom = 100             // Custom effect
}

[Serializable]
public struct CardEffect
{
    [Tooltip("Effect type")]
    public CardEffectType effectType;
    
    [Tooltip("Effect value")]
    public int effectValue;
    
    [Tooltip("Element Type (optional)")]
    public ElementType associatedElement;
    
    [Tooltip("Effect description (for custom effects or additional explanation)")]
    public string effectDescription;

    /// <summary>
    /// Create a simple effect
    /// </summary>
    public CardEffect(CardEffectType type, int value)
    {
        effectType = type;
        effectValue = value;
        associatedElement = ElementType.None;
        effectDescription = string.Empty;
    }

    /// <summary>
    /// Create an effect with element association
    /// </summary>
    public CardEffect(CardEffectType type, int value, ElementType element)
    {
        effectType = type;
        effectValue = value;
        associatedElement = element;
        effectDescription = string.Empty;
    }

    /// <summary>
    /// Get the effect's description text
    /// </summary>
    public string GetEffectDescription()
    {
        if (!string.IsNullOrEmpty(effectDescription))
            return effectDescription;

        // Generate default description based on effect type
        switch (effectType)
        {
            case CardEffectType.Damage:
                return $"Deal {effectValue} damage";
            case CardEffectType.AOEDamage:
                return $"Deal {effectValue} damage to all enemies";
            case CardEffectType.Block:
                return $"Gain {effectValue} block";
            case CardEffectType.Heal:
                return $"Heal {effectValue} HP";
            case CardEffectType.GainEnergy:
                return associatedElement != ElementType.None 
                    ? $"Gain {effectValue} {associatedElement} energy" 
                    : $"Gain {effectValue} energy";
            case CardEffectType.DrawCards:
                return $"Draw {effectValue} card(s)";
            case CardEffectType.ApplyBuff:
                return $"Apply buff (Strength: {effectValue})";
            case CardEffectType.ApplyDebuff:
                return $"Apply debuff (Strength: {effectValue})";
            default:
                return "Unknown effect";
        }
    }
}
