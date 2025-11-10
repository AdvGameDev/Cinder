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
    [SerializeField] private CraftingMenuCraftingUI CraftableCardsListUI;
    [SerializeField] private GameObject DebugTools; // Temp
    private bool ShowDebugTools = false;

    [Header("Player stuff")]
    [SerializeField] private Deck PlayerEnergyDeck;
    [SerializeField] private Deck PlayerActionDeck;
    [SerializeField] private Deck CraftableCardsList;
    [SerializeField] private List<int> PlayerEssences;

    void Start()
    {
        DebugTools.SetActive(ShowDebugTools);

        ActionDeckUI.gameObject.SetActive(true);
        EnergyDeckUI.gameObject.SetActive(false);
        CraftableCardsListUI.gameObject.SetActive(false);

        PlayerEnergyDeck = new Deck();
        PlayerEnergyDeck.Initialize(InitialDeckData.GetInitialEnergyDeck());
        EnergyDeckUI?.Initialize(this, PlayerEnergyDeck);
        
        PlayerActionDeck = new Deck();
        PlayerActionDeck.Initialize(InitialDeckData.GetInitialPlayerDeck());
        ActionDeckUI?.Initialize(this, PlayerActionDeck);

        CraftableCardsList = new Deck();
        CraftableCardsList.Initialize(InitialDeckData.GetInitialCraftableCards());
        CraftableCardsListUI?.Initialize(this, CraftableCardsList);

        PlayerEssences = new List<int> { 10, 10, 10, 10, 10 };

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
        CraftableCardsListUI?.DisplayDeck();
    }

    public void UpdateDeckDisplay(int deckNum)
    {
        if (deckNum == 0)
        {
            // deckNum = 0 if we are showing energy deck
            EnergyDeckUI.gameObject.SetActive(true);
            ActionDeckUI.gameObject.SetActive(false);
            CraftableCardsListUI.gameObject.SetActive(false);
        }
        else if (deckNum == 1)
        {
            // deckNum = 1 if we are showing action deck
            EnergyDeckUI.gameObject.SetActive(false);
            ActionDeckUI.gameObject.SetActive(true);
            CraftableCardsListUI.gameObject.SetActive(false);
        }
        else if (deckNum == 2)
        {
            // deckNum = 2 if we are showing the crafting menu
            EnergyDeckUI.gameObject.SetActive(false);
            ActionDeckUI.gameObject.SetActive(false);
            CraftableCardsListUI.gameObject.SetActive(true);
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

    private bool CanCraftCard(Card card)
    {
        Assert.IsNotNull(card);
        for (int i = 0; i < PlayerEssences.Count; i++)
        {
            if (PlayerEssences[i] < card.CraftingEssenceCost[i])
            {
                return false;
            }
        }
        return true;
    }

    public void CraftCard(Card card)
    {
        Assert.IsNotNull(card, "Cannot craft null");
        if (CanCraftCard(card))
        {
            Debug.Log($"Crafting {card.cardName}");
            for (int i = 0; i < PlayerEssences.Count; i++)
            {
                PlayerEssences[i] -= card.CraftingEssenceCost[i];
            }
            UpdateEssenceText();
            switch (card.cardType)
            {
                case CardType.Action:
                    PlayerActionDeck.AddCard(card);
                    ActionDeckUI.AddCardToDisplay(card);
                    break;
                case CardType.Energy:
                    PlayerEnergyDeck.AddCard(card);
                    EnergyDeckUI.AddCardToDisplay(card);
                    break;
            }
        }
        else
        {
            Debug.Log($"Not enough essence!");
        }
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
