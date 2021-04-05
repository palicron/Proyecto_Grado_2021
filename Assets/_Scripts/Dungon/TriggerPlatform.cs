using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerPlatform : MonoBehaviour
{

    public PlaftormController platformCt;
    [Header("ON/OFF VALUES")]
    public TextMeshPro state;
    public float Velx;
    public float Velz;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (platformCt.type == PlaftormController.PlatformType.TRIGGEREXIT)
            {
                platformCt.active = false;
                StartCoroutine(WaitForMove(2f));
            }
            else if (platformCt.type == PlaftormController.PlatformType.TRANSPORTPLAYER)
            {
                if (Velx != 0)
                {
                    platformCt.VelX = Velx;
                }
                if (Velz != 0)
                {
                    platformCt.Velz = Velz;
                }
            }
            else if(state!=null)
            {
                platformCt.active = true;
                state.text = "ON";  
            }
            else {platformCt.active = true;} 
        }
        
    }

    void OnTriggerExit(Collider other) {
        if (platformCt.type == PlaftormController.PlatformType.ROTATIVETRIGGER)
        {
            platformCt.active = false;
        }
    }

     IEnumerator WaitForMove(float time) {  
        yield return new WaitForSeconds(time);
         platformCt.active = true;
        }
}
