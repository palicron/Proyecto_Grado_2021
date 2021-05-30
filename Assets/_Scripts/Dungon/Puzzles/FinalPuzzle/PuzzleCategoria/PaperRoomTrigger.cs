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
                //Texto guia posterior a una decision del jugador
                manager.CuentaRegresiva=false;
                manager.Questiontxt.text = "Vuelve al activador";
                //Activo la plataforma con la que colisiono
                if (manager.listOfAwnsers.Contains(optionNumber)) 
                {
                    manager.timeTxt.text = "Correcto";
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
                    manager.timeTxt.text = "Incorrecto";
                    manager.ContIncorrectas++;
                    manager.oportunidades--;
                    if (manager.oportunidades == 0)
                    {
                        manager.failed = true;
                    }

                }
                questionManger.activated = false;
                StartCoroutine(WaitMovement(waitTime));          
            }   
        }

    }

    IEnumerator WaitMovement(float time)
    {
        yield return new WaitForSeconds(time);
        questionManger.stateText.text = "Activador \n en espera";
        foreach (PlaftormController plat in manager.platforms)
        {
            plat.active = false;
        }
    }

}
