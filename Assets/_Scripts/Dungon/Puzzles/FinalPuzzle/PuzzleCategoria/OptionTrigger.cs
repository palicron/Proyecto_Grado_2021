using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionTrigger : MonoBehaviour
{
    [Header("Dependences")]
    public DivisionManager manager;
    public PlaftormController platform;
    public PlaftormController[] otherPlatforms;
    [Header("Trigger Characteristics")]
    public int optionNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (platform!=null) 
            {
                //Activo la plataforma con la que colisiono
                platform.active = true;
                if (manager.listOfAwnsers.Contains(optionNumber)) 
                {
                    platform.GetComponentInChildren<TextMeshPro>().text = "Correcto";
                    manager.ContCorrectas++;
                    manager.faltantes--;
                    if (manager.faltantes==0) 
                    {
                        manager.completed = true;
                    }
                }
                else 
                {
                    platform.GetComponentInChildren<TextMeshPro>().text = "Incorrecto";
                    manager.timeTxt.text = "Trampa activada";
                    manager.ContIncorrectas++;
                    manager.oportunidades--;
                    if (manager.oportunidades == 0)
                    {
                        manager.failed = true;
                    }
                    else
                    {
                        for (int i = 1; i <= manager.ContIncorrectas; i++)
                        {
                            manager.changeQuestTrap[i].active = true;
                        }
                    }

                }
                foreach (PlaftormController plat in otherPlatforms)
                {
                    plat.active = true;
                }
                if (manager.QaA.Count != 0)
                {
                    manager.changeQuestionChossing();
                }
                else { manager.failed = true; }
            }
        }

    }
}
