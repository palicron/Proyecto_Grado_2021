using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToM_Trigger : MonoBehaviour
{
    [SerializeField]
    NPC_Tom TargetTom;
    [SerializeField]
    int DialogueTrigger = 0;
    [SerializeField]
    bool OneTime = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCtr>())
        {
            TargetTom.starConbersationEvent(other.gameObject.GetComponent<PlayerCtr>());
         
            if (OneTime)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
