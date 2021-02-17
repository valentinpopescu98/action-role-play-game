using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Image icon;
    public int slotNumber;

    Equipment equipment;

    public void AddEquipment(Equipment newEquipment)
    {
        equipment = newEquipment;

        icon.sprite = equipment.icon;
        icon.enabled = true;
    }

    public void ClearEquipment()
    {
        equipment = null;

        icon.sprite = null;
        icon.enabled = false;
    }
}
