using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CraftingRecipeTemplate", menuName = "Scriptable Objects/CraftingRecipeTemplate")]
public class CraftingRecipeTemplate : ScriptableObject
{
    public string RecipeName;
    public List<int> CraftingEssenceCost; // [0] = Fire, [1] = Earth, [2] = Water, [3] = Air
    public bool UsesNeutralEssence; // If true, then any essence can be used to fulfill the cost
    public Card CraftingResult;

    public CraftingRecipeTemplate(string recipeName, List<int> craftingEssenceCost, Card craftingResult, bool usesNeutralEssence = false)
    {
        RecipeName = recipeName;
        UsesNeutralEssence = usesNeutralEssence;
        CraftingEssenceCost = craftingEssenceCost;
        CraftingResult = craftingResult;
    }
}
