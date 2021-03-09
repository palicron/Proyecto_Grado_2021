using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    Quaternion angle_final = Quaternion.Euler(90, 0, 0);
    Quaternion angle_start = Quaternion.Euler(0, 0, 0);
    public Quaternion target;

    public enum PlatformType{ 
        NORMAL,
        TRANSLATE,
        TRIGGERTRANSLATE,
        TRIGGEREXIT,
        ROTATIVE,
        ROTATIVETRIGGER,
       
    }

    void Start()
    {
        target = angle_start;
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
        else if (type == PlatformType.ROTATIVETRIGGER) 
        {
            platformRB.DORotate(new Vector3(0, 0, 0), 0.5f, RotateMode.Fast);
            
        }
    }



    void MovePlatform() 
    {

        if (type == PlatformType.TRANSLATE || type == PlatformType.TRIGGERTRANSLATE || type == PlatformType.TRIGGEREXIT)
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
        else if (type == PlatformType.ROTATIVE)
        {
            transform.Rotate(new Vector3(rotationX, rotationY, rotationZ) * platformSpeed * Time.deltaTime);
        }
        else if (type == PlatformType.ROTATIVETRIGGER)
        {
            platformRB.DORotate(new Vector3(90, 0, 0), 0.10f, RotateMode.Fast);

        }
    }


    IEnumerator WaitForMove(float time) {
        moveToTheNext = false;
        yield return new WaitForSeconds(time);
        moveToTheNext = true;
    }

  

}
