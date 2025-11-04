using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages elemental essence currency
/// </summary>
public class CurrencyManager : MonoBehaviour
{
    private static CurrencyManager instance;
    public static CurrencyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CurrencyManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("CurrencyManager");
                    instance = go.AddComponent<CurrencyManager>();
                }
            }
            return instance;
        }
    }

    private Dictionary<ElementType, int> essence = new Dictionary<ElementType, int>();

    public event Action<Dictionary<ElementType, int>> OnEssenceChanged;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeEssence();
    }

    private void InitializeEssence()
    {
        essence.Clear();
        foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
        {
            essence[element] = 0;
        }
    }

    /// <summary>
    /// Add essence of a specific element
    /// </summary>
    public void AddEssence(ElementType element, int amount)
    {
        if (!essence.ContainsKey(element))
        {
            essence[element] = 0;
        }

        essence[element] += amount;
        Debug.Log($"Gained {amount} {element} essence. Total: {essence[element]}");
        
        OnEssenceChanged?.Invoke(essence);
    }

    /// <summary>
    /// Add multiple essences at once
    /// </summary>
    public void AddEssences(Dictionary<ElementType, int> rewards)
    {
        foreach (var reward in rewards)
        {
            AddEssence(reward.Key, reward.Value);
        }
    }
}
