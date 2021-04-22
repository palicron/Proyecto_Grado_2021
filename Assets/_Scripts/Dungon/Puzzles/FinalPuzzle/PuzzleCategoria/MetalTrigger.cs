using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalTrigger : MonoBehaviour
{
    public MetalRoomOption option;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            option.choosed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            option.choosed = false  ;
        }

    }
}
