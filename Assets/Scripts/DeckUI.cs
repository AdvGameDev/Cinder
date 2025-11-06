using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DeckUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _cardCountText;
    [SerializeField] private CardType _deckType;

    private BattleManager _battleManager;

    public void Initialize(BattleManager manager)
    {
        _battleManager = manager;
    }

    public void UpdateCardCount(int count)
    {
        _cardCountText.text = count.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_battleManager == null) return;

        _battleManager.RequestDrawFromDeck(_deckType);
    }
}