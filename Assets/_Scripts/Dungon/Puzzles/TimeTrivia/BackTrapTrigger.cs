using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTrapTrigger : MonoBehaviour
{
    public PlaftormController[] platforms;
    public bool used;


    private void Start()
    {
        used = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Platform")
        {
            if (used)
            {
                foreach (PlaftormController platctr in platforms)
                {
                    platctr.active = true;
                }
                used = false;
            }
            else
            {
                used = true;
     
            }
        }

    }
}
