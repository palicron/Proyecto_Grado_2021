using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPuzzle : MonoBehaviour
{

    public PuzzleBotonController ButtonContr;
    public PuzzlePlatform PlatformCt;
    public FallFloorController fallFloorController;
    public FloatingPuzzle floatingPuzzle;


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
            else if (PlatformCt!=null && fallFloorController!=null) 
            {
                PlatformCt.active = true;
                if (PlatformCt.correct && !PlatformCt.AlreadyActivated) 
                {
                    fallFloorController.correctAwnser++;
                    PlatformCt.AlreadyActivated = true;
                }
                
            }
            else if (floatingPuzzle != null)
            {
                floatingPuzzle.initialPos = new Vector3(floatingPuzzle.platformRB.position.x, floatingPuzzle.platformRB.position.y, floatingPuzzle.platformRB.position.z);
                floatingPuzzle.active = true;
            }

        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
