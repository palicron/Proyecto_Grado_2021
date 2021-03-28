using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{

    public DoorScript doorScript;
    public DoorMovilePuzzle movileScript;
    private GameObject parent;

 private void Start()
    {
        doorScript = this.GetComponentInParent<DoorScript>();
        movileScript = doorScript.gameObject.GetComponentInParent<DoorMovilePuzzle>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            doorScript.isChosed=true;
            if(doorScript.correct==false)
            {   
                movileScript.incorrect++;
            }
        }

    }
}
