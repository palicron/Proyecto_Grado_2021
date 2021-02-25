using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListItem
{
    public Item item;
    public int quantity;

    public ListItem(Item pItem)
    {
        item = pItem;
        quantity = 1;
    }
}
