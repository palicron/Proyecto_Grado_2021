using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTriviaTrigger : MonoBehaviour
{
   public PlaftormController[] platforms;
   public TriviaManager manager;
   public bool used;
   public int incorrectPosition=1;
    public int correctAnwsers; 
   public bool choosedAPlatform;


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
                    if (i == manager.correctAnwser)
                    {
                        if (platforms[i].playerOnPlat) { 
                            choosedAPlatform = true;
                            manager.preguntasRespondidasCorrectamente++;
                        }
                        
                        platforms[i].active = false;
                    }
                    else 
                    {

                        platforms[i].positionTrap = incorrectPosition;
                        platforms[i].active = true;
                        if (platforms[i].playerOnPlat)
                        {
                            choosedAPlatform = true;
                            incorrectPosition++;
                        }
                    }
                    
                }
            
                used = true;
            }
            if (!choosedAPlatform) 
            {
                incorrectPosition++;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        used = false;
    }
}
