using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventory;
    public Transform mainInventory;
    public Transform equipmentInventory;
    public Text goldAmountText;

    InventoryManager inventoryManager;
    EquipmentManager equipmentManager;
    InventorySlot[] inventorySlots;
    EquipmentSlot[] equipmentSlots;
    int gold;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = InventoryManager.instance;
        equipmentManager = EquipmentManager.instance;
        inventoryManager.onItemChangedCallback += UpdateInventory;
        equipmentManager.onEquipmentChangedCallback += UpdateEquipment;

        inventorySlots = mainInventory.GetComponentsInChildren<InventorySlot>();
        equipmentSlots = equipmentInventory.GetComponentsInChildren<EquipmentSlot>();
    }

    void Update()
    {
        //TAB
        if (Input.GetButtonDown("Inventory"))
        {
            inventory.gameObject.SetActive(!inventory.gameObject.activeSelf);
        }

        gold = FindObjectOfType<Player>().gold;
        goldAmountText.text = gold.ToString();
    }

    void UpdateInventory()
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventoryManager.items.Count)
                inventorySlots[i].AddItem(inventoryManager.items[i]);
            else
                inventorySlots[i].ClearItem();
        }
    }

    void UpdateEquipment()
    {
        for (int i = 1; i < equipmentSlots.Length; i++)
        {
            if (equipmentManager.currentEquipment[i] != null)
                equipmentSlots[i].AddEquipment(equipmentManager.currentEquipment[i]);
            else
                equipmentSlots[i].ClearEquipment();
        }
    }
}
