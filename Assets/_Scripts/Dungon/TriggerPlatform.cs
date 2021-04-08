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
    [Header("ON/OFF ROT VALUES")]
    public float Rotx;
    public float Roty;
    public float Rotz;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (platformCt.type == PlaftormController.PlatformType.TRIGGEREXIT)
            {
                platformCt.active = false;
                StartCoroutine(WaitForMove(2f));
            }
            else if (platformCt.type == PlaftormController.PlatformType.ROTATIVE)
            {
                platformCt.platformSpeed = 0;
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
            else if (platformCt.type == PlaftormController.PlatformType.ROTATIVETRIGGER)
            {
                if (Rotx !=0 && Roty == 0 && Rotz == 0) 
                {
                    platformCt.rotationX = Rotx;
                    platformCt.rotationY = 0;
                    platformCt.rotationZ = 0;
             
                }
                if (Roty != 0 && Rotx == 0 && Rotz == 0)
                {
                    platformCt.rotationY = Roty;
                    platformCt.rotationZ = 0;
                    platformCt.rotationX = 0;
                  

                }
                if (Rotz != 0 && Roty == 0 && Rotx == 0)
                {
                    platformCt.rotationZ = Rotz;
                    platformCt.rotationY = 0;
                    platformCt.rotationX = 0;
                    

                }
                platformCt.active = true;
            }
            else {platformCt.active = true;} 
        }
        
    }

    void OnTriggerExit(Collider other) {
        if (platformCt.type == PlaftormController.PlatformType.ROTATIVETRIGGER)
        {
            platformCt.active = false;
        }
        else if (platformCt.type == PlaftormController.PlatformType.TRIGGEREXIT)
        {
            platformCt.active = false;

        }
    }

     IEnumerator WaitForMove(float time) {  
        yield return new WaitForSeconds(time);
         platformCt.active = true;
        }
}
