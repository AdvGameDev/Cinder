using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;
using System.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI EssenceDisplayText;
    [SerializeField] private CraftingMenuDeckUI ActionDeckUI;
    [SerializeField] private CraftingMenuDeckUI EnergyDeckUI;
    // [SerializeField] private CraftingWindow CraftingWindowUI;
    [SerializeField] private GameObject DebugTools; // Temp
    private bool ShowDebugTools = false;

    [Header("Player stuff")]
    [SerializeField] private Deck PlayerEnergyDeck;
    [SerializeField] private Deck PlayerActionDeck;
    [SerializeField] private List<int> PlayerEssences;

    void Start()
    {
        DebugTools.SetActive(ShowDebugTools);

        ActionDeckUI.gameObject.SetActive(true);
        EnergyDeckUI.gameObject.SetActive(false);
        // CraftingWindowUI.gameObject.SetActive(false);

        PlayerEnergyDeck = new Deck();
        PlayerEnergyDeck.Initialize(InitialDeckData.GetInitialEnergyDeck());
        EnergyDeckUI?.Initialize(this, PlayerEnergyDeck);
        
        PlayerActionDeck = new Deck();
        PlayerActionDeck.Initialize(InitialDeckData.GetInitialPlayerDeck());
        ActionDeckUI?.Initialize(this, PlayerActionDeck);

        PlayerEssences = new List<int> { 100, 100, 100, 100, 100 };

        UpdateEssenceText();
        UpdateDeck();
    }

    private void UpdateEssenceText()
    {
        EssenceDisplayText.text = $"Essences:\nFire: {PlayerEssences[0]}\nEarth: {PlayerEssences[1]}\nWater: {PlayerEssences[2]}\nAir: {PlayerEssences[3]}\nGeneric: {PlayerEssences[4]}";
    }

    private void UpdateDeck()
    {
        EnergyDeckUI?.DisplayDeck();
        ActionDeckUI?.DisplayDeck();
    }

    public void UpdateDeckDisplay(int deckNum)
    {
        // deckNum = 0 if we are showing energy deck
        // deckNum = 1 if we are showing action deck
        if (deckNum == 0)
        {
            EnergyDeckUI.gameObject.SetActive(true);
            ActionDeckUI.gameObject.SetActive(false);
        }
        else if (deckNum == 1)
        {
            EnergyDeckUI.gameObject.SetActive(false);
            ActionDeckUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log($"Unknown deckNum {deckNum}");
        }
    }

    public void ToggleDebugTools()
    {
        ShowDebugTools = !ShowDebugTools;
        DebugTools.SetActive(ShowDebugTools);
    }

    private void Awake()
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

    private bool CanCraftCard(CraftingRecipeTemplate recipe)
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
        // Assert.IsNotNull(recipe);
        // if (!CanCraftCard(recipe))
        // {
        //     Debug.Log("Not enough essences to craft this card.");
        //     return;
        // }
        // for (int i = 0; i < PlayerEssences.Count; i++)
        // {
        //     PlayerEssences[i] -= recipe.CraftingEssenceCost[i];
        // }
        // PlayerDeck.AddCard(recipe.CraftingResult); // add card to player's deck
        // Debug.Log($"Crafted card: {recipe.CraftingResult.cardName}");
        // UpdateText();
    }

    public void AddEssence(int typeIndex, int amount)
    {
        Assert.IsTrue(typeIndex >= 0 && typeIndex < PlayerEssences.Count);
        PlayerEssences[typeIndex] += amount;
        Debug.Log($"Added {amount} essence of type {typeIndex}. New total: {PlayerEssences[typeIndex]}");
        UpdateEssenceText();
    }

    public void DestroyCard(int index)
    {
        // Assert.IsTrue(index >= -1 && index < PlayerDeck.cards.Count, $"Invalid Destroy operation at index {index}");
        // Assert.IsTrue(PlayerDeck.cards.Count > 0, $"Deck is already empty, no cards left to destroy");
        // if (index == -1) index = PlayerDeck.cards.Count - 1;
        // Debug.Log($"Card {PlayerDeck.cards[index].cardName} has been Destroyed");
        // PlayerDeck.cards.RemoveAt(index);
        // UpdateText();
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
        UpdateEssenceText();
    }
}
