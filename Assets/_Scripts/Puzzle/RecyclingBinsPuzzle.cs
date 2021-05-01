using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecyclingBinsPuzzle : MonoBehaviour
{

    public TextMeshPro textHolder;

    public GameObject items;

    public Storage storage;

    public int minimumScore;

    public Animator animator;

    public PlaftormController completedPlatform;

    public PlaftormController failedPlatform;



    public void EnableItems()
    {
        items.SetActive(true);
    }

    public void StartPuzzle()
    {
        animator.SetBool("enter",true);
    }

    public void Verify()
    {
        if(storage.GetTotalScore()<minimumScore)
        {
            textHolder.text = "No lograste completar el reto...";
            failedPlatform.active=true;
            return;
        }
        textHolder.text = "¡Felicidades!\nSuperaste este reto.";
        completedPlatform.active = true;
        Debug.Log("Puntos necesarios: " + minimumScore + " - Puntos adquiridos: " + storage.GetTotalScore());
        return;
    }
    // Start is called before the first frame update
    void Start()
    {
        items.SetActive(false);
        //animator.SetBool("enter", true);
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
