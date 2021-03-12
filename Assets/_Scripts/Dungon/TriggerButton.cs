using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : MonoBehaviour
{

    public PuzzleBotonController ButtonContr;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ButtonContr.active = true;
        }

    }

  

    // Update is called once per frame
    void Update()
    {
        
    }
}
