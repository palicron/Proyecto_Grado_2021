using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlasticTrapTimer : MonoBehaviour
{
    [Header("Dependencies")]
    public PlaftormController trampZone;
    public TextMeshPro timerTxt;
    [Header("Timer Dependences")]
    public float tiempoInicial;
    public float tiempoRestante;
    public bool CuentaRegresiva;
    public float waitTime;

    private void Start()
    {
        tiempoRestante = tiempoInicial;
    }


    private void FixedUpdate()
    {
        if (CuentaRegresiva)
        {
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
                timerTxt.text = "" + tiempoRestante.ToString("f0");
            }
            else
            {
                activarTrampa();
                tiempoRestante = tiempoInicial;
                StartCoroutine(WaitTrapTime(waitTime));
            }
        }
    }

    void activarTrampa() 
    {
        CuentaRegresiva = false;
        trampZone.active=true;
    }


    IEnumerator WaitTrapTime(float time)
    {
        yield return new WaitForSeconds(time);
        trampZone.active=false;

    }

}
