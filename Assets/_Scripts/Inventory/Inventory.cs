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

    int extraBagSpace = 10;

    bool extraBag1;

    bool extraBag2;

    public List<ListItem> items = new List<ListItem>();

    void Start()
    {
        int numExtraBags = PlayerPrefs.SetInt("numExtraBags", 0);
    }

    public bool Add (Item item)
    {
        int actualSpace = space;
        if(extraBag1)
        {
            space += extraBagSpace;
        }
        if(extraBag2)
        {
            space += extraBagSpace;
        }
        Debug.Log(actualSpace);
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

    public void DebugInventory()
    {
        for(int  i = 0; i < items.Count; i++)
        {
            Debug.Log("Slot 1: " + items[i].item.name + " : " + items[i].quantity);
        }
    }

    //Get The List of Items of the inventory
    public List<ListItem> getInventory()
    {
        return items;
    }

    public bool SetExtraBag(int i)
    {
        if(i == 4)
        {
            extraBag1 = true;
            return true;
        }
        if (i == 5)
        {
            extraBag1 = true;
            return true;
        }
        return false;
    }

    public bool RemoveExtraBag(int i)
    {
        int boolBag1 = extraBag1? extraBagSpace: 0;
        int boolBag2 = extraBag2 ? extraBagSpace : 0;
        if (i == 4 && (space + (extraBagSpace * boolBag2)) > items.Count )
        {
            extraBag1 = false;
            return true;
        }
        if (i == 5 && (space + (extraBagSpace * boolBag1)) > items.Count )
        {
            extraBag2 = false;
            return true;
        }
        return false;
    }

    public bool isThereExtraBag(int  i)
    {
        if(i==1)
        {
            return extraBag1;
        }
        else
        {
            return extraBag2;
        }
    }
}
