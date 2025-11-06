using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    public event Action OnDeath;
    public event Action<int, int> OnHealthChanged; // (currentHealth, maxHealth)

    public string characterName;
    public int maxHealth;
    public int currentHealth;
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
        isDead = false;
        spriteRenderer.sprite = characterSprite;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public virtual void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        if (isDead) return;

        isDead = true;
        spriteRenderer.sprite = deadSprite;
        OnDeath?.Invoke();
    }
}
