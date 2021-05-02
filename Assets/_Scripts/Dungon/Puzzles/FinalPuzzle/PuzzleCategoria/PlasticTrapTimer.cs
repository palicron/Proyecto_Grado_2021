using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlasticTrapTimer : MonoBehaviour
{
    [Header("Dependencies")]
    public TextMeshPro timerTxt;
    public PlaftormController plat;
    [Header("Timer Dependences")]
    public float tiempoInicial;
    public float tiempoRestante;
    public bool CuentaRegresiva;

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
                timerTxt.text = "Trampa activada \n" + tiempoRestante.ToString("f0");
            }
            else
            {
                timerTxt.text = "";
                activarTrampa();
                
            }
        }
    }

    void activarTrampa() 
    {
        CuentaRegresiva = false;
        plat.active = true;
    }


}
