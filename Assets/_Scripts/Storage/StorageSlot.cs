using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageSlot : MonoBehaviour
{ 

    public Image icon;

    ListItem lItem;

    public Text itemQuantity;

    Inventory inventory;

    public void AddItem(ListItem newItem)
    {
        lItem = newItem;
        icon.sprite = lItem.item.icon;
        icon.enabled = true;
        if (lItem.quantity > 1)
        {
            itemQuantity.text = "" + lItem.quantity;
        }
    }

    public void ClearSlot()
    {
        lItem = null;
        icon.sprite = null;
        icon.enabled = false;
        itemQuantity.text = "";
    }

    public void UseItem()
    { 
        if(lItem!=null)
        {
            if (inventory.Add(lItem.item))
            {
                if (lItem.quantity > 1)
                {
                    --lItem.quantity;
                    if (lItem.quantity > 1)
                    {
                        itemQuantity.text = "" + lItem.quantity;
                    }
                    else
                    {
                        itemQuantity.text = "";
                    }
                }
                else
                {
                    Storage.instance.Remove(lItem);
                }
            }
        }
    }

    public void SetInventory(Inventory pInventory)
    {
        inventory = pInventory;
    }
}
