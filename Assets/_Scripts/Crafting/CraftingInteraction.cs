using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingInteraction : MonoBehaviour
{
    private bool playerClose = false;

    private bool interacting = false;

    GameObject craftingUI;

    GameObject gameUI;

    public GameObject interactingText;

    void Start()
    {
        gameUI = GameObject.Find("/PF_GameUI");

        craftingUI = gameUI.transform.Find("Crafting").gameObject;
    }
    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            gameUI.GetComponent<CraftingUI>().SetPlayerScore(collider.gameObject.GetComponent<PlayerScore>());
            playerClose = true;
            interactingText.GetComponent<Animator>().SetBool("isInteracting", true);
        }
    }

    void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            craftingUI.SetActive(false);
            interacting = false;
            playerClose = false;
            interactingText.GetComponent<Animator>().SetBool("isInteracting", false);
        }
    }

    void Update()
    {
        if (playerClose && Input.GetButtonDown("Interaction"))
        {
            interacting = !interacting;
            craftingUI.SetActive(!craftingUI.activeSelf);
        }
    }
}
