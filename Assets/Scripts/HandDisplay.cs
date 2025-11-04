using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandDisplay : MonoBehaviour
{
    public Hand hand;
    public GameObject cardPrefab;
    public Transform handContainer;
    public float spacing = 100f;

    private List<GameObject> displayedCards = new List<GameObject>();

    private void Update()
    {
        UpdateHandDisplay();
    }

    private void UpdateHandDisplay()
    {
        // Clear old displayed cards
        foreach (GameObject go in displayedCards)
        {
            Destroy(go);
        }
        displayedCards.Clear();

        // Display each card in hand left to right
        for (int i = 0; i < hand.cardsInHand.Count; i++)
        {
            Card card = hand.cardsInHand[i];
            GameObject cardGO = Instantiate(cardPrefab, handContainer);
            cardGO.GetComponentInChildren<Text>().text = card.name; // Assuming prefab has Text
            cardGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * spacing, 0);
            displayedCards.Add(cardGO);
        }
    }
}
