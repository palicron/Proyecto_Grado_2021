using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFloatPuzzle : MonoBehaviour
{
    public FloatingManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            manager.active = true;
        }

    }
}
