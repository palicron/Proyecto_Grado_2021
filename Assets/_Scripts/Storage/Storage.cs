using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{

    #region Singleton
    public static Storage instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one storage instance found");
        }
        instance = this;
    }
    #endregion

    public delegate void OnStorageChanged();

    public OnStorageChanged onStorageChangedCallBack;

    public int space = 20;

    public List<ListItem> items;

    public StorageType storageType;

    private static int totalScore;

    public virtual bool Add(Item item)
    {
        if(storageType != StorageType.Storage && (int) storageType != (int) item.type-1)
        {
            ErrorDialog.instance.ThrowError("Esta caneca no es la adecuada para el elemento que deseas reciclar.");
            return false;
        }
        bool added = false;
        if (items.Count >= space)
        {
            ErrorDialog.instance.ThrowError("El contenedor est� lleno.");
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
        totalScore++;
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

    public int GetTotalScore()
    {
        return totalScore;
    }

    public void SetContent(ref List<ListItem> pItems, StorageType pType)
    {
        items = pItems;
        storageType = pType;
        if (onStorageChangedCallBack != null)
        {
            onStorageChangedCallBack.Invoke();
        }
    }
}
