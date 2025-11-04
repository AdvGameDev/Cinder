using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Enemy : Character, IPointerClickHandler
{
    public int damage = 5;

    public void OnPointerClick(PointerEventData eventData)
    {
        TakeDamage(damage);
    }
}