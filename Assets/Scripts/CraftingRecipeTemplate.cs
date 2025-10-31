using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipeTemplate", menuName = "Scriptable Objects/CraftingRecipeTemplate")]
public class CraftingRecipeTemplate : ScriptableObject
{
    public string RecipeName;
    public int CraftingEssenceCost;
    public Card CraftingResult;
}
