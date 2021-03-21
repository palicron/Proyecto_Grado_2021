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

    public PlayerCtr playerCtr;

    Inventory inventory;

    int armorModifier = 0; 

    int damageModifier = 0;

    int velocityModifier = 0;

    bool weaponEquipped = false; 

    void Start()
    {
        currentEquipment = new Equipment[6];
        equipmentUI = GameObject.Find("Equipment");
        playerCtr = gameObject.GetComponent<PlayerCtr>();
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

        if (newItem != null)
        {
            if(newItem.slot == EquipSlot.Tool)
            {
                if (newItem.isWeapon)
                {
                    if (weaponEquipped)
                    {
                        ErrorDialog.instance.ThrowError("¡Ya tienes un arma equipada!");
                        return false;
                    }
                    playerCtr.SetWeapon(((Tool)newItem).toolEquippedView, true);
                    weaponEquipped = true;
                }
                else
                {
                    GameObject pfView = ((Tool)newItem).toolEquippedView;
                    if(pfView != null)
                    {
                        playerCtr.SetTool(pfView, true);
                    }
                }
            }
            currentEquipment[slotIndex] = newItem;
        }

        UpdateStats(true, newItem);
        
        if (onItemEquipedCallBack != null)
        {
            onItemEquipedCallBack.Invoke();
        }
        return true;
    }

    public void Unequip (int pSlot)
    {
        UpdateStats(false, currentEquipment[pSlot]);
        if(currentEquipment[pSlot].isWeapon)
        {
            playerCtr.SetWeapon(null, false);
            weaponEquipped = false;
        }
        currentEquipment[pSlot] = null;
        if (onItemEquipedCallBack != null)
        {
            onItemEquipedCallBack.Invoke();
        }
    }

    void UpdateStats(bool p, Equipment item)
    {
        int sign = p ? 1 : -1;
        armorModifier += sign * item.armorModifier;
        damageModifier += sign * item.damageModifier;
        velocityModifier += sign * item.velocityModifier;
        playerCtr.ModifyStats(armorModifier, damageModifier, velocityModifier);
    }

    bool ContainsEquipment(int pId)
    {
        for(int i = 0; i < currentEquipment.Length; i++)
        {
            if(currentEquipment[i].id == pId)
            {
                return true;
            }
        }
        return false;
    }
}
