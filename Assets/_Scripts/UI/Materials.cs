using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Materials : MonoBehaviour
{
    public MaterialContainer page1;

    public MaterialContainer page2;

    public List<Item> items = new List<Item>();

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
        if (items.Count <= actualIndex)
        {
            return false;
        }
        page1.SetPageUI(items[actualIndex]);
        page2.SetPageUI(items.Count > actualIndex + 1 ? items[actualIndex + 1] : null);
        return true;
    }

    public void nextPage()
    {
        actualIndex += 2;
        if (!SetPages())
        {
            actualIndex += -2;
        }
        CheckDisableButton();
    }

    public void previousPage()
    {
        actualIndex += -2;
        if (actualIndex < 0 || !SetPages())
        {
            actualIndex += 2;
        }
        CheckDisableButton();
    }

    void CheckDisableButton()
    {
        if (actualIndex >= items.Count - 2)
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
