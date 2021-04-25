using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePuzzleInteraction : MonoBehaviour
{
    private bool playerClose = false;

    private bool playerInteracting = false;

    private bool chestOpened = false;

    GameObject storageUI;

    GameObject inventoryUI;

    public GameObject interactingText;

    Storage storage;

    public StorageType storageType;

    public List<ListItem> items = new List<ListItem>();

    void Start()
    {
        storage = GetComponentInParent<Storage>();
        storageUI = GameObject.Find("/PF_GameUI").transform.Find("Storage").gameObject;
        inventoryUI = GameObject.Find("/PF_GameUI").transform.Find("Inventory").gameObject;
    }
    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            playerClose = true;
            interactingText.GetComponent<Animator>().SetBool("isInteracting", true);
        }
    }

    void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            storageUI.SetActive(false);
            UI_Status.instance.SetOpen(false, MenuType.Storage);
            chestOpened = false;
            playerClose = false;
            playerInteracting = false;
            Inventory.instance.StorageDetected(null);
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
            if (!chestOpened)
            {
                Inventory.instance.StorageDetected(null);
            }
            else
            {
                Inventory.instance.StorageDetected(storage);
            }
            storage.SetContent(ref items, storageType);
            storageUI.SetActive(chestOpened);
            inventoryUI.SetActive(chestOpened);
            UI_Status.instance.SetOpen(chestOpened, MenuType.Inventory);
            UI_Status.instance.SetOpen(chestOpened, MenuType.Storage);
        }
    }

}
