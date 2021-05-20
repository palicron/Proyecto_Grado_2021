using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Tool")]
public class Tool : Equipment
{
    public bool isWeapon;

    public void Reset()
    {
        type = ItemType.Equipment;
    }
}