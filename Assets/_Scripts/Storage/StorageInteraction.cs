using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInteraction : MonoBehaviour
{
    private bool playerClose = false;

    private bool playerInteracting = false;

    private bool chestOpened = false;

    public GameObject chestLid;

    public GameObject storageUI;

    public GameObject inventoryUI;

    public GameObject interactingText;

    Storage storage;

    Inventory inventory;

    public List<ListItem> items = new List<ListItem>();

    void Awake()
    {
        storage = GetComponentInParent<Storage>();
    }
    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            inventory = collider.gameObject.GetComponent<Inventory>();
            playerClose = true;
            interactingText.GetComponent<Animator>().SetBool("isInteracting", true);
        }
    }

    void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            storageUI.SetActive(false);
            chestOpened = false;
            playerClose = false;
            playerInteracting = false;
            inventory.StorageDetected(null);
            GetComponent<Animator>().SetBool("isOpened", false);
            interactingText.GetComponent<Animator>().SetBool("isInteracting", false);
        }
    }

    void Update()
    {
        if (playerClose && Input.GetButtonDown("Interaction"))
        {
            chestOpened = !chestOpened;
            playerInteracting = chestOpened;
            GetComponent<Animator>().SetBool("isOpened", chestOpened);
            inventory.StorageDetected(storage);
            storage.SetInventory(inventory);
            storage.SetContent(ref items);
            storageUI.SetActive(chestOpened);
            inventoryUI.SetActive(chestOpened);
        }
    }
}
