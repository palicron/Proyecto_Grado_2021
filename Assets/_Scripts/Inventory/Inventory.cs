using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton
    public static Inventory instance;

    void Awake ()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one inventory instance found");
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallBack;

    public delegate void OnStorage();

    public OnStorage onStorageCallBack;

    public Storage storage;

    int space = 20;

    public List<ListItem> items = new List<ListItem>();

    public bool Add (Item item)
    {
        bool added = false;
        if(items.Count >= space)
        {
            Debug.Log("Inventory is full.");
            return false;
        }
        List<ListItem> resultItems = items.FindAll(pItem => pItem.item.name.Equals(item.name));
        if (resultItems.Count>=0)
        {
            for(int i = 0; i<resultItems.Count; i++)
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

        if(onItemChangedCallBack!=null)
        {
            onItemChangedCallBack.Invoke();
        }
        return true;
    }

    public void Remove (ListItem item)
    {
        items.Remove(item);
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }

    public void StorageDetected(Storage pStorage)
    {
        storage = pStorage;
        if (onStorageCallBack != null)
        {
            onStorageCallBack.Invoke();
        }
    }

    public void RecyclingDetected(Storage pStorage)
    {
        storage = pStorage;
        if (onStorageCallBack != null)
        {
            onStorageCallBack.Invoke();
        }
    }

    public void DebugInventory()
    {
        for(int  i = 0; i < items.Count; i++)
        {
            Debug.Log("Slot 1: " + items[i].item.name + " : " + items[i].quantity);
        }
    }
}
