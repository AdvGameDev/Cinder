using System;
using UnityEngine;

/// <summary>
/// Base class for all combat participants (Player and Enemy)
/// </summary>
public abstract class Combatant : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected int maxHealth = 100;
    protected int currentHealth;

    public event Action<int, int> OnHealthChanged; // current, max
    public event Action OnDeath;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Take damage from an attack
    /// </summary>
    public virtual void TakeDamage(int damage)
    {
        if (IsDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        Debug.Log($"{gameObject.name} took {damage} damage. HP: {currentHealth}/{maxHealth}");
        
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (IsDead)
        {
            Die();
        }
    }

    /// <summary>
    /// Heal this combatant
    /// </summary>
    public virtual void Heal(int amount)
    {
        if (IsDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        Debug.Log($"{gameObject.name} healed {amount}. HP: {currentHealth}/{maxHealth}");
        
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    /// <summary>
    /// Called when health reaches 0
    /// </summary>
    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} has died!");
        OnDeath?.Invoke();
    }

    /// <summary>
    /// Reset combatant to initial state
    /// </summary>
    public virtual void ResetCombatant()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
