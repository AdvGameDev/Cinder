using UnityEngine;

[System.Serializable]
public class Card
{
    public string Title;
    public string Description;
    public CraftingRecipeTemplate RecipeTemplate;

    public Card(string title, string description, CraftingRecipeTemplate recipeTemplate)
    {
        Title = title;
        Description = description;
        RecipeTemplate = recipeTemplate;
    }
}
