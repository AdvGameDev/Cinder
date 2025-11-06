using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main battle controller managing turn order, combat flow, and win/lose conditions
public class BattleManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private BattleUI _battleUI;
    [SerializeField] private DeckUI _actionDeckUI;
    [SerializeField] private DeckUI _energyDeckUI;
    [SerializeField] private HandUI _handUI;

    [Header("Rewards")]
    [SerializeField] private int _fireEssenceReward = 10;
    [SerializeField] private int _earthEssenceReward = 10;
    [SerializeField] private int _waterEssenceReward = 10;

    public enum BattleState
    {
        Inactive,
        DrawingPhase,
        PlayerTurn,
        EnemyTurn,
        Victory,
        Defeat
    }

    private BattleState _currentState = BattleState.Inactive;

    public BattleState CurrentState => _currentState;
    public Player Player => _player;
    public Enemy Enemy => _enemy;

    // --- Battle Lifecycle ---

    public void SetupBattle(int currentPlayerHealth, List<Card> playerActionDeck, List<Card> playerEnergyDeck)
    {
        Debug.Log("BattleManager: Setting up battle...");

        _player.Setup(currentPlayerHealth, Player.maxHealth, playerActionDeck, playerEnergyDeck);
        _enemy.Setup(_enemy.maxHealth, _enemy.maxHealth);

        _actionDeckUI?.Initialize(this);
        _energyDeckUI?.Initialize(this);
        _handUI?.Initialize(this);

        _actionDeckUI?.UpdateCardCount(playerActionDeck.Count);
        _energyDeckUI?.UpdateCardCount(playerEnergyDeck.Count);

        _player.OnDeath += OnPlayerDeath;
        _enemy.OnDeath += OnEnemyDeath;

        _battleUI?.Initialize(this);

        StartPlayerTurn();
    }

    private void OnDisable()
    {
        if (_player != null) _player.OnDeath -= OnPlayerDeath;
        if (_enemy != null) _enemy.OnDeath -= OnEnemyDeath;
    }

    private void StartPlayerTurn()
    {
        _currentState = BattleState.DrawingPhase;
        Debug.Log("--- Player Turn: Drawing Phase ---");
        _battleUI?.UpdateBattleStateText("DRAW PHASE");
        _player?.StartTurn();
        _battleUI?.UpdateDrawCount(_player.drawsRemaining, _player.drawsPerTurn);
        _battleUI.OnTurnChanged();
    }

    public void RequestDrawFromDeck(CardType deckType)
    {
        if (_currentState != BattleState.DrawingPhase)
        {
            Debug.Log("Cannot draw: Not in the Drawing Phase");
            return;
        }

        if (_player.drawsRemaining > 0)
        {
            _player.DrawCard(deckType);

            if (deckType == CardType.Action)
            {
                _actionDeckUI.UpdateCardCount(_player.actionDrawPile.CardCount);
            }
            else
            {
                _energyDeckUI.UpdateCardCount(_player.energyDrawPile.CardCount);
            }

            _battleUI?.UpdateDrawCount(_player.drawsRemaining, _player.drawsPerTurn);

            if (_player.drawsRemaining == 0)
            {
                _currentState = BattleState.PlayerTurn;
                _battleUI?.UpdateBattleStateText("YOUR TURN");
                Debug.Log("Drawing phase complete. Main turn begins");
                _battleUI?.OnTurnChanged();
            }
        }
        else
        {
            Debug.Log("No draws remaining");
        }
    }

    public void OnEndTurnPressed()
    {
        if (_currentState != BattleState.PlayerTurn) return;
        _player?.EndTurn();
        StartCoroutine(EnemyTurnSequence());
    }

    private IEnumerator EnemyTurnSequence()
    {
        _currentState = BattleState.EnemyTurn;
        _battleUI?.UpdateBattleStateText("ENEMY TURN");
        Debug.Log("--- Enemy Turn ---");
        yield return new WaitForSeconds(0.5f);

        _enemy?.TakeTurn(_player);
        _battleUI?.OnTurnChanged();

        yield return new WaitForSeconds(1.0f);

        if (_currentState != BattleState.Defeat && _currentState != BattleState.Victory)
        {
            StartPlayerTurn();
        }
    }

    public bool AttemptToPlayCard(Card card, Character target)
    {
        if (_currentState != BattleState.PlayerTurn || card == null) return false;

        if (_player.TrySpendEnergy(card.energyCost))
        {
            ExecuteCardEffects(card, target);
            _player.PlayCard(card);
            return true;
        }
        else
        {
            Debug.LogWarning($"Cannot afford to play {card.cardName}");
            return false;
        }
    }

    private void ExecuteCardEffects(Card card, Character target)
    {
        Debug.Log($"Executing effects for card: {card.cardName}");
        foreach (CardEffect effect in card.effects)
        {
            switch (effect.effectType)
            {
                case CardEffectType.Damage:
                    if (target != null) target.TakeDamage(effect.effectValue);
                    break;
                case CardEffectType.Block:
                    _player.GainBlock(effect.effectValue);
                    break;
                case CardEffectType.GainEnergy:
                    _player.GainEnergy(effect.elementType, effect.effectValue);
                    break;
            }
        }
    }

    // --- Battle End Conditions ---

    private void OnPlayerDeath()
    {
        if (_currentState == BattleState.Defeat) return;
        _currentState = BattleState.Defeat;
        Debug.Log("=== DEFEAT ===");
        _battleUI?.UpdateBattleStateText("YOU DIED");
        GameManager.Instance.EndBattle(0, new List<Card>(), new List<Card>());

        _battleUI?.ShowDefeat();
    }

    private void OnEnemyDeath()
    {
        if (_currentState == BattleState.Victory) return;
        _currentState = BattleState.Victory;
        Debug.Log("=== VICTORY ===");
        _battleUI?.UpdateBattleStateText("VICTORY!");
        List<Card> finalActionDeck = _player.GetAllRemainingActionCards();
        List<Card> finalEnergyDeck = _player.GetAllRemainingEnergyCards();

        GameManager.Instance.EndBattle(_player.currentHealth, finalActionDeck, finalEnergyDeck);

        GiveEssenceRewards();
        _battleUI?.ShowVictory();
    }

    private void GiveEssenceRewards()
    {
        Dictionary<ElementType, int> rewards = new Dictionary<ElementType, int>
        {
            { ElementType.Fire, _fireEssenceReward },
            { ElementType.Earth, _earthEssenceReward },
            { ElementType.Water, _waterEssenceReward }
        };
        CurrencyManager.Instance.AddEssences(rewards);
    }
}
