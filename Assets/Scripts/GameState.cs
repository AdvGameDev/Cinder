using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public struct GameState
{
    public int PlayerHealth;
    public Deck PlayerSpellsDeck;
    public Deck PlayerEnergyDeck;
    public ElementCollection Essences;
}
