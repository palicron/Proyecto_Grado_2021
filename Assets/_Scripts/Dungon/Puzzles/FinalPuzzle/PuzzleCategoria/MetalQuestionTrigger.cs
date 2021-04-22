using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalQuestionTrigger : MonoBehaviour
{
    [Header("Dependences")]
    public MetalRoomManager manager;
    public PlaftormController plat;
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
                activated = true;
            }
        }

    }

}
