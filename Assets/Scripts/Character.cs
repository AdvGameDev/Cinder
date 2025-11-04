using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHealth;
    public int currentHealth;

    public Sprite characterSprite;
    public Sprite deadSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        spriteRenderer.sprite = characterSprite;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        spriteRenderer.sprite = deadSprite;
    }
}