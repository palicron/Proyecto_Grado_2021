using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ItemDescriptionUI : MonoBehaviour, IDragHandler
{
    public Canvas canvas;

    public GameObject itemDescUI;

    public RectTransform UI;

    public Image icon;

    public Text name;

    public Text description;

    public Text type;

    public Text rarity;

    private string lastItem = "";

    public void setItem(Item pItem)
    {
        icon.sprite = pItem.icon;

        name.text = pItem.name;

        description.text = pItem.description;

        type.text = "Type: " + pItem.type;

        rarity.text = "Rarity: " +  pItem.rarity;

        if (name.text.Equals(lastItem) && itemDescUI.activeSelf == true)
        {
            itemDescUI.SetActive(false);
        }
        else
        {
            itemDescUI.SetActive(true);
        }

        lastItem = name.text;
    }

    public void closeButton()
    {
        itemDescUI.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UI.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

}
