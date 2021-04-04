using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{

    public PlaftormController platformCt;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(platformCt.type == PlaftormController.PlatformType.TRIGGEREXIT )
            {
             platformCt.active = false;
            StartCoroutine(WaitForMove(2f));
            }
            else{platformCt.active = true;} 
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
