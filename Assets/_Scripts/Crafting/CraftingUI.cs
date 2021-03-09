using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    public GameObject craftingUI;

    public CraftingPage page1;

    public CraftingPage page2;

    public List<Recipe> recipes = new List<Recipe>();

    public int actualIndex;

    void Start()
    {
        actualIndex = 0;
        SetPages();
    }

    bool SetPages()
    {
        if(recipes.Count<=actualIndex)
        {
            return false;
        }
        page1.SetPageUI(recipes[actualIndex]);
        page2.SetPageUI(recipes.Count > actualIndex+1 ? recipes[actualIndex+1] : null);
        return true;
    }

    public void nextPage()
    {
        actualIndex += 2;
        if(!SetPages())
        {
            actualIndex += -2;
        }
    }

    public void previousPage()
    {
        actualIndex += -2;
        if (actualIndex<0 || !SetPages())
        {
            actualIndex += 2;
        }
    }

    public void CloseButton()
    {
        craftingUI.SetActive(false);
    }

    public void SetPlayerScore(PlayerScore pScore)
    {
        page1.SetPlayerScore(pScore);
        page2.SetPlayerScore(pScore);
    }
}
