using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine.Assertions;
using System.Dynamic;

[System.Serializable]
public class Deck : List<Card>
{
    public Deck() { }
    public string PrintDeckContent(int minLine = 0, int maxLine = int.MaxValue)
    {
        StringBuilder builder = new StringBuilder();
        int limit = Math.Min(maxLine, Count);
        Assert.IsTrue(minLine <= limit);
        for (int i = 0; i < limit; i++)
        {
            builder.AppendLine($"  {i}. {this[i].Title}");
        }
        return builder.ToString();
    }

    public void AddCard(Card card)
    {
        Assert.IsNotNull(card);
        this.Add(card);
    }

    public void FillDeckWithPlaceholderCards()
    {
        this.Clear();
        for (int i = 0; i < 30; i++)
        {
            CraftingRecipeTemplate craftingRecipeTemplate = ScriptableObject.CreateInstance<CraftingRecipeTemplate>();
            craftingRecipeTemplate.RecipeName = "Placeholder Recipe";
            craftingRecipeTemplate.CraftingEssenceCost = new List<int> { 10, 10, 10, 10 };
            craftingRecipeTemplate.UsesNeutralEssence = false;
            craftingRecipeTemplate.CraftingResult = null;
            Card card = new Card("Placeholder Card", "This is a placeholder card.", craftingRecipeTemplate);
            this.Add(card);
        }
    }
}
