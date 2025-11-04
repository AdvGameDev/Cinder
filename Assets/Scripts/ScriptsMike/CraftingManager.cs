using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;
    public Deck PlayerDeck;
    public TextMeshProUGUI DeckDisplayText;
    public List<int> PlayerEssences;

    void Start()
    {
        PlayerDeck = new Deck();
        PlayerDeck.FillDeckWithPlaceholderCards(); // Fill with placeholder cards for testing
        PlayerEssences = new List<int> { 100, 100, 100 };
        Debug.Log(PlayerDeck.PrintDeckContent(0, 5));
        DeckDisplayText.text = PlayerDeck.PrintDeckContent(0, 10);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void CraftCard(CraftingRecipeTemplate recipe)
    {
        Assert.IsNotNull(recipe);
        for (int i = 0; i < PlayerEssences.Count; i++)
        {
            Assert.IsTrue(PlayerEssences[i] >= recipe.CraftingEssenceCost[i], $"Not enough essence of type {i} to craft {recipe.RecipeName}");
            PlayerEssences[i] -= recipe.CraftingEssenceCost[i];
        }
        PlayerDeck.AddCard(new Card(recipe.CraftingResult.Title, recipe.CraftingResult.Description, recipe.CraftingResult.RecipeTemplate));
        Debug.Log($"Crafted card: {recipe.CraftingResult.Title}");
    }

    public void AddEssence(int type, int amount)
    {
        Assert.IsTrue(type >= 0 && type < PlayerEssences.Count, "Invalid essence type");
        PlayerEssences[type] += amount;
        Debug.Log($"Added {amount} essence of type {type}. New total: {PlayerEssences[type]}");
    }
}
