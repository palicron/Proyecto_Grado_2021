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

    public GameObject nextArrow;

    public GameObject previousArrow;

    void Start()
    {
        actualIndex = 0;
        SetPages();
        CheckDisableButton();
    }

    bool SetPages()
    {
        if(recipes.Count<=actualIndex)
        {
            return false;
        }
        page1.SetPageUI(recipes[actualIndex]);
        page2.SetPageUI(recipes.Count > actualIndex+1 ? recipes[actualIndex+1] : null);
        UI_SFX.instance.PlayTurnPage();
        return true;
    }

    public void nextPage()
    {
        actualIndex += 2;
        if(!SetPages())
        {
            actualIndex += -2;
        }
        CheckDisableButton();
    }

    public void previousPage()
    {
        actualIndex += -2;
        if (actualIndex<0 || !SetPages())
        {
            actualIndex += 2;
        }
        CheckDisableButton();
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

    void CheckDisableButton()
    {
        if(actualIndex >= recipes.Count-2)
        {
            nextArrow.GetComponent<Button>().interactable = false;
        }
        else
        {
            nextArrow.GetComponent<Button>().interactable = true;
        }
        //
        if (actualIndex == 0)
        {
            previousArrow.GetComponent<Button>().interactable = false;
        }
        else
        {
            previousArrow.GetComponent<Button>().interactable = true;
        }

    }
}
