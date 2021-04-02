using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Status : MonoBehaviour
{
    #region Singleton

    public static UI_Status instance;

    void Awake()
    {
        instance = this;
        isOpened = false;
        menus = new bool[System.Enum.GetNames(typeof(MenuType)).Length];
    }
    #endregion

    bool isOpened;
    bool[] menus;
    public delegate void OnMenusChanged();
    public OnMenusChanged onMenusChangedCallBack;

    public bool IsMenuOpen()
    {
        return isOpened;
    }

    public void SetOpen(bool p, MenuType i)
    {
        menus[(int)i] = p;
        UpdateBoolean();
        if (onMenusChangedCallBack != null)
        {
            onMenusChangedCallBack.Invoke();
        }
    }

    void UpdateBoolean()
    {
        isOpened = false;
        for (int i = 0; i<menus.Length;i++)
        {
            Debug.Log(((MenuType)i)+": "+menus[i]);
            isOpened = isOpened || menus[i];
            if(isOpened)
            {
                return;
            }
        }
    }
}

public enum MenuType
{
    Crafting,
    Equipment,
    Inventory,
    Description,
    Storage,
    MiniGame
}
