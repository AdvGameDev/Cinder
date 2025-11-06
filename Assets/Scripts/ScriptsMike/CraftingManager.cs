using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;
using System.Linq;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;
    public Deck PlayerDeck;
    public TextMeshProUGUI DeckDisplayText;
    public TextMeshProUGUI EssenceDisplayText;
    public List<int> PlayerEssences;

    void Start()
    {
        PlayerDeck = new Deck();
        PlayerDeck.FillDeckWithPlaceholderCards(); // Fill with placeholder cards for testing (Temp)
        PlayerEssences = new List<int> { 100, 100, 100, 100, 100 }; // Fill with some starting essences (Temp)
        DeckDisplayText.text = $"Current Deck:\n{PlayerDeck.PrintDeckContent()}";
        EssenceDisplayText.text = $"Essences:\nFire: {PlayerEssences[0]}\nEarth: {PlayerEssences[1]}\nWater: {PlayerEssences[2]}\nAir: {PlayerEssences[3]}\nGeneric: {PlayerEssences[4]}";
    }

    void UpdateText()
    {
        DeckDisplayText.text = $"Current Deck:\n{PlayerDeck.PrintDeckContent()}";
        EssenceDisplayText.text = $"Essences:\nFire: {PlayerEssences[0]}\nEarth: {PlayerEssences[1]}\nWater: {PlayerEssences[2]}\nAir: {PlayerEssences[3]}\nGeneric: {PlayerEssences[4]}";
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

    bool CanCraftCard(CraftingRecipeTemplate recipe)
    {
        Assert.IsNotNull(recipe);
        for (int i = 0; i < PlayerEssences.Count; i++)
        {
            if (PlayerEssences[i] < recipe.CraftingEssenceCost[i])
            {
                return false;
            }
        }
        return true;
    }

    public void CraftCard(CraftingRecipeTemplate recipe)
    {
        Assert.IsNotNull(recipe);
        if (!CanCraftCard(recipe))
        {
            Debug.Log("Not enough essences to craft this card.");
            return;
        }
        for (int i = 0; i < PlayerEssences.Count; i++)
        {
            PlayerEssences[i] -= recipe.CraftingEssenceCost[i];
        }
        PlayerDeck.AddCard(recipe.CraftingResult);
        Debug.Log($"Crafted card: {recipe.CraftingResult.Title}");
        UpdateText();
    }

    public void AddEssence(int typeIndex, int amount)
    {
        Assert.IsTrue(typeIndex >= 0 && typeIndex < PlayerEssences.Count);
        PlayerEssences[typeIndex] += amount;
        Debug.Log($"Added {amount} essence of type {typeIndex}. New total: {PlayerEssences[typeIndex]}");
        UpdateText();
    }

    public void DestroyCard(int index)
    {
        Assert.IsTrue(index >= -1 && index < PlayerDeck.Count, $"Invalid Destroy operation at index {index}");
        Assert.IsTrue(PlayerDeck.Count > 0, $"Deck is already empty, no cards left to destroy");
        if (index == -1) index = PlayerDeck.Count - 1;
        Debug.Log($"Card {PlayerDeck[index].Title} has been Destroyed");
        PlayerDeck.RemoveAt(index);
        UpdateText();
    }

    public void AddFireEssence(int amount) // For testing
    {
        AddEssence(0, amount);
    }

    public void AddEarthEssence(int amount) // For testing
    {
        AddEssence(1, amount);
    }

    public void AddWaterEssence(int amount) // For testing
    {
        AddEssence(2, amount);
    }

    public void AddAirEssence(int amount) // For testing
    {
        AddEssence(3, amount);
    }

    public void AddGenericEssence(int amount) // For testing
    {
        AddEssence(4, amount);
    }

    public void AddEssenceAll(int amount) // For testing
    {
        for (int i = 0; i < PlayerEssences.Count; i++)
        {
            PlayerEssences[i] += amount;
            Debug.Log($"Added {amount} essence of type {i}. New total: {PlayerEssences[i]}");
        }
        UpdateText();
    }
}
