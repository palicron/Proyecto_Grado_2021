using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipSlot slot;
    public int equipmentId;
    public int armorModifier;
    public int damageModifier;
    public int velocityModifier;
    public GameObject equippedView;

    public void Reset()
    {
        type = ItemType.Equipment;
    }

    public override bool Use()
    {
       return EquipmentManager.instance.Equip(this);
    }

}

public enum EquipSlot { Head, Chest, Legs, Feet, Tool }