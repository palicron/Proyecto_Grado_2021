using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MetalQuestionTrigger : MonoBehaviour
{
    [Header("Dependences")]
    public MetalRoomManager manager;
    public PlaftormController plat;
    public TextMeshPro stateText;
    [Header("Trigger Characteristics")]
    public bool activated;

    private void Start()
    {
        stateText.text = "Activador \n en espera";
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!activated)
            {
                stateText.text = "Activador \n generado";
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
