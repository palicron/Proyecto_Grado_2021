using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    void OnTriggerEnter(Collider collider)
    {

        if(collider.gameObject.tag == "Player")
        {
            bool pickedUp = Inventory.instance.Add(item);
            if(pickedUp)
            {
                Destroy(gameObject);
            }
        }
    }
}
