using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI Refernences")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Image _artImage;

    private Card _cardData;
    private BattleManager _battleManager;

    private Vector3 _startPosition;
    private Transform _originalParent;
    private int _originalSiblingIndex;

    public void Initialize(Card card, BattleManager manager)
    {
        _cardData = card;
        _battleManager = manager;

        _nameText.text = _cardData.cardName;
        _descriptionText.text = _cardData.description;

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
        // Enlarge and bring to front on hover
        transform.localScale = Vector3.one * 1.1f;
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Return to normal size when mouse leaves
        transform.localScale = Vector3.one;
        transform.SetSiblingIndex(_originalSiblingIndex);
    }

    // --- Drag and Drop Logic ---

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Store original state
        _originalParent = transform.parent;
        _startPosition = transform.position;
        _originalSiblingIndex = transform.GetSiblingIndex();

        // Detach from the hand container so it can be dragged freely
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move card with the mouse
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Simple drag up to play for the MVP
        float dragUpToPlayThreshold = 150f;
        if (transform.position.y > _startPosition.y + dragUpToPlayThreshold)
        {
            bool success = _battleManager.AttemptToPlayCard(_cardData, _battleManager.Enemy);

            if (!success)
            {
                transform.SetParent(_originalParent);
                transform.position = _startPosition;
                transform.SetSiblingIndex(_originalSiblingIndex);
            }
        }
        else
        {
            // If not dragged enough, return the card to original place
            transform.SetParent(_originalParent);
            transform.position = _startPosition;
            transform.SetSiblingIndex(_originalSiblingIndex);
        }

        // Reset scale just in case
        transform.localScale = Vector3.one;
    }
}