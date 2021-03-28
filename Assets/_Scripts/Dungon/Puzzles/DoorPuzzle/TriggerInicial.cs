using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInicial : MonoBehaviour
{

    [Header("Inicial Dependences")]
    public PlaftormController floor;
    //Atributo encargado de activar la primera puerta de preguntas del puzzle
    public DoorMovilePuzzle doorScript;
    [Header("Floor Characteristics")]

    public Transform[] points;
     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            doorScript.isActivated=true;
            if(floor!=null){floor.active=true;}
        }
    }
}
