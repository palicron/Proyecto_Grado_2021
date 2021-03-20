using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDmg : MonoBehaviour
{

   public float Dmg = 20.0f;

    private void OnTriggerEnter(Collider other)
    {
        Playerhealthsystems hs = other.GetComponent<Playerhealthsystems>();
        if(hs)
        {
            hs.TakeDmg(Dmg);
        }
    }

}
