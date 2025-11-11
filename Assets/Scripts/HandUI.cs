using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _handContainer;

    private BattleManager _battleManager;
    private Hand _playerHand;

    private Dictionary<Card, CardUI> _cardToUIMap = new Dictionary<Card, CardUI>();
    private bool _rebuildPending = false;

    public void Initialize(BattleManager manager)
    {
        _battleManager = manager;
        _playerHand = _battleManager.Player.hand;

        // Subscribe to hand events
        _playerHand.OnCardAdded += OnCardAddedToHand;
        _playerHand.OnCardRemoved += OnCardRemovedFromHand;
    }

    private void OnCardAddedToHand(Card card)
    {
        RebuildHand();
    }

    private void OnCardRemovedFromHand(Card card)
    {
        RebuildHand();
    }

    // Completely rebuild the hand UI from the hand state
    private void RebuildHand()
    {
        if (_cardPrefab == null)
        {
            Debug.LogError("Card Prefab is not set in HandUI");
            return;
        }

        // Only start coroutine if not already pending
        if (!_rebuildPending)
        {
            _rebuildPending = true;
            StartCoroutine(RebuildAtEndOfFrame());
        }
    }

    private System.Collections.IEnumerator RebuildAtEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        _rebuildPending = false;
        RebuildHandImmediate();
    }

    private void RebuildHandImmediate()
    {
        if (_handContainer == null || _playerHand == null || _playerHand.cardsInHand == null)
        {
            return;
        }

        // Destroy all existing card UI elements immediately
        while (_handContainer.childCount > 0)
        {
            Transform child = _handContainer.GetChild(0);
            if (child != null)
            {
                DestroyImmediate(child.gameObject);
            }
        }

        // Clear the dictionary
        _cardToUIMap.Clear();

        // Recreate all cards from hand order
        for (int i = 0; i < _playerHand.cardsInHand.Count; i++)
        {
            Card card = _playerHand.cardsInHand[i];

            if (card == null)
            {
                continue;
            }

            GameObject cardGO = Instantiate(_cardPrefab, _handContainer);
            CardUI cardUI = cardGO.GetComponent<CardUI>();

            if (cardUI != null)
            {
                cardUI.Initialize(card, _battleManager);
                _cardToUIMap[card] = cardUI;
            }
        }

        // Reposition all cards
        RepositionCards();
    }

    private void RepositionCards()
    {
        if (_handContainer == null || _handContainer.childCount == 0) return;

        float spacing = 20f;
        float cardWidth = 100f;

        // Get actual card width from first child
        if (_handContainer.childCount > 0)
        {
            RectTransform rectTransform = _handContainer.GetChild(0) as RectTransform;
            if (rectTransform != null)
            {
                cardWidth = rectTransform.rect.width;
            }
        }

        // Position cards centered horizontally
        int cardCount = _handContainer.childCount;
        float totalWidth = (cardCount - 1) * (cardWidth + spacing) + cardWidth;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < cardCount; i++)
        {
            Transform cardTransform = _handContainer.GetChild(i);
            CardUI cardUI = cardTransform.GetComponent<CardUI>();

            float xPos = startX + i * (cardWidth + spacing);
            cardTransform.localPosition = new Vector3(xPos, 0, 0);
            cardTransform.SetSiblingIndex(i);

            // Update the CardUI's stored sibling index so hover works correctly
            if (cardUI != null)
            {
                cardUI.UpdateSiblingIndex(i);
            }
        }
    }

    private void OnDestroy()
    {
        if (_playerHand != null)
        {
            _playerHand.OnCardAdded -= OnCardAddedToHand;
            _playerHand.OnCardRemoved -= OnCardRemovedFromHand;
        }
    }
}