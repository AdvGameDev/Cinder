using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    public event Action OnDeath;
    public event Action<int, int> OnHealthChanged; // (currentHealth, maxHealth)
    public event Action<int> OnBlockChanged;

    public string characterName;
    public int maxHealth;
    public int currentHealth;
    public int currentBlock;
    public bool isDead;

    public Sprite characterSprite;
    public Sprite deadSprite;

    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
    }

    public virtual void Setup(int startingHealth, int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = startingHealth;
        currentBlock = 0;
        isDead = false;
        spriteRenderer.sprite = characterSprite;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        OnBlockChanged?.Invoke(currentBlock);
    }

    public virtual void TakeDamage(int amount)
    {
        if (isDead) return;

        int damageToHealth = amount - currentBlock;
        currentBlock = Mathf.Max(0, currentBlock - amount);
        OnBlockChanged?.Invoke(currentBlock);

        if (damageToHealth > 0)
        {
            currentHealth -= damageToHealth;
            if (currentHealth < 0) currentHealth = 0;

            OnHealthChanged?.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public virtual void GainBlock(int amount)
    {
        if (amount <= 0) return;
        currentBlock += amount;
        OnBlockChanged?.Invoke(currentBlock);
    }

    public void ResetBlock()
    {
        currentBlock = 0;
        OnBlockChanged?.Invoke(currentBlock);
    }

    protected void Die()
    {
        if (isDead) return;

        isDead = true;
        spriteRenderer.sprite = deadSprite;
        OnDeath?.Invoke();
    }
}
