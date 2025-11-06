using UnityEngine;

public class Enemy : Character
{
    [Header("Enemy Stats")]
    [SerializeField] private int _attackDamage = 5;
    // Add more actions later

    private string _nextTurnIntent;

    public override void Setup(int startingHealth, int newMaxHealth)
    {
        base.Setup(startingHealth, newMaxHealth);
        PrepareNextTurn();
    }

    public void TakeTurn(Player target)
    {
        if (target == null || target.isDead) return;

        // For now the only action is to attack
        Debug.Log($"Enemy attacks for {_attackDamage} damage.");
        target.TakeDamage(_attackDamage);

        PrepareNextTurn();
    }

    private void PrepareNextTurn()
    {
        _nextTurnIntent = $"Attack ({_attackDamage})";
    }

    public string GetIntent()
    {
        return _nextTurnIntent;
    }
}
