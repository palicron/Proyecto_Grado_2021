using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinalRooms : MonoBehaviour
{

    [Header("Dependences Platform")]
    public PlaftormController platform;
    public bool activada;
    [Header("Manager Dependences")]
    public PaperRoomManager paperManager;
    public MetalRoomManager metalManager;
    public PlasticRoomManager plasticManager;
   



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
          
            if (paperManager != null && metalManager != null && !activada)
            {
                paperManager.completed = false;
                metalManager.oportunidades = paperManager.oportunidades + metalManager.oportunidades;
            }
            else if (plasticManager != null && metalManager != null && !activada)
            {
                metalManager.completed = false;
                plasticManager.oportunidades = plasticManager.oportunidades + metalManager.oportunidades;
            }
            if (platform != null)
            {
                platform.active = false;
            }
            activada =true;
        }

    }
}
