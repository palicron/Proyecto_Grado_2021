using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovilePuzzle : MonoBehaviour
{

    [Header("Movile Dependences")]
    public Rigidbody RBMovile;
    public DoorScript[] Doors;
    public Transform[] positions;
     [Header("Movile Characteristics")]
    public float MovileSpeed;
    public float waitTime;
    private int actualPosition = 0;
    private int nextposition = 1;
    public bool moveToTheNext = true;
    [Header("Movile Information")]
    public int correctAnwser=-1;
    public int incorrect=0;
    public bool isActivated=false;
    public bool GiveSpeedDoors=false;
    
  

    // Update is called once per frame
    void Update()
    {
        if(isActivated)
        {
            if (GiveSpeedDoors) 
            {
                int i=0;
                foreach (DoorScript doors in Doors)
                {   
                    doors.IncorrectAccountant = incorrect;
                    doors.waitTime = doors.waitTime + (incorrect * 0.15f)  ;
                    if(i==correctAnwser)
                    {
                        doors.correct=true;
                    }
                    i++;
                }
                GiveSpeedDoors = false;
            }
            MoveMovileSystem();
        }
    }


    void MoveMovileSystem()
    {
         if (moveToTheNext)
            {
                StopCoroutine(WaitForMove(0));
                RBMovile.MovePosition(Vector3.MoveTowards(RBMovile.position, positions[nextposition].position, MovileSpeed * Time.deltaTime));
            }

            if (Vector3.Distance(RBMovile.position, positions[nextposition].position) <= 0)
            {
                StartCoroutine(WaitForMove(waitTime));
                actualPosition = nextposition;
                nextposition++;
                if (nextposition > positions.Length - 1)
                {
                    isActivated= false;
                }
            }
    }

     IEnumerator WaitForMove(float time) {
        moveToTheNext = false;
        yield return new WaitForSeconds(time);
        moveToTheNext = true;
    }

}
