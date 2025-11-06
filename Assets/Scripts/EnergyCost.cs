using System;
using System.Collections.Generic;

[System.Serializable]
public struct ElementCost
{
    public ElementType elementType;
    public int amount;

    public ElementCost(ElementType type, int val)
    {
        elementType = type;
        amount = val;
    }
}

[System.Serializable]
public class EnergyCost
{
    public int genericCost;
    public List<ElementCost> specificCosts;

    public EnergyCost(int generic, List<ElementCost> specifics)
    {
        genericCost = generic;
        specificCosts = specifics;
    }

    public EnergyCost()
    {
        specificCosts = new List<ElementCost>();
    }
}