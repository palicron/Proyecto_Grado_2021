using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperQuestionTrigger : MonoBehaviour
{
    [Header("Dependences")]
    public PaperRoomManager manager;
    public PlaftormController[] traps;
    [Header("Trigger Characteristics")]
    public bool activated;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!activated)
            {
                if (manager.QaA.Count != 0)
                {
                    manager.generateQuestion();
                    
                }
                else
                {
                    manager.failed = true;
                }

                if (traps.Length!=0)
                {
                    if (manager.ContIncorrectas>0 && manager.ContIncorrectas < traps.Length) 
                    {
                        traps[manager.ContIncorrectas].platformSpeed += 1f; 
                        traps[manager.ContIncorrectas].active = true;
                    }
                }
                activated = true;
            }
        }

    }
}
