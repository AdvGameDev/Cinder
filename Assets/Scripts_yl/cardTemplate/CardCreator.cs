using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Editor utility to create example cards
/// </summary>
public class CardCreator
{
    [MenuItem("Card System/Create Strike Card")]
    public static void CreateStrikeCard()
    {
        // Create the Strike card
        CardTemplate strikeCard = ScriptableObject.CreateInstance<CardTemplate>();
        strikeCard.cardName = "Strike";
        strikeCard.description = "Deal 6 damage.";

        // Set energy cost: 1 Neutral
        strikeCard.energyCost = new EnergyCost
        {
            genericCost = 0,
            specificCosts = new List<ElementCost>
            {
                new ElementCost(ElementType.Neutral, 1)
            }
        };

        // Add damage effect
        strikeCard.effects = new List<CardEffect>
        {
            new CardEffect(CardEffectType.Damage, 6)
        };

        // Save the asset
        string path = "Assets/Strike.asset";
        AssetDatabase.CreateAsset(strikeCard, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Strike card created at {path}");
        
        // Select the created asset
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = strikeCard;
    }
}
