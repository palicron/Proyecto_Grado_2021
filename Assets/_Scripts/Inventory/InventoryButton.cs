using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    InventorySlot slot;

    void Start()
    {
        slot = gameObject.GetComponentInParent<InventorySlot>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        slot.UseItem(eventData);
    }
}
