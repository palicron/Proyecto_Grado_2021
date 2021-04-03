using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInicial : MonoBehaviour
{

    [Header("Inicial Dependences")]
    public PlaftormController floor;
    //Atributo encargado de activar la primera puerta de preguntas del puzzle
    public DoorMovilePuzzle OldMovileScript;
    public DoorMovilePuzzle NextMovileScript;
 

     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (OldMovileScript!=null) { NextMovileScript.incorrect = OldMovileScript.incorrect; ; }
            NextMovileScript.isActivated=true;
            NextMovileScript.MovileSpeed = NextMovileScript.MovileSpeed + NextMovileScript.incorrect;  
            NextMovileScript.GiveSpeedDoors = true;
            if(floor!=null){floor.active=true;}
        }
    }
}
