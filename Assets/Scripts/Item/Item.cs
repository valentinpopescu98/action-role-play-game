using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item", order = 1)]

public class Item : ScriptableObject
{
    new public string name = "New MyScriptableObject";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {

    }

    public void RemoveFromInventory()
    {
        InventoryManager.instance.Remove(this);
    }
}
