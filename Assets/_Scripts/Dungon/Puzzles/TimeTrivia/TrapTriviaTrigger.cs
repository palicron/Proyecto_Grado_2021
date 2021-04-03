using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTriviaTrigger : MonoBehaviour
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
                for (int i=0; i < platforms.Length; i++) 
                {
                    if (i+1 == manager.correctAnwser) { platforms[i].active = false; }
                    else { platforms[i].active = true; }
                    
                }
            
                used = true;
            }
            else 
            {
                used = false;
            }
        }

    }
}
