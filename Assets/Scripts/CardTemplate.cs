using UnityEngine;
using System.Collections.Generic;

public class CardTemplate : ScriptableObject
{
    public string Name;
    public string Description;
    public List<CardEffect> Effects;
}