using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        healthsystems hs = other.gameObject.GetComponent<healthsystems>();
        if(hs)
        {
            hs.TakeDmg(damage);
            Debug.Log("HIT");
        }
    }
}
