using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] private TextMeshProUGUI _playerHealthText;
    [SerializeField] private TextMeshProUGUI _playerBlockText;
    [SerializeField] private TextMeshProUGUI _playerEnergyText;

    [Header("Enemy UI")]
    [SerializeField] private TextMeshProUGUI _enemyHealthText;
    [SerializeField] private TextMeshProUGUI _enemyBlockText;
    [SerializeField] private TextMeshProUGUI _enemyIntentText;

    [Header("Turn/State UI")]
    [SerializeField] private TextMeshProUGUI _announcementText;
    [SerializeField] private TextMeshProUGUI _drawCountText;
    [SerializeField] private Button _endTurnButton;

    [Header("Result Panels")]
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _defeatPanel;

    private BattleManager _battleManager;

    public void Initialize(BattleManager manager)
    {
        _battleManager = manager;

        // --- Subscribe to Player Events ---
        if (_battleManager.Player != null)
        {
            _battleManager.Player.OnHealthChanged += UpdatePlayerHealth;
            _battleManager.Player.OnBlockChanged += UpdatePlayerBlock;
            _battleManager.Player.OnEnergyChanged += UpdatePlayerEnergy;
        }

        // --- Subscribe to Enemy Events ---
        if (_battleManager.Enemy != null)
        {
            _battleManager.Enemy.OnHealthChanged += UpdateEnemyHealth;
            _battleManager.Enemy.OnBlockChanged += UpdateEnemyBlock;
        }

        // --- Button Listeners ---
        _endTurnButton?.onClick.AddListener(() => _battleManager.OnEndTurnPressed());

        // --- Initial State ---
        UpdatePlayerHealth(_battleManager.Player.currentHealth, _battleManager.Player.maxHealth);
        UpdatePlayerBlock(_battleManager.Player.currentBlock);
        UpdatePlayerEnergy(_battleManager.Player.CurrentEnergy);
        UpdateEnemyHealth(_battleManager.Enemy.currentHealth, _battleManager.Enemy.maxHealth);
        UpdateEnemyBlock(_battleManager.Enemy.currentBlock);
        _victoryPanel?.SetActive(false);
        _defeatPanel?.SetActive(false);
    }

    public void UpdateBattleStateText(string text)
    {
        _announcementText.text = text;
    }

    private void UpdatePlayerHealth(int current, int max)
    {
        _playerHealthText.text = $"HP: {current} / {max}";
    }

    private void UpdatePlayerBlock(int amount)
    {
        _playerBlockText.text = amount > 0 ? $"Block: {amount}" : "";
    }

    private void UpdatePlayerEnergy(Dictionary<ElementType, int> energy)
    {
        string energyString = string.Join(" | ", energy.Where(e => e.Value > 0).Select(e => $"{e.Key}: {e.Value}"));
        _playerEnergyText.text = string.IsNullOrEmpty(energyString) ? "Energy: 0" : energyString;
    }

    private void UpdateEnemyHealth(int current, int max)
    {
        _enemyHealthText.text = $"HP: {current} / {max}";
    }

    private void UpdateEnemyBlock(int amount)
    {
        if (_enemyBlockText != null)
        {
            _enemyBlockText.text = amount > 0 ? $"Block: {amount}" : "";
        }
    }

    public void OnTurnChanged()
    {
        if (_battleManager == null) return;

        _enemyIntentText.text = _battleManager.Enemy.GetIntent();
        _endTurnButton.interactable = (_battleManager.CurrentState == BattleManager.BattleState.PlayerTurn);
    }

    public void UpdateDrawCount(int remaining, int total)
    {
        _drawCountText.text = $"Draws: {remaining}/{total}";
    }

    public void ShowVictory()
    {
        _victoryPanel?.SetActive(true);
        _endTurnButton.interactable = false;
    }

    public void ShowDefeat()
    {
        _defeatPanel?.SetActive(true);
        _endTurnButton.interactable = false;
    }

    private void OnDestroy()
    {
        if (_battleManager != null)
        {
            if (_battleManager.Player != null)
            {
                _battleManager.Player.OnHealthChanged -= UpdatePlayerHealth;
                _battleManager.Player.OnBlockChanged -= UpdatePlayerBlock;
                _battleManager.Player.OnEnergyChanged -= UpdatePlayerEnergy;
            }
            if (_battleManager.Enemy != null)
            {
                _battleManager.Enemy.OnHealthChanged -= UpdateEnemyHealth;
                _battleManager.Enemy.OnBlockChanged -= UpdateEnemyBlock;
            }
            _endTurnButton?.onClick.RemoveAllListeners();
        }
    }
}
