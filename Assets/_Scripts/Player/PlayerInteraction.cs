using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    Storage storage;

    Inventory inventory;

    void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Storage")
        {
            storage = collider.GetComponent<Storage>();
            inventory.StorageDetected(storage);
            storage.SetInventory(inventory);
        }
    }

    void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag == "Storage")
        {
            inventory.StorageDetected(null);
        }
    }
}
