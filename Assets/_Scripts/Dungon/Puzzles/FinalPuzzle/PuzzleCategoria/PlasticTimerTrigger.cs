using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticTimerTrigger : MonoBehaviour
{
    public PlasticTrapTimer option;
    public PlasticTrapTimer option2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            option.tiempoRestante = option.tiempoInicial;
            option2.tiempoRestante = option2.tiempoInicial;
            option.CuentaRegresiva = true;
            option2.CuentaRegresiva = true;
        }
    }

  
}
