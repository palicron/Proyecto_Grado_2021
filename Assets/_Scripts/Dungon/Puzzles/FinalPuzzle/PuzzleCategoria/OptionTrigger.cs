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
                platform.active = true;
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
                foreach (PlaftormController plat in otherPlatforms)
                {
                    plat.active = true;
                }
                //Activo la plataforma con la que colisiono
                if (manager.QaA.Count != 0)
                {
                    manager.changeQuestionChossing();
                }
                else { manager.failed = true; }
            }
        }

    }
}
