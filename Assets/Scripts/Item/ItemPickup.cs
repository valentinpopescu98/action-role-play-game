using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    bool wasPickedUp = false;

    public override void Interact()
    {
        base.Interact();

        Pickup();
    }

    void Pickup()
    {
        wasPickedUp = InventoryManager.instance.Add(item);
        if (wasPickedUp)
            Destroy(gameObject);
    }
}
