using Mono.Cecil;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public enum ElementType
{
    Neutral = Constants.Neutral,
    Fire = Constants.Fire,
    Earth = Constants.Earth,
    Water = Constants.Water,
    Air = Constants.Air,
    Count // trick for getting number of types
}

[System.Serializable]
public struct ElementCost
{
    public int Generic;
    public ElementCollection Specific;

    public ElementCost(int generic, ElementCollection specific)
    {
        Generic = generic;
        Specific = specific;
    }
}

// Reusable Iterable Data Structure for Elemental Related Things
// e.g. Essences, Energy Cost, Current Energy, etc.
[System.Serializable]
public struct ElementCollection
{
    public int Neutral;
    public int Fire;
    public int Earth;
    public int Water;
    public int Air;

    public ElementCollection(int neutral, int fire, int earth, int water, int air)
    {
        Neutral = neutral;
        Fire = fire;
        Earth = earth;
        Water = water;
        Air = air;
    }

    public int TotalValue
    {
        get
        {
            int result = 0;
            for (int i = 0; i < (int) ElementType.Count; i++)
            {
                result += this[i];
            }
            return result;
        }
    }

    public bool CanAfford(ElementCost cost)
    {
        if (this < cost.Specific)
        {
            return false;
        }

        return (this - cost.Specific).TotalValue >= cost.Generic;
    }

    public void Expend(ElementCost cost)
    {
        Assert.IsTrue(CanAfford(cost));
    }

    // make field access iterable
    public int this[int index]
    {
        get
        {
            switch (index)
            {
                case Constants.Neutral: return Neutral;
                case Constants.Fire: return Fire;
                case Constants.Earth: return Earth;
                case Constants.Water: return Water;
                case Constants.Air: return Air;
                default: throw new System.IndexOutOfRangeException("Invalid EssenceType index!");
            }
        }
        set
        {
            switch (index)
            {
                case Constants.Neutral: Neutral = value; break;
                case Constants.Fire: Fire = value; break;
                case Constants.Earth: Earth = value; break;
                case Constants.Water: Water = value; break;
                case Constants.Air: Air = value; break;
                default: throw new System.IndexOutOfRangeException("Invalid EssenceType index!");
            }
        }
    }

    public int this[ElementType type]
    {
        set
        {
            this[(int) type] = value;
        }
        get
        {
            return this[(int) type];
        }
    }

    // overriding Equals() and GetHashCode() according to guidelines
    public override int GetHashCode()
        // Manually update this if new elements are added!
        => System.HashCode.Combine(Neutral, Fire, Earth, Water, Air);

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return this == (ElementCollection) obj;
    }

    // overloading operators (mostly boilerplate)
    public static ElementCollection operator +(ElementCollection lhs, ElementCollection rhs)
    {
        ElementCollection result = new ElementCollection();
        for (int i = 0; i < (int) ElementType.Count; i++)
        {
            result[i] = lhs[i] + rhs[i];
        }
        return result;
    }

    public static ElementCollection operator -(ElementCollection lhs, ElementCollection rhs)
    {
        ElementCollection result = new ElementCollection();
        for (int i = 0; i < (int) ElementType.Count; i++)
        {
            result[i] = lhs[i] - rhs[i];
        }
        return result;
    }

    public static bool operator ==(ElementCollection lhs, ElementCollection rhs)
    {
        for (int i = 0; i < (int) ElementType.Count; i++)
        {
            if (lhs[i] != rhs[i]) return false;
        }
        return true;
    }

    public static bool operator !=(ElementCollection lhs, ElementCollection rhs)
        => !(lhs == rhs);

    public static bool operator <(ElementCollection lhs, ElementCollection rhs)
    {
        for (int i = 0; i < (int) ElementType.Count; i++)
        {
            if (lhs[i] >= rhs[i]) return false;
        }
        return true;
    }

    public static bool operator >(ElementCollection lhs, ElementCollection rhs)
    {
        for (int i = 0; i < (int) ElementType.Count; i++)
        {
            if (lhs[i] <= rhs[i]) return false;
        }
        return true;
    }

    public static bool operator <=(ElementCollection lhs, ElementCollection rhs)
        => lhs < rhs || lhs == rhs;

    public static bool operator >=(ElementCollection lhs, ElementCollection rhs)
        => lhs > rhs || lhs == rhs;
}