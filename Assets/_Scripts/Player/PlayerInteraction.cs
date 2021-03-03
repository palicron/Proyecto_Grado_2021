using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    Storage storage;

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Storage")
        {
            storage = collider.GetComponent<Storage>();
            Inventory.instance.StorageDetected(storage);
        }
    }

    void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag == "Storage")
        {
            Inventory.instance.StorageDetected(null);
        }
    }
}
