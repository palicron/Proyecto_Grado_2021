using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialContainer : MonoBehaviour
{
    public Image icon;

    public Text name;

    public Text description;

    public Text type;

    public Text rarity;

    public Text extraInfo;

    public void SetPageUI(Item pItem)
    {
        if (pItem == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            icon.sprite = pItem.icon;
            name.text = pItem.name;
            description.text = pItem.description;
            type.text = "Tipo: "+pItem.type.ToString();
            rarity.text = "Rareza: "+pItem.rarity.ToString();
            extraInfo.text = pItem.extraInfo;

        }
    }
}
