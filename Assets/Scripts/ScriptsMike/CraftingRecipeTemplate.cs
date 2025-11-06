using UnityEngine;
using System.Collections.Generic;
using System.Text;

[CreateAssetMenu(fileName = "CraftingRecipeTemplate", menuName = "Scriptable Objects/CraftingRecipeTemplate")]
public class CraftingRecipeTemplate : ScriptableObject
{
    public string RecipeName;
    public List<int> CraftingEssenceCost; // [0] = Fire, [1] = Earth, [2] = Water, [3] = Air, [4] = Generi
    public MikeCard CraftingResult;

    public CraftingRecipeTemplate(string recipeName, List<int> craftingEssenceCost, MikeCard craftingResult)
    {
        RecipeName = recipeName;
        CraftingEssenceCost = craftingEssenceCost;
        CraftingResult = craftingResult;
    }

    public override string ToString()
    {
        return $"Fire: {CraftingEssenceCost[0]}\nEarth: {CraftingEssenceCost[1]}\nWater: {CraftingEssenceCost[2]}\nAir: {CraftingEssenceCost[3]}\nGeneric: {CraftingEssenceCost[4]}";
    }
}
