using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CraftingRecipeTemplate", menuName = "Scriptable Objects/CraftingRecipeTemplate")]
public class CraftingRecipeTemplate : ScriptableObject
{
    public string RecipeName;
    public List<int> CraftingEssenceCost; // [0] = Fire, [1] = Earth, [2] = Water, [3] = Air
    public Card CraftingResult;
}
