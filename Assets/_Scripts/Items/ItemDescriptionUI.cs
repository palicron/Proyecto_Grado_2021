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

    public Text extraInfo;

    private int lastItem = -2;


    public void setItem(Item pItem)
    {
        int actualId = pItem.id;

        icon.sprite = pItem.icon;

        name.text = pItem.name;

        description.text = pItem.description;

        type.text = "Tipo: " + pItem.type;

        rarity.text = "Rareza: " + pItem.rarity;

        extraInfo.text = pItem.extraInfo;

        if (actualId == lastItem && itemDescUI.activeSelf == true)
        {
            itemDescUI.SetActive(false);
            UI_Status.instance.SetOpen(false, MenuType.Description);
        }
        else
        {
            itemDescUI.SetActive(true);
            UI_Status.instance.SetOpen(true, MenuType.Description);
        }

        lastItem = actualId;
    }

    public void closeButton()
    {
        itemDescUI.SetActive(false);
        UI_Status.instance.SetOpen(false, MenuType.Description);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UI.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

}
