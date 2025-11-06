using System;

[System.Serializable]
public enum CardEffectType
{
    Damage,
    Block,
    GainEnergy
}

[System.Serializable]
public class CardEffect
{
    public CardEffectType effectType;
    public int effectValue;

    public ElementType elementType;
}
