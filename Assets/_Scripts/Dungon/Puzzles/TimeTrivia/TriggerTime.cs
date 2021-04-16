using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerTime : MonoBehaviour
{
    public PlaftormController[] platforms;
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
            if (!used)
            {
                foreach (PlaftormController platctr in platforms)
                {
                  platctr.active = true;
                }
                if(TextopanelOpciones.Length!=0)
                {
                    foreach (TextMeshPro textField in TextopanelOpciones)
                    {
                    textField.text = "";
                    }
                }
               
                used = true;
            }
            else 
            {
                used = false;
                if (manager!=null) 
                {

                    if (manager.preguntasRespondidasCorrectamente == manager.cantidadDePreguntas)
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
                       
                        manager.changeQuestion();
                    }
                }
            }
        }

    }


}
