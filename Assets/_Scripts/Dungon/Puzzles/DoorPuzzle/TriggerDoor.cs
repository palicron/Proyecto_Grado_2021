using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{

    public DoorScript doorScript;
    public DoorMovilePuzzle movileScript;
    public bool alreadyTriggered;

 private void Start()
    {
        alreadyTriggered=false;
        doorScript = this.GetComponentInParent<DoorScript>();
        movileScript = doorScript.gameObject.GetComponentInParent<DoorMovilePuzzle>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            doorScript.isChosed=true;
            if(!alreadyTriggered)
            {
                if(doorScript.correct==false)
                {
                movileScript.incorrect++;
                }
                alreadyTriggered=true;
            }
        }

    }
}
