using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionTrigger : MonoBehaviour
{
    [Header("Dependences")]
    public DivisionManager manager;
    public PlaftormController platform;
    [Header("Trigger Characteristics")]
    public int optionNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Platform")
        {
            if (platform!=null) 
            {
                if (manager.listOfAwnsers.Contains(optionNumber)) 
                {
                    platform.GetComponentInChildren<TextMeshPro>().text = "Correcto";
                    manager.ContCorrectas++;
                    manager.faltantes--;
                }
                else 
                {
                    platform.GetComponentInChildren<TextMeshPro>().text = "Incorrecto";
                    manager.ContIncorrectas++;
                    manager.oportunidades--;
                }
                //Activo la plataforma con la que colisiono
                manager.changeQuestionChossing();
                platform.active = true;

            }
        }

    }
}
