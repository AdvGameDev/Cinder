using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Battle UI controller - displays health, energy, and battle controls
/// </summary>
public class BattleUI : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI playerEnergyText;

    [Header("Enemy UI")]
    [SerializeField] private TextMeshProUGUI enemyHealthText;
    [SerializeField] private TextMeshProUGUI enemyIntentText;

    [Header("Buttons")]
    [SerializeField] private Button strikeButton;
    [SerializeField] private Button endTurnButton;

    [Header("Result UI")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;

    private BattleManager battleManager;

    /// <summary>
    /// Initialize the UI with battle manager reference
    /// </summary>
    public void Initialize(BattleManager manager)
    {
        battleManager = manager;

        // Setup button listeners
        if (strikeButton != null)
        {
            strikeButton.onClick.AddListener(OnStrikeButtonPressed);
        }

        if (endTurnButton != null)
        {
            endTurnButton.onClick.AddListener(OnEndTurnButtonPressed);
        }

        // Hide result panels
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (defeatPanel != null) defeatPanel.SetActive(false);

        // Subscribe to events
        if (battleManager.Player != null)
        {
            battleManager.Player.OnHealthChanged += OnPlayerHealthChanged;
            battleManager.Player.OnEnergyChanged += OnPlayerEnergyChanged;
        }

        if (battleManager.Enemy != null)
        {
            battleManager.Enemy.OnHealthChanged += OnEnemyHealthChanged;
        }

        UpdateUI();
    }

    /// <summary>
    /// Update all UI elements
    /// </summary>
    public void UpdateUI()
    {
        UpdatePlayerUI();
        UpdateEnemyUI();
        UpdateButtons();
    }

    private void UpdatePlayerUI()
    {
        if (battleManager.Player == null) return;

        // Update health
        if (playerHealthText != null)
        {
            playerHealthText.text = $"HP: {battleManager.Player.CurrentHealth}/{battleManager.Player.MaxHealth}";
        }

        // Update energy
        if (playerEnergyText != null)
        {
            var energy = battleManager.Player.CurrentEnergy;
            int neutralEnergy = energy.ContainsKey(ElementType.Neutral) ? energy[ElementType.Neutral] : 0;
            playerEnergyText.text = $"Energy: {neutralEnergy}";
        }
    }

    private void UpdateEnemyUI()
    {
        if (battleManager.Enemy == null) return;

        // Update health
        if (enemyHealthText != null)
        {
            enemyHealthText.text = $"Enemy HP: {battleManager.Enemy.CurrentHealth}/{battleManager.Enemy.MaxHealth}";
        }

        // Update intent
        if (enemyIntentText != null)
        {
            enemyIntentText.text = $"Intent: {battleManager.Enemy.GetIntent()}";
        }
    }

    private void UpdateButtons()
    {
        bool isPlayerTurn = battleManager.CurrentState == BattleManager.BattleState.PlayerTurn;

        if (strikeButton != null)
        {
            strikeButton.interactable = isPlayerTurn;
        }

        if (endTurnButton != null)
        {
            endTurnButton.interactable = isPlayerTurn;
        }
    }

    private void OnStrikeButtonPressed()
    {
        battleManager.PlayStrikeCard();
    }

    private void OnEndTurnButtonPressed()
    {
        battleManager.OnEndTurnPressed();
    }

    private void OnPlayerHealthChanged(int current, int max)
    {
        if (playerHealthText != null)
        {
            playerHealthText.text = $"HP: {current}/{max}";
        }
    }

    private void OnPlayerEnergyChanged(Dictionary<ElementType, int> energy)
    {
        if (playerEnergyText != null)
        {
            int neutralEnergy = energy.ContainsKey(ElementType.Neutral) ? energy[ElementType.Neutral] : 0;
            playerEnergyText.text = $"Energy: {neutralEnergy}";
        }
    }

    private void OnEnemyHealthChanged(int current, int max)
    {
        if (enemyHealthText != null)
        {
            enemyHealthText.text = $"Enemy HP: {current}/{max}";
        }
    }

    public void ShowVictory()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        UpdateButtons();
    }

    public void ShowDefeat()
    {
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true);
        }
        UpdateButtons();
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        if (battleManager != null)
        {
            if (battleManager.Player != null)
            {
                battleManager.Player.OnHealthChanged -= OnPlayerHealthChanged;
                battleManager.Player.OnEnergyChanged -= OnPlayerEnergyChanged;
            }

            if (battleManager.Enemy != null)
            {
                battleManager.Enemy.OnHealthChanged -= OnEnemyHealthChanged;
            }
        }

        // Remove button listeners
        if (strikeButton != null)
        {
            strikeButton.onClick.RemoveListener(OnStrikeButtonPressed);
        }

        if (endTurnButton != null)
        {
            endTurnButton.onClick.RemoveListener(OnEndTurnButtonPressed);
        }
    }
}
