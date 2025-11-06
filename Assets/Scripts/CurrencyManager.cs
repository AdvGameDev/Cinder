using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    private Dictionary<ElementType, int> _essenceStorage = new Dictionary<ElementType, int>();

    public event Action<Dictionary<ElementType, int>> OnEssenceChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeEssence();
    }

    private void InitializeEssence()
    {
        _essenceStorage.Clear();
        foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
        {
            _essenceStorage[element] = 0;
        }
        OnEssenceChanged?.Invoke(new Dictionary<ElementType, int>(_essenceStorage));
    }

    public void AddEssence(ElementType element, int amount)
    {
        if (amount <= 0) return;

        if (!_essenceStorage.ContainsKey(element))
        {
            _essenceStorage[element] = 0;
        }

        _essenceStorage[element] += amount;
        OnEssenceChanged?.Invoke(new Dictionary<ElementType, int>(_essenceStorage));
    }

    public void AddEssences(Dictionary<ElementType, int> rewards)
    {
        foreach (var reward in rewards)
        {
            AddEssence(reward.Key, reward.Value);
        }
    }

    public int GetEssenceAmount(ElementType element)
    {
        return _essenceStorage.ContainsKey(element) ? _essenceStorage[element] : 0;
    }

    // --- Future Implementation Templates

    public bool TrySpendEssence(ElementType element, int amount)
    {
        // TODO: Implement spending logic
        return false;
    }

    public bool CanAffordCraft(Dictionary<ElementType, int> costs)
    {
        // TODO: Implement the check
        return false;
    }
}