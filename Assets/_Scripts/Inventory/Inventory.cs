using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListItem
{
    public Item item;
    public int quantity;

    public ListItem(Item pItem)
    {
        item = pItem;
        quantity = 1;
    }
}
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

    public int space = 20;

    public List<ListItem> items = new List<ListItem>();

    public bool Add (Item item)
    {
        bool added = false;
        if(items.Count >= space)
        {
            Debug.Log("Inventory is full.");
            return false;
        }
        int index = items.FindLastIndex(pItem => pItem.item.name.Equals(item.name));
        if (index!=-1)
        {
            ListItem actItem = items[index];
            if(actItem.quantity<actItem.item.maxStack)
            {
                items[index].quantity++;
                added = true;
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

}
