using System;

[System.Serializable]
public enum CardEffectType
{
    Damage,
    Block,
    Move,
    Count // trick to see how many enums there are
}

[System.Serializable]
public struct CardEffect
{
    public CardEffectType effectType;
    public int effectValue;
}
