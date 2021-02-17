using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item
{
    public int hpBoost;

    public override void Use()
    {
        base.Use();
        FindObjectOfType<Player>().Heal(hpBoost);
        RemoveFromInventory();
    }
}
