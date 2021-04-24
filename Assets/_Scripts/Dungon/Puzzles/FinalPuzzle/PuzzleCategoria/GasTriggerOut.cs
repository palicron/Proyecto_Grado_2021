using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTriggerOut : MonoBehaviour
{
    public bool GasIsActivated;
    public int validacion=0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Platform")
        {
            GasIsActivated = true;
            validacion = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Platform")
        {
            GasIsActivated = false;
            validacion=1;
        }

    }

}
