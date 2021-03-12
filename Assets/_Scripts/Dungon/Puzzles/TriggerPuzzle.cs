using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPuzzle : MonoBehaviour
{

    public PuzzleBotonController ButtonContr;
    public PuzzlePlatform PlatformCt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (ButtonContr != null)
            {
                ButtonContr.active = true;
            }
            else if (PlatformCt!=null) 
            {
                PlatformCt.active = true;
            }
            
        }

    }

  

    // Update is called once per frame
    void Update()
    {
        
    }
}
