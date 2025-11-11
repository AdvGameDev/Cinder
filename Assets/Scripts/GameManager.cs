using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// Singleton that manages the persistent game state between battles
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("Player Stats")]
    public int playerMaxHealth = 25;
    public int playerCurrentHealth;

    [Header("Player Decks")]
    public Deck cardDeck;
    public Deck energyDeck;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        InitializeGame();
    }

    private void InitializeGame()
    {
        playerCurrentHealth = playerMaxHealth;
        cardDeck = new Deck();
        cardDeck.Initialize(InitialDeckData.GetInitialPlayerDeck());
        energyDeck = new Deck();
        energyDeck.Initialize(InitialDeckData.GetInitialEnergyDeck());
    }

    public void StartBattle(BattleManager battleManager)
    {
        Debug.Log("GameManager: Preparing to start a battle.");

        if (battleManager == null)
        {
            Debug.LogError("BattleManager reference is null. Cannot start battle");
            return;
        }

        battleManager.SetupBattle(playerCurrentHealth, new List<Card>(cardDeck.cards), new List<Card>(energyDeck.cards));

        Debug.Log("GameManager: Data has been passed to BattleManager.");
    }

    public void EndBattle(int finalPlayerHealth, List<Card> finalActionDeckState, List<Card> finalEnergyDeckState)
    {
        Debug.Log("GameManager: Processing battle results.");
        playerCurrentHealth = finalPlayerHealth;

        if (finalActionDeckState != null)
        {
            cardDeck.Initialize(finalActionDeckState);
            Debug.Log($"Action deck updated. New count: {cardDeck.CardCount}");
        }

        if (finalEnergyDeckState != null)
        {
            cardDeck.Initialize(finalEnergyDeckState);
            Debug.Log($"Energy deck updated. New count: {energyDeck.CardCount}");
        }

        if (playerCurrentHealth <= 0)
        {
            Debug.Log("Game Over!");
            return;
        }
        SceneManager.LoadScene("Scenes/Crafting");
    }
}
