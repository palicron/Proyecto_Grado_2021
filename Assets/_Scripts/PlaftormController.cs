using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaftormController : MonoBehaviour
{

    public Rigidbody platformRB;
    public float platformSpeed;
    public Transform[] positions;

    private int actualPosition = 0;
    private int nextposition = 1;

    public bool moveToTheNext = true;
    public float waitTime;


    // Update is called once per frame
    void Update()
    {
        MovePlatform();
        
    }

    void MovePlatform() 
    {
        if (moveToTheNext) {
            StopCoroutine(WaitForMove(0));
            platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[nextposition].position, platformSpeed * Time.deltaTime));
        }

        if (Vector3.Distance(platformRB.position, positions[nextposition].position) <=0) 
        {
            StartCoroutine(WaitForMove(waitTime));
            actualPosition = nextposition;
            nextposition++;

            if (nextposition > positions.Length-1) {
                nextposition = 0;
            }
        }
    }

    IEnumerator WaitForMove(float time) {
        moveToTheNext = false;
        yield return new WaitForSeconds(time);
        moveToTheNext = true;
    }

}
