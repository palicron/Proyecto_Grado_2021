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
            platformCt.active = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (platformCt.type == PlaftormController.PlatformType.TRIGGEREXIT)
        {
            platformCt.active = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
