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
        if (_cardPrefab == null)
        {
            Debug.LogError("Card Prefab is not set in HandUI");
            return;
        }

        GameObject cardGO = Instantiate(_cardPrefab, _handContainer);
        CardUI cardUI = cardGO.GetComponent<CardUI>();

        if (cardUI != null)
        {
            cardUI.Initialize(card, _battleManager);

            _cardToUIMap[card] = cardUI;
            RepositionCards();
        }
        else
        {
            Debug.LogError("The assigned Card Prefab does not have a CardUI component on it");
        }
    }

    private void OnCardRemovedFromHand(Card card)
    {
        if (_cardToUIMap.TryGetValue(card, out CardUI cardUI))
        {
            Destroy(cardUI.gameObject);
            _cardToUIMap.Remove(card);
            RepositionCards();
        }
    }

    // To be fine-tuned with real cards
    private void RepositionCards()
    {
        float cardWidth = 100f;
        float spacing = 20f;
        var cardUIs = _cardToUIMap.Values.ToList();
        float totalWidth = (cardUIs.Count - 1) * (cardWidth + spacing) + cardWidth;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < cardUIs.Count; i++)
        {
            float xPos = startX + i * (cardWidth + spacing);
            cardUIs[i].transform.localPosition = new Vector3(xPos, 0, 0);
            cardUIs[i].transform.SetSiblingIndex(i);
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