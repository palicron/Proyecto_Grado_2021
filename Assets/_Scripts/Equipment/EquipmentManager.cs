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

    bool callback = true;

    public EquipmentsList equipmentList;

    void Start()
    {
        //PlayerPrefsX.SetIntArray("Equipment", new int[0]);
        currentEquipment = new Equipment[6];
        equipmentUI = GameObject.Find("Equipment");
        playerCtr = gameObject.GetComponent<PlayerCtr>();
        SetEquipment();
    }

    IEnumerator Initialize()
    {
        yield return new WaitForSeconds(0.1F);
        if (onItemEquipedCallBack != null)
        {
            onItemEquipedCallBack.Invoke();
        }
    }

    void SetEquipment()
    {
        int[] savedEquipment = PlayerPrefsX.GetIntArray("Equipment");
        for (int i = 0; i < savedEquipment.Length; i++)
        {
            int index = savedEquipment[i];
            if(index!=-1)
            {
                Equip(equipmentList.equipments[savedEquipment[i]]);
                Debug.Log("Recuperando equipamiento: " + savedEquipment[i]);
            }
        }
        StartCoroutine(Initialize());
    }

    void OnDestroy()
    {
        int[] savedEquipment = new int[currentEquipment.Length];
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            if(currentEquipment[i]!=null)
            {
                savedEquipment[i] = currentEquipment[i].equipmentId;
            }
            else
            {
                savedEquipment[i] = -1;
            }
        }
        PlayerPrefsX.SetIntArray("Equipment", savedEquipment);
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
                if (((Tool)newItem).isWeapon)
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
            if (currentEquipment[slotIndex] != null)
            {
                callback = false;
                Unequip(slotIndex);
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
        if(currentEquipment[pSlot].slot == EquipSlot.Tool)
        {
            if (((Tool)currentEquipment[pSlot]).isWeapon)
            {
                playerCtr.SetWeapon(null, false);
                weaponEquipped = false;
            }
            else
            {
                playerCtr.SetTool(null, false);
            }
        }
        currentEquipment[pSlot] = null;
        if(!callback)
        {
            callback = true;
            return;
        }
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
