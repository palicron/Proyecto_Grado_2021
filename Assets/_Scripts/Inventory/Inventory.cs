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

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add (Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("Inventory is full.");
            return false;
        }
        items.Add(item);

        if(onItemChangedCallBack!=null)
        {
            onItemChangedCallBack.Invoke();
        }
        return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item);
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }

}
