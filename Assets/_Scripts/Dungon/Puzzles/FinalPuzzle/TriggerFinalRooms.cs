using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinalRooms : MonoBehaviour
{

    [Header("Dependences")]
    public PaperRoomManager oldManager;
    public PaperRoomManager newManager;
    public PlaftormController platform;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (platform != null)
            {
                platform.active = false;
            }
            if(oldManager!=null && newManager!=null) 
            {
                newManager.oportunidades += oldManager.oportunidades;
            }
        }

    }
}
