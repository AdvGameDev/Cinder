using TMPro;
using UnityEngine;

public class CraftingWindow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CraftingRecipeTemplate template;
    public TextMeshProUGUI CraftingCostText;
    void Start()
    {
        CraftingCostText.text = $"Cost:\n{template}";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
