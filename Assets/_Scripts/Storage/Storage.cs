using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage instance;

    void Awake()
    {
        instance = this;
    }

    public delegate void OnStorageChanged();

    public OnStorageChanged onStorageChangedCallBack;

    public delegate void OnInventory();

    public OnInventory onInventoryCallBack;

    public Inventory inventory;

    public int space = 40;

    public List<ListItem> items = new List<ListItem>();

    public bool Add(Item item)
    {
        bool added = false;
        if (items.Count >= space)
        {
            Debug.Log("Storage is full.");
            return false;
        }
        List<ListItem> resultItems = items.FindAll(pItem => pItem.item.name.Equals(item.name));
        if (resultItems.Count >= 0)
        {
            for (int i = 0; i < resultItems.Count; i++)
            {
                ListItem actItem = resultItems[i];
                if (actItem.quantity < actItem.item.maxStack)
                {
                    resultItems[i].quantity++;
                    added = true;
                    break;
                }
            }
        }
        if (!added)
        {
            items.Add(new ListItem(item));
        }

        if (onStorageChangedCallBack != null)
        {
            onStorageChangedCallBack.Invoke();
        }
        return true;
    }

    public void Remove(ListItem item)
    {
        items.Remove(item);
        if (onStorageChangedCallBack != null)
        {
            onStorageChangedCallBack.Invoke();
        }
    }

    public void SetInventory(Inventory pInventory)
    {
        inventory = pInventory;
        if (onInventoryCallBack != null)
        {
            onInventoryCallBack.Invoke();
        }
    }
}
