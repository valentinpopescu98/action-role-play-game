using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    public Transform player;
    public GameObject sword;
    public GameObject pistol;
    public GameObject hpPotion;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearItem()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        if (item.name == "Sword")
            Instantiate(sword, player.position, sword.transform.rotation);
        else if (item.name == "Pistol")
            Instantiate(pistol, player.position, pistol.transform.rotation);
        else if (item.name == "Health Potion")
            Instantiate(hpPotion, player.position, hpPotion.transform.rotation);

        InventoryManager.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
