using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePuzzle : Storage
{

    #region Singleton
    public static Storage instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one storagePuzzle instance found");
            totalScore = 0;
        }
        instance = this;
    }
    #endregion

    private static int totalScore; 

    public override bool Add(Item item)
    {
        if (storageType != StorageType.Storage && (int)storageType != (int)item.type - 1)
        {
            ErrorDialog.instance.ThrowError("Esta caneca no es la adecuada para el elemento que deseas reciclar.");
            return false;
        }
        bool added = false;
        if (items.Count >= space)
        {
            ErrorDialog.instance.ThrowError("El contenedor está lleno.");
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

    public int GetTotalScore()
    {
        return totalScore;
    }

}
