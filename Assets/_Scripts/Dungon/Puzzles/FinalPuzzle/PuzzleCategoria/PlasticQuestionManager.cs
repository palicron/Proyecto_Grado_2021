using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticQuestionManager : MonoBehaviour
{
    [Header("Dependences")]
    public PlasticRoomManager manager;
    public PlaftormController plat;
    public PlaftormController[] optionPlatforms;
    [Header("Trigger Characteristics")]
    public bool activated;

    private void Start()
    {
        activated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!activated)
            {
                if (manager.QaA.Count != 0)
                {
                    foreach (PlaftormController plat in optionPlatforms) 
                    {
                        plat.active = false;
                    }
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
