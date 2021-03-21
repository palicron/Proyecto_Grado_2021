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

    public delegate void OnPocketAdded();

    public OnPocketAdded onPocketAddedCallback;

    public Storage storage;

    int space = 8;

    int extraBagSpace = 4;

    bool extraBag1;

    bool extraBag2;

    public List<ListItem> items = new List<ListItem>();

    void Start()
    {
        //PlayerPrefs.SetInt("numExtraBags", 0);
        int numExtraBags = PlayerPrefs.GetInt("numExtraBags");
        if(numExtraBags>=1)
        {
            extraBag1 = true;
            if(numExtraBags==2)
            {
                extraBag2 = true;
            }
        }
        if (onPocketAddedCallback != null)
        {
            onPocketAddedCallback.Invoke();
        }
    }

    private bool VerifyExtraBag()
    {
        if (extraBag1)
        {
            if (extraBag2)
            {
                return false;
            }
            PlayerPrefs.SetInt("numExtraBags", 2);
            extraBag2 = true;
            if (onPocketAddedCallback != null)
            {
                onPocketAddedCallback.Invoke();
            }
            return true;
        }
        PlayerPrefs.SetInt("numExtraBags", 1);
        extraBag1 = true;
        if (onPocketAddedCallback != null)
        {
            onPocketAddedCallback.Invoke();
        }
        return true;
    }

    public bool Add (Item item)
    {
        if(item.name == "Bolsillo de Mochila")
        {
            return VerifyExtraBag();
        }
        int actualSpace = space;
        if(extraBag1)
        {
            actualSpace += extraBagSpace;
        }
        if(extraBag2)
        {
            actualSpace += extraBagSpace;
        }
        bool added = false;
        List<ListItem> resultItems = items.FindAll(pItem => pItem.id == item.id);
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
            if(items.Count >= actualSpace)
            {
                ErrorDialog.instance.ThrowError("El inventario está lleno");
                return false;
            }
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
        if(item.quantity > 1)
        {
            item.quantity--;
        }
        else
        {
            items.Remove(item);
        }
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

    public bool ContainsItem(int pId, bool remove)
    {
        ListItem itemSearched = items.Find(pItem => pItem.id == pId);
        if(itemSearched != null)
        {
            if(remove)
            {
                Remove(itemSearched);
            }
            return true;
        }
        return false;
    }
}
