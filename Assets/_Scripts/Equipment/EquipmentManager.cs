using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public Equipment[] currentEquipment;

    public delegate void OnItemEquiped();

    public OnItemEquiped onItemEquipedCallBack;

    public GameObject equipmentUI;

    void Start()
    {
        currentEquipment = new Equipment[6];
        equipmentUI = GameObject.Find("Equipment");
    }

    public bool Equip (Equipment newItem)
    {
        int slotIndex = (int)newItem.slot;
        if(slotIndex == 4 && currentEquipment[5] == null)
        {
            slotIndex++;
        }
        if (newItem == currentEquipment[slotIndex])
        {
            return false;
        }
        currentEquipment[slotIndex] = newItem;
        
        if (onItemEquipedCallBack != null)
        {
            onItemEquipedCallBack.Invoke();
        }
        return true;
    }

    public void Unequip (int pSlot)
    {
        currentEquipment[pSlot] = null;
        if (onItemEquipedCallBack != null)
        {
            onItemEquipedCallBack.Invoke();
        }
    }
}
