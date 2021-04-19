using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EquipmentButton : MonoBehaviour, IPointerClickHandler
{
    EquipmentSlot slot;

    void Start()
    {
        slot = gameObject.GetComponentInParent<EquipmentSlot>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        slot.UseItem(eventData);
    }
}
