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

    void Update()
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
        if (recipe.UsesNeutralEssence)
        {
            int totalNeeded = recipe.CraftingEssenceCost.Sum();
            int totalAvailable = PlayerEssences.Sum();
            return totalAvailable >= totalNeeded;
        }
        for (int i = 0; i < PlayerEssences.Count; i++)
        {
            if (PlayerEssences[i] < recipe.CraftingEssenceCost[i])
            {
                return false;
            }
        }
        return true;
    }

    // TODO: Crafting with neutral essences not implemented yet
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
    }

    public void AddEssenceAll(int amount)
    {
        for (int i = 0; i < PlayerEssences.Count; i++)
        {
            PlayerEssences[i] += amount;
            Debug.Log($"Added {amount} essence of type {i}. New total: {PlayerEssences[i]}");
        }
    }

    public void AddEssence(int typeIndex, int amount)
    {
        Assert.IsTrue(typeIndex >= 0 && typeIndex < PlayerEssences.Count);
        PlayerEssences[typeIndex] += amount;
        Debug.Log($"Added {amount} essence of type {typeIndex}. New total: {PlayerEssences[typeIndex]}");
    }

    public void DestroyCard(int index)
    {
        Assert.IsTrue(index >= 0 && index < PlayerDeck.Count, $"Invalid Destroy operation at index {index}");
        Debug.Log($"Card {PlayerDeck[index].Title} has been Destroyed");
        PlayerDeck.RemoveAt(index);
    }

    public void DestroyLastCard()
    {
        DestroyCard(PlayerDeck.Count - 1);
    }

    public void AddFireEssence(int amount)
    {
        AddEssence(0, amount);
    }

    public void AddEarthEssence(int amount)
    {
        AddEssence(1, amount);
    }

    public void AddWaterEssence(int amount)
    {
        AddEssence(2, amount);
    }

    public void AddAirEssence(int amount)
    {
        AddEssence(3, amount);
    }

    public void AddGenericEssence(int amount)
    {
        AddEssence(4, amount);
    }
}
