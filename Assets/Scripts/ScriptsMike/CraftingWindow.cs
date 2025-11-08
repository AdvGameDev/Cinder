using TMPro;
using UnityEngine;

public class CraftingWindow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _deckContainer;
    private CraftingManager _craftingManager;
    public CraftingRecipeTemplate template;
    public TextMeshProUGUI CraftingCostText;

    public void Initialize(CraftingManager manager)
    {
        _craftingManager = manager;
        
    }
    
    
}
