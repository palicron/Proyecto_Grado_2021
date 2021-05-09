using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerTime : MonoBehaviour
{
    public PlaftormController[] plataformasRegreso;
    public PlaftormController[] panelesElevadizos;
    public TextMeshPro[] TextopanelOpciones;
    public TriviaManager manager;
    public bool used;


    private void Start()
    {
        used = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Platform")
        {
            if (!used && !manager.completed)
            {
                foreach (PlaftormController platctr in plataformasRegreso)
                {
                  platctr.active = true;
                }
                foreach (PlaftormController platctr in panelesElevadizos)
                {
                    platctr.active = false;
                }
                if (TextopanelOpciones.Length!=0)
                {
                    foreach (TextMeshPro textField in TextopanelOpciones)
                    {
                    textField.text = "";
                    }
                }
                manager.timerTxt.text = "Esperando Activador";
                used = true;
            }
            else 
            {
                used = false;
                if (manager!=null) 
                {
                    foreach (PlaftormController platctr in plataformasRegreso)
                    {
                        platctr.active = false;
                    }
                    if ( manager.Restantes == 0)
                    {
                        manager.completed = true;
                    }
                    else if(manager.QaA.Count==0)
                    {
                        //TODO
                    }
                    else {
                        if (panelesElevadizos.Length!=0)
                        {
                            foreach (PlaftormController platctr in panelesElevadizos)
                            {
                                platctr.active = true;
                            }
                        }   
                        manager.CuentaRegresiva = true;
                        manager.changeQuestion();
                    }
                }
            }
        }

    }


}
