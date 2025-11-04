using UnityEngine;

/// <summary>
/// Enemy combatant with fixed attack pattern
/// </summary>
public class Enemy : Combatant
{
    [Header("Enemy Settings")]
    [SerializeField] private int attackDamage = 5;
    
    private int nextAttackDamage;

    public int NextAttackDamage => nextAttackDamage;

    protected override void Awake()
    {
        base.Awake();
        nextAttackDamage = attackDamage;
    }

    /// <summary>
    /// Enemy takes its turn - attacks the player
    /// </summary>
    public void TakeTurn(Combatant target)
    {
        if (IsDead || target == null) return;

        Debug.Log($"{gameObject.name} attacks for {nextAttackDamage} damage!");
        target.TakeDamage(nextAttackDamage);

        // Set next attack (for now it's always the same)
        nextAttackDamage = attackDamage;
    }

    /// <summary>
    /// Get the enemy's intent (what it will do next turn)
    /// </summary>
    public string GetIntent()
    {
        return $"Attack: {nextAttackDamage}";
    }

    public override void ResetCombatant()
    {
        base.ResetCombatant();
        nextAttackDamage = attackDamage;
    }
}
