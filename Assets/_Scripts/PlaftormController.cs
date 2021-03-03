using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaftormController : MonoBehaviour
{

    public Rigidbody platformRB;
    public PlatformType type;
    public float platformSpeed;
    public float rotationX;
    public float rotationY;
    public float rotationZ;

    public Transform[] positions;
    private int actualPosition = 0;
    private int nextposition = 1;
    public bool moveToTheNext = true;
    public float waitTime;
    public bool active = false;
  

    public enum PlatformType{ 
        NORMAL,
        TRANSLATE,
        TRIGGER,
        TRIGGEREXIT,
        ROTATIVE
    }

    void Awake() {
        if (type == PlatformType.TRANSLATE || type == PlatformType.ROTATIVE) { active = true; }
    }
   

    // Update is called once per frame
    void Update()
    {
        if (active) {
            MovePlatform();
        }
    }

    void MovePlatform() 
    {

        if (type == PlatformType.TRANSLATE || type == PlatformType.TRIGGER || type == PlatformType.TRIGGEREXIT)
        {
            if (moveToTheNext)
            {
                StopCoroutine(WaitForMove(0));
                platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[nextposition].position, platformSpeed * Time.deltaTime));
            }

            if (Vector3.Distance(platformRB.position, positions[nextposition].position) <= 0)
            {
                StartCoroutine(WaitForMove(waitTime));
                actualPosition = nextposition;
                nextposition++;

                if (nextposition > positions.Length - 1)
                {
                    nextposition = 0;
                }
            }
        }
        else if(type == PlatformType.ROTATIVE)
        {
            transform.Rotate(new Vector3(rotationX, rotationY, rotationZ) * platformSpeed * Time.deltaTime);
        }
    }

    IEnumerator WaitForMove(float time) {
        moveToTheNext = false;
        yield return new WaitForSeconds(time);
        moveToTheNext = true;
    }

  

}
