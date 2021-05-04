using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticTrapDownTrigger : MonoBehaviour
{
    public PlaftormController trap;
    public float waitTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trap.active = false;

        }
        else if (other.tag == "Platform") 
        {
            StartCoroutine(WaitForMove(waitTime));
        }
    }


    IEnumerator WaitForMove(float time)
    {
        yield return new WaitForSeconds(time);
        trap.active = false;
    }
}
