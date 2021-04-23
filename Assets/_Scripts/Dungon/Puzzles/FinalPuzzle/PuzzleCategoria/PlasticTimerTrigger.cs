using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticTimerTrigger : MonoBehaviour
{
    public PlasticTrapTimer option;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            option.CuentaRegresiva = true;
        }
    }

  
}
