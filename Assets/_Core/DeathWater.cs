using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            GameManager.intance.resetPlayer(15);
        }
       
    }
}
