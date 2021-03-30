using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTime : MonoBehaviour
{
    public PlaftormController[] platforms;
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
                foreach (PlaftormController platctr in platforms)
                {
                  platctr.active = true;
                    
                }
                used = true;
            }
            else 
            {
                used = false;
                if (manager!=null) 
                {
                    if (manager.QaA.Count > 0)
                    {
                        manager.changeQuestion();
                    }
                }
            }
        }

    }


}
