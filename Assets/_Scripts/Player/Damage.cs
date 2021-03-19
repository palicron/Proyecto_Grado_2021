using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage = 10;
    public GameObject particle;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Player"))
            return;   
        healthsystems hs = other.gameObject.GetComponent<healthsystems>();
        if(hs)
        {
            hs.TakeDmg(damage);
          
        }
    }
}
