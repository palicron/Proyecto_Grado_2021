    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInicial : MonoBehaviour
{

    [Header("Inicial Dependences")]
    public PlaftormController floor;
    private GameObject parent;
    //Atributo encargado de activar la primera puerta de preguntas del puzzle
    public DoorMovilePuzzle OldMovileScript;
    public DoorMovilePuzzle NextMovileScript;
    public FinalDoorSystem finalDoorSystemScript;

    private void Start()
    {
        OldMovileScript = gameObject.GetComponentInParent<DoorMovilePuzzle>();
        finalDoorSystemScript = GameObject.Find("MovingSystem").GetComponent<FinalDoorSystem>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (OldMovileScript!=null && NextMovileScript!=null) { NextMovileScript.incorrect = OldMovileScript.incorrect; }
            if (NextMovileScript!=null)
            {
                NextMovileScript.isActivated = true;
                NextMovileScript.MovileSpeed = NextMovileScript.MovileSpeed + (4*NextMovileScript.incorrect);
                NextMovileScript.GiveSpeedDoors = true;
            }
            if (finalDoorSystemScript!= null && OldMovileScript != null) 
            {

                finalDoorSystemScript.anwsers++;
                finalDoorSystemScript.incorrect = OldMovileScript.incorrect;
            }
            if(floor!=null){floor.active=true;}
        }
    }
}
