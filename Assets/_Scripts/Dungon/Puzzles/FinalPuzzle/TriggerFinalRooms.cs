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
            if (platform != null)
            {
                platform.active = false;
            }
            if (paperManager != null && metalManager != null && !activada)
            {
                metalManager.oportunidades = paperManager.oportunidades + metalManager.oportunidades;
            }
            else if (plasticManager != null && metalManager != null && !activada)
            {
                plasticManager.oportunidades = plasticManager.oportunidades + metalManager.oportunidades;
            }
            activada=true;
        }

    }
}
