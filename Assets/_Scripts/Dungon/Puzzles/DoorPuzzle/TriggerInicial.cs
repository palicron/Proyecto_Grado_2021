using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInicial : MonoBehaviour
{

     [Header("Inicial Dependences")]
    public Rigidbody floor;
    public DoorMovilePuzzle doorScript;

     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
        }

    }
}
