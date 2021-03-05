using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StorageType { Storage, Paper, Plastic, Glass, Metal}

public class StorageInteraction : MonoBehaviour
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

    PlayerScore playerScore;

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
            playerScore = collider.gameObject.GetComponent<PlayerScore>();
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
            Inventory.instance.StorageDetected(null);
            GetComponent<Animator>().SetBool("isOpened", false);
            interactingText.GetComponent<Animator>().SetBool("isInteracting", false);
            if (storageType != StorageType.Storage) UpdateScore();
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
                if (storageType != StorageType.Storage) UpdateScore();
            }   
            else
            {
                Inventory.instance.StorageDetected(storage);
            }
            storage.SetContent(ref items, storageType);
            storageUI.SetActive(chestOpened);
            inventoryUI.SetActive(chestOpened);
        }
    }

    void UpdateScore()
    {
        int totalScore = 0;
        for (int i = 0; i < items.Count; i++)
        {
            totalScore += items[i].quantity;
        }
        items = new List<ListItem>();
        playerScore.UpdateScore(((int)storageType) - 1, totalScore);
    }
}
