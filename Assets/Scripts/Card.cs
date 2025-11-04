using System.Collections.Generic;

[System.Serializable]
public class Card
{
    [System.Serializable]
    public struct EssenceCost
    {
        public int fire;
        public int water;
        public int earth;

        public EssenceCost(int fire, int water, int earth)
        {
            this.fire = fire;
            this.water = water;
            this.earth = earth;
        }
    }

    public string name;
    public int damage;
    public int armor;
    public EssenceCost essenceCost;

    public Card(string name, int damage, int armor)
    {
        this.name = name;
        this.damage = damage;
        this.armor = armor;
        this.essenceCost = new EssenceCost(0, 1, 0);
    }
}
