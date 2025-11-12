using UnityEngine;

/// <summary>
/// Enemy that alternates between attacking and gaining block each turn.
/// </summary>
public class AlternatingEnemy : Enemy
{
    [Header("Alternating Actions")]
    [SerializeField] private int _alternatingAttackDamage = 7;
    [SerializeField] private int _blockGain = 3;
    
    private bool _shouldAttackThisTurn = true;

    public override void Setup(int startingHealth, int newMaxHealth)
    {
        base.Setup(startingHealth, newMaxHealth);
        _shouldAttackThisTurn = true; // Start with attack
        PrepareNextTurn();
    }

    public override void TakeTurn(Player target)
    {
        if (target == null || target.isDead) return;

        if (_shouldAttackThisTurn)
        {
            Debug.Log($"{characterName} attacks for {_alternatingAttackDamage} damage.");
            target.TakeDamage(_alternatingAttackDamage);
        }
        else
        {
            Debug.Log($"{characterName} gains {_blockGain} block.");
            GainBlock(_blockGain);
        }

        _shouldAttackThisTurn = !_shouldAttackThisTurn;
        PrepareNextTurn();
    }

    private void PrepareNextTurn()
    {
        if (_shouldAttackThisTurn)
        {
            _nextTurnIntent = $"Attack ({_alternatingAttackDamage})";
        }
        else
        {
            _nextTurnIntent = $"Block ({_blockGain})";
        }
    }
}
