using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Equipment[] currentEquipment;
    public int space = 9;
    public PlayerController playerController;

    InventoryManager inventory;

    public delegate void OnEquipmentChanged();
    public OnEquipmentChanged onEquipmentChangedCallback;

    void Start()
    {
        inventory = InventoryManager.instance;
        currentEquipment = new Equipment[space];
    }

    void Update()
    {
        if (Input.GetButtonDown("Unequip"))
        {
            Unequip(playerController.equipSlot - 1);
        }
    }

    public void Equip(Equipment newItem)
    {
        currentEquipment[newItem.equipSlot] = newItem;
        playerController.slotEquipped[newItem.equipSlot] = true;

        if (onEquipmentChangedCallback != null)
            onEquipmentChangedCallback.Invoke();
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            inventory.Add(currentEquipment[slotIndex]);

            currentEquipment[slotIndex] = null;
            playerController.slotEquipped[slotIndex] = false;

            if (onEquipmentChangedCallback != null)
                onEquipmentChangedCallback.Invoke();
        }
    }
}
