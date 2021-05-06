using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFinalBossCtr : MonoBehaviour
{
  

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            FinalLevelCtr.intance.starFight();
            Destroy(this);
        }
        
    }

 
}
