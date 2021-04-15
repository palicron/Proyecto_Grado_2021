using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame_Interaction : MonoBehaviour
{
    private bool playerClose = false;

    private bool interacting = false;

    GameObject minigameUI;

    GameObject gameUI;

    public GameObject interactingText;

    void Start()
    {
        gameUI = GameObject.Find("/PF_GameUI");

        minigameUI = gameUI.transform.Find("MiniGame").gameObject;
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
            playerClose = false;
            interactingText.GetComponent<Animator>().SetBool("isInteracting", false);
        }
    }

    void Update()
    {
        if (playerClose && Input.GetButtonDown("Interaction"))
        {
            interacting = minigameUI.activeSelf;
            if (interacting) return;
            minigameUI.SetActive(true);
            UI_Status.instance.SetOpen(true, MenuType.MiniGame);
            Time.timeScale = 0.0f;
        }
    }
}
