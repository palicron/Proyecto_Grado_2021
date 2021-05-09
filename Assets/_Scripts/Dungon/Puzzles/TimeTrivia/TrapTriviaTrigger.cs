using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrapTriviaTrigger : MonoBehaviour
{
   public PlaftormController[] platforms;
   public TextMeshPro[] TextopanelOpciones;
   public TriviaManager manager;
   public bool used;

    private void Start()
    {
        used = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Platform")
        {
            if (!used)
            {
                for (int i=0; i < platforms.Length; i++) 
                {
                  
                    if (platforms[i].playerOnPlat && manager.listOfAwnsers.Contains(i)) { 
                           manager.timerTxt.text = "Correcto";
                           manager.ContCorrectas++;
                           manager.Restantes--;
                      }

                    else if(platforms[i].playerOnPlat && !manager.listOfAwnsers.Contains(i))
                    {
                        manager.timerTxt.text = "Incorrecto";
                        if (platforms[i].playerOnPlat)
                        {
                        
                            manager.ContIncorrectas++;
                            manager.Oportunidades--;
                        }
                        platforms[i].positionTrap = manager.ContIncorrectas;
                        platforms[i].active = true;  
                    }
                }
                used = true;
               
                resetAllText();
               
            }

        }

    }

    void resetAllText() 
    {
        foreach (TextMeshPro textInfo in TextopanelOpciones) 
        {
            textInfo.text = "";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        manager.Questiontxt.text = "";   
        used = false;
    }
}
