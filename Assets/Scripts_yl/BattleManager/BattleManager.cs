using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main battle controller managing turn order, combat flow, and win/lose conditions
/// </summary>
public class BattleManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private BattleUI battleUI;

    [Header("Cards")]
    [SerializeField] private CardTemplate strikeCard;

    [Header("Rewards")]
    [SerializeField] private int fireEssenceReward = 10;
    [SerializeField] private int earthEssenceReward = 10;
    [SerializeField] private int airEssenceReward = 10;

    public enum BattleState
    {
        Inactive,
        PlayerTurn,
        EnemyTurn,
        Victory,
        Defeat
    }

    private BattleState currentState = BattleState.Inactive;

    public BattleState CurrentState => currentState;
    public Player Player => player;
    public Enemy Enemy => enemy;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference not set in BattleManager!");
        }

        if (enemy == null)
        {
            Debug.LogError("Enemy reference not set in BattleManager!");
        }

        if (strikeCard == null)
        {
            Debug.LogWarning("Strike card not assigned in BattleManager!");
        }

        SetupBattle();
    }

    /// <summary>
    /// Initialize the battle
    /// </summary>
    private void SetupBattle()
    {
        // Subscribe to death events
        if (player != null)
        {
            player.OnDeath += OnPlayerDeath;
        }

        if (enemy != null)
        {
            enemy.OnDeath += OnEnemyDeath;
        }

        // Setup UI
        if (battleUI != null)
        {
            battleUI.Initialize(this);
        }

        // Start the battle
        StartBattle();
    }

    /// <summary>
    /// Start a new battle
    /// </summary>
    public void StartBattle()
    {
        Debug.Log("=== Battle Start ===");
        
        player?.ResetCombatant();
        enemy?.ResetCombatant();

        StartPlayerTurn();
    }

    /// <summary>
    /// Start the player's turn
    /// </summary>
    private void StartPlayerTurn()
    {
        currentState = BattleState.PlayerTurn;
        Debug.Log("--- Player Turn ---");

        player?.StartTurn();

        if (battleUI != null)
        {
            battleUI.UpdateUI();
        }
    }

    /// <summary>
    /// Called when player presses End Turn button
    /// </summary>
    public void OnEndTurnPressed()
    {
        if (currentState != BattleState.PlayerTurn)
        {
            Debug.LogWarning("Cannot end turn - not player's turn!");
            return;
        }

        player?.EndTurn();
        StartCoroutine(EnemyTurnSequence());
    }

    /// <summary>
    /// Execute enemy turn
    /// </summary>
    private IEnumerator EnemyTurnSequence()
    {
        currentState = BattleState.EnemyTurn;
        Debug.Log("--- Enemy Turn ---");

        yield return new WaitForSeconds(0.5f);

        enemy?.TakeTurn(player);

        if (battleUI != null)
        {
            battleUI.UpdateUI();
        }

        yield return new WaitForSeconds(1.0f);

        // Check if player died from enemy attack
        if (player != null && !player.IsDead)
        {
            StartPlayerTurn();
        }
    }

    /// <summary>
    /// Player plays the Strike card
    /// </summary>
    public void PlayStrikeCard()
    {
        if (currentState != BattleState.PlayerTurn)
        {
            Debug.LogWarning("Cannot play card - not player's turn!");
            return;
        }

        if (strikeCard == null)
        {
            Debug.LogError("Strike card not assigned!");
            return;
        }

        bool success = player.TryPlayCard(strikeCard, enemy);
        
        if (success && battleUI != null)
        {
            battleUI.UpdateUI();
        }
    }

    /// <summary>
    /// Called when player dies
    /// </summary>
    private void OnPlayerDeath()
    {
        currentState = BattleState.Defeat;
        Debug.Log("=== DEFEAT ===");
        
        if (battleUI != null)
        {
            battleUI.ShowDefeat();
        }
    }

    /// <summary>
    /// Called when enemy dies
    /// </summary>
    private void OnEnemyDeath()
    {
        currentState = BattleState.Victory;
        Debug.Log("=== VICTORY ===");
        
        GiveRewards();

        if (battleUI != null)
        {
            battleUI.ShowVictory();
        }
    }

    /// <summary>
    /// Give rewards to the player
    /// </summary>
    private void GiveRewards()
    {
        var rewards = new Dictionary<ElementType, int>
        {
            { ElementType.Fire, fireEssenceReward },
            { ElementType.Earth, earthEssenceReward },
            { ElementType.Air, airEssenceReward }
        };

        CurrencyManager.Instance.AddEssences(rewards);
        
        Debug.Log($"Rewards: {fireEssenceReward} Fire, {earthEssenceReward} Earth, {airEssenceReward} Air essence");
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        if (player != null)
        {
            player.OnDeath -= OnPlayerDeath;
        }

        if (enemy != null)
        {
            enemy.OnDeath -= OnEnemyDeath;
        }
    }
}
