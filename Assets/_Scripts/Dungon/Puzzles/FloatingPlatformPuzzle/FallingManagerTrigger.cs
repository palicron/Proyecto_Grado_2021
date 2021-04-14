using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingManagerTrigger : MonoBehaviour
{

    [Header("Trigger Depende")]
    public FloatingOption optionActual;
    public FallinFloorManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (optionActual.correct)
            {

                if (manager.ActualFloatingPosition <= manager.managers.Count && !optionActual.active)
                {
                    manager.ActualFloatingPosition++;
                    StartCoroutine(waitForMove(optionActual.waitTime));
                   
                }
                else if(manager.ActualFloatingPosition > manager.managers.Count)
                {
                    manager.completed = true;
                }
            }
        }

    }

    IEnumerator waitForMove(float time)
    {     
        yield return new WaitForSeconds(time+1);
        manager.changeQuestion();
    }
}
