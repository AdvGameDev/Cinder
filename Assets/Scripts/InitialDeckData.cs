using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class InitialDeckData
{
    public static List<Card> GetInitialPlayerDeck()
    {
        List<Card> deck = new List<Card>();

        // Basic Cards (Neutral Energy)
        Card strikeCard = new Card
        {
            cardName = "Strike",
            description = "Deal 6 damage",
            cardType = CardType.Action,
            energyCost = new EnergyCost(1, new List<ElementCost>()),
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.Damage, effectValue = 6 } },
            CraftingEssenceCost = new List<int> { 0, 0, 0, 0, 5 } // TEMP for MVP
        };
        deck.Add(strikeCard);
        deck.Add(strikeCard);
        deck.Add(strikeCard);

        Card blockCard = new Card
        {
            cardName = "Block",
            description = "Gain 5 block",
            cardType = CardType.Action,
            energyCost = new EnergyCost(1, new List<ElementCost>()),
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.Block, effectValue = 5 } },
            CraftingEssenceCost = new List<int> { 0, 0, 0, 0, 5 } // TEMP for MVP
        };
        deck.Add(blockCard);
        deck.Add(blockCard);
        deck.Add(blockCard);
        deck.Add(blockCard);

        // Elemental Cards
        Card fireballCard = new Card
        {
            cardName = "Fireball",
            description = "Deal 8 fire damage",
            cardType = CardType.Action,
            energyCost = new EnergyCost(0, new List<ElementCost> { new ElementCost(ElementType.Fire, 1) }),
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.Damage, effectValue = 8 } },
            CraftingEssenceCost = new List<int> { 5, 0, 0, 0, 0 } // TEMP for MVP
        };
        deck.Add(fireballCard);
        deck.Add(fireballCard);

        Card earthShieldCard = new Card
        {
            cardName = "Earth Shield",
            description = "Gain 7 block",
            cardType = CardType.Action,
            energyCost = new EnergyCost(0, new List<ElementCost> { new ElementCost(ElementType.Earth, 1) }),
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.Block, effectValue = 7 } },
            CraftingEssenceCost = new List<int> { 0, 5, 0, 0, 0 } // TEMP for MVP
        };
        deck.Add(earthShieldCard);
        deck.Add(earthShieldCard);
        deck.Add(earthShieldCard);

        Card blizzardCard = new Card
        {
            cardName = "Blizzard",
            description = "Deal 3 damage \nGain 3 block",
            cardType = CardType.Action,
            energyCost = new EnergyCost(0, new List<ElementCost> { new ElementCost(ElementType.Water, 1) }),
            effects = new List<CardEffect>
            {
                new CardEffect { effectType = CardEffectType.Damage, effectValue = 3 },
                new CardEffect { effectType = CardEffectType.Block, effectValue = 3 }
            },
            CraftingEssenceCost = new List<int> { 0, 0, 10, 0, 0 } // TEMP for MVP
        };
        deck.Add(blizzardCard);
        deck.Add(blizzardCard);
        deck.Add(blizzardCard);

        Debug.Log($"Generated initial action deck with {deck.Count} cards.");
        return deck;
    }

    public static List<Card> GetInitialEnergyDeck()
    {
        List<Card> energyDeck = new List<Card>();
        EnergyCost costToPlay = new EnergyCost(0, new List<ElementCost>());

        Card fireEnergyCard = new Card
        {
            cardName = "Fire Crystal",
            description = "Gain 1 fire energy",
            cardType = CardType.Energy,
            energyCost = costToPlay,
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.GainEnergy, effectValue = 1, elementType = ElementType.Fire } },
            CraftingEssenceCost = new List<int> { 0, 0, 0, 0, 1 } // TEMP for MVP
        };
        energyDeck.Add(fireEnergyCard);
        energyDeck.Add(fireEnergyCard);

        Card earthEnergyCard = new Card
        {
            cardName = "Earth Crystal",
            description = "Gain 1 earth energy",
            cardType = CardType.Energy,
            energyCost = costToPlay,
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.GainEnergy, effectValue = 1, elementType = ElementType.Earth } },
            CraftingEssenceCost = new List<int> { 0, 0, 0, 0, 1 } // TEMP for MVP
        };
        energyDeck.Add(earthEnergyCard);
        energyDeck.Add(earthEnergyCard);

        Card waterEnergyCard = new Card
        {
            cardName = "Water Crystal",
            description = "Gain 1 water energy",
            cardType = CardType.Energy,
            energyCost = costToPlay,
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.GainEnergy, effectValue = 1, elementType = ElementType.Water } },
            CraftingEssenceCost = new List<int> { 0, 0, 0, 0, 1 } // TEMP for MVP
        };
        energyDeck.Add(waterEnergyCard);
        energyDeck.Add(waterEnergyCard);

        Debug.Log($"Generated initial energy deck with {energyDeck.Count} cards.");
        return energyDeck;
    }

    public static List<Card> GetInitialCraftableCards()
    {
        List<Card> deck = new List<Card>();
        EnergyCost costToPlay = new EnergyCost(0, new List<ElementCost>());

        Card strikeCard = new Card
        {
            cardName = "Strike",
            description = "Deal 6 damage",
            cardType = CardType.Action,
            energyCost = new EnergyCost(1, new List<ElementCost>()),
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.Damage, effectValue = 6 } },
            CraftingEssenceCost = new List<int> { 0, 0, 0, 0, 5 } // TEMP for MVP
        };

        Card blockCard = new Card
        {
            cardName = "Block",
            description = "Gain 5 block",
            cardType = CardType.Action,
            energyCost = new EnergyCost(1, new List<ElementCost>()),
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.Block, effectValue = 5 } },
            CraftingEssenceCost = new List<int> { 0, 0, 0, 0, 5 } // TEMP for MVP
        };

        Card fireballCard = new Card
        {
            cardName = "Fireball",
            description = "Deal 8 fire damage",
            cardType = CardType.Action,
            energyCost = new EnergyCost(0, new List<ElementCost> { new ElementCost(ElementType.Fire, 1) }),
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.Damage, effectValue = 8 } },
            CraftingEssenceCost = new List<int> { 5, 0, 0, 0, 0 } // TEMP for MVP
        };

        Card earthShieldCard = new Card
        {
            cardName = "Earth Shield",
            description = "Gain 7 block",
            cardType = CardType.Action,
            energyCost = new EnergyCost(0, new List<ElementCost> { new ElementCost(ElementType.Earth, 1) }),
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.Damage, effectValue = 7 } },
            CraftingEssenceCost = new List<int> { 0, 5, 0, 0, 0 } // TEMP for MVP
        };

        Card blizzardCard = new Card
        {
            cardName = "Blizzard",
            description = "Deal 3 damage \nGain 3 block",
            cardType = CardType.Action,
            energyCost = new EnergyCost(0, new List<ElementCost> { new ElementCost(ElementType.Water, 1) }),
            effects = new List<CardEffect>
            {
                new CardEffect { effectType = CardEffectType.Damage, effectValue = 3 },
                new CardEffect { effectType = CardEffectType.Block, effectValue = 3 }
            },
            CraftingEssenceCost = new List<int> { 0, 0, 10, 0, 0 } // TEMP for MVP
        };

        Card fireEnergyCard = new Card
        {
            cardName = "Fire Crystal",
            description = "Gain 1 fire energy",
            cardType = CardType.Energy,
            energyCost = costToPlay,
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.GainEnergy, effectValue = 1, elementType = ElementType.Fire } },
            CraftingEssenceCost = new List<int> { 0, 0, 0, 0, 1 } // TEMP for MVP
        };

        Card earthEnergyCard = new Card
        {
            cardName = "Earth Crystal",
            description = "Gain 1 earth energy",
            cardType = CardType.Energy,
            energyCost = costToPlay,
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.GainEnergy, effectValue = 1, elementType = ElementType.Earth } },
            CraftingEssenceCost = new List<int> { 0, 0, 0, 0, 1 } // TEMP for MVP
        };

        Card waterEnergyCard = new Card
        {
            cardName = "Water Crystal",
            description = "Gain 1 water energy",
            cardType = CardType.Energy,
            energyCost = costToPlay,
            effects = new List<CardEffect> { new CardEffect { effectType = CardEffectType.GainEnergy, effectValue = 1, elementType = ElementType.Water } },
            CraftingEssenceCost = new List<int> { 0, 0, 0, 0, 1 } // TEMP for MVP
        };

        deck.Add(strikeCard);
        deck.Add(blockCard);
        deck.Add(fireballCard);
        deck.Add(earthShieldCard);
        deck.Add(blizzardCard);
        deck.Add(fireEnergyCard);
        deck.Add(earthEnergyCard);
        deck.Add(waterEnergyCard);
        Debug.Log($"Generated initial craftable cards with {deck.Count} cards.");
        return deck;
    }
}