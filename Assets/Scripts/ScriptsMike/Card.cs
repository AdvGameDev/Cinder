using UnityEngine;

[System.Serializable]
public class Card
{
    public string Title;
    public string Description;

    public Card(string title, string description)
    {
        Title = title;
        Description = description;
    }
}
