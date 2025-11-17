using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Card
{
    public string Name;
    public string Description;
    public List<CardEffect> Effects;

    public CardTemplate Template;
    public string GUID;

    public Card(CardTemplate template)
    {
        Template = template;
        Name = Template.Name;
        Description = Template.Description;
        Effects = new List<CardEffect>();
        foreach (CardEffect effect in Template.Effects)
        {
            Effects.Add(effect);
        }
        GUID = System.Guid.NewGuid().ToString();
    }
}
