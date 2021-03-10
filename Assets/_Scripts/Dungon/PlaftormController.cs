using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaftormController : MonoBehaviour
{

    [Header("Platform Description")]
    public Rigidbody platformRB;
    public PlatformType type;
    [Header("Platform Movement")]
    public float platformSpeed;
    public float waitTime;
    [Header("Platform Rotations")]
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    [Header("Platform Array")]
    public Transform[] positions;
    private int actualPosition = 0;
    private int nextposition = 1;
    public bool moveToTheNext = true;
    public bool active = false;
    Quaternion angle_final = Quaternion.Euler(90, 0, 0);
    Quaternion angle_start = Quaternion.Euler(0, 0, 0);
    private Quaternion target;

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
        else if (type == PlatformType.TRIGGEREXIT)
        {
            //RESETEA LA PLATAFORMA AL PRIMER PUNTO DONDE SE POSICIONO 
            platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[0].position,platformSpeed*Time.deltaTime));
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
