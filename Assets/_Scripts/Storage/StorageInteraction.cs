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

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            playerClose = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            chestOpened = false;
            playerClose = false;
            playerInteracting = false;
            storageUI.SetActive(false);
            GetComponent<Animator>().SetBool("isOpened", false);
        }
    }

    void Update()
    {
        if (playerClose && Input.GetButtonDown("Interaction"))
        {
            chestOpened = !chestOpened;
            playerInteracting = chestOpened;
            GetComponent<Animator>().SetBool("isOpened", chestOpened);
            storageUI.SetActive(chestOpened);
            inventoryUI.SetActive(chestOpened);
        }
    }
}
