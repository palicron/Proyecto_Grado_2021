using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaperRoomTrigger : MonoBehaviour
{
    [Header("Dependences")]
    public PaperRoomManager manager;
    public PlaftormController platform;
    public PaperQuestionTrigger questionManger;
    [Header("Trigger Characteristics")]
    public int optionNumber;
    public float waitTime;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (platform!=null) 
            {
                //Activo la plataforma con la que colisiono
                if (manager.listOfAwnsers.Contains(optionNumber)) 
                {
                    platform.GetComponentInChildren<TextMeshPro>().text = "Correcto";
                    manager.ContCorrectas++;
                    manager.faltantes--;
                    if (manager.faltantes==0) 
                    {
                        foreach (PlaftormController plat in questionManger.traps)
                        {
                            plat.type = PlaftormController.PlatformType.MOVEMENTESCREENTRIGGERED;
                            plat.GetComponentInChildren<DebufController>().DammageDebuff = 0f;
                            plat.platformSpeed = 10f;
                            plat.active = true;
                        }
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

                }
                questionManger.activated = false;
                StartCoroutine(WaitMovement(waitTime));
                manager.AnwserChoosed();
               
            }   
        }

    }

    IEnumerator WaitMovement(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (PlaftormController plat in manager.platforms)
        {

            plat.active = true;
        }
    }

}
