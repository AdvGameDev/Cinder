using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CraftingCraftViewCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Refernences")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Image _artImage;
    [SerializeField] private TextMeshProUGUI _craftingCostText;
    [SerializeField] private Image _craftingCostBackground;
    private Card _cardData;
    private CraftingManager _craftingManager;
    private Vector3 _startPosition;
    private Transform _originalParent;
    private int _originalSiblingIndex;

    public void Initialize(Card card, CraftingManager manager)
    {
        _cardData = card;
        _craftingManager = manager;

        _nameText.text = _cardData.cardName;
        _descriptionText.text = _cardData.description;

        _craftingCostText.text = $"Fire: {card.CraftingEssenceCost[0]}\nEarth: {card.CraftingEssenceCost[1]}\nWater: {card.CraftingEssenceCost[2]}\nAir: {card.CraftingEssenceCost[3]}\nGeneric: {card.CraftingEssenceCost[4]}\n";
        _craftingCostText.gameObject.SetActive(false);
        _craftingCostBackground.gameObject.SetActive(false);

        var costParts = new List<string>();

        if (_cardData.energyCost.genericCost > 0)
        {
            costParts.Add(_cardData.energyCost.genericCost.ToString());
        }

        if (_cardData.energyCost.specificCosts != null)
        {
            foreach (var specificCosts in _cardData.energyCost.specificCosts)
            {
                costParts.Add($"{specificCosts.amount} {specificCosts.elementType}");
            }
        }

        if (costParts.Count > 0)
        {
            _costText.text = string.Join("+", costParts);
        }
        else
        {
            _costText.text = "0";
        }
    }

    // --- Mouse Hover Effects ---

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.1f;
        _craftingCostBackground.gameObject.SetActive(true);
        _craftingCostText.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        _craftingCostBackground.gameObject.SetActive(false);
        _craftingCostText.gameObject.SetActive(false);
    }
}