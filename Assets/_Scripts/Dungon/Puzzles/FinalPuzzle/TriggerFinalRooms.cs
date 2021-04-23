using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{

    [Header("Dependences")]
    public PaperRoomManager paperManager;
    public MetalRoomManager metalManager;
    public PlaftormController platform;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (platform != null)
            {
                platform.active = false;
            }
            if(paperManager != null && metalManager != null) 
            {
                metalManager.oportunidades = paperManager.oportunidades + metalManager.oportunidades;
            }
        }

    }
}
