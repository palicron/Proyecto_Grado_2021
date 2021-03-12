using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPuzzle : MonoBehaviour
{

    public PuzzleBotonController ButtonContr;
    public PuzzlePlatform PlatformCt;
    public FallFloorController fallFloorController;


    private void Start()
    {
        fallFloorController = transform.GetComponentInParent<FallFloorController>();
    }

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
                if (PlatformCt.correct && !PlatformCt.AlreadyActivated) 
                {
                    fallFloorController.correctAwnser++;
                    PlatformCt.AlreadyActivated = true;
                }
                
            }
        }

    }

  

    // Update is called once per frame
    void Update()
    {
        
    }
}
