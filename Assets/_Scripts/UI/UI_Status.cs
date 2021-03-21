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
    }
    #endregion

    public GameObject[] menus;

    public bool IsMenuOpen()
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if(menus[i].activeSelf)
            {
                Debug.Log("Can't attack with an opened menu");
                return true;
            }
        }
        return false;
    }
}
