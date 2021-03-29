using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaftormController : MonoBehaviour
{
    [Header("Platform Dependences")]
    public Inventory PlayerInventory;
    public EquipmentManager Equipment;
    public PlayerScore Score;
    public Rigidbody platformRB;
    public Rigidbody playerRigid;
    public bool playerOnPlat;
    public PlayerCtr plactr;
    [Header("Platform Description")]
    public PlatformType type;
    [Header("Platform Movement")]
    public float platformSpeed;
    public float waitTime;
    public Vector3 VelocityVector;
    [Header("Platform Rotations")]
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    [Header("Platform Array")]
    public Transform[] positions;
    public float[] speedVariation;
    public Transform[] rotations;
    private int actualPosition = 0;
    private int nextposition = 1;
    private int actualSpeed=0;
    public bool moveToTheNext = true;
    public bool active = false;
    public float verify=0;
    

    public enum PlatformType{ 
        NORMAL,
        TRANSLATEPATH,
        TRIGGERTRANSLATE,
        TRIGGEREXIT,
        ROTATIVE,
        ROTATIVETRIGGER,
        MULTIPLESPEED,
        MOVEMENTESCREENTRIGGERED,
        TRANSLATEMOVEMENT
    }

    void Start()
    {
        plactr = GameObject.Find("Player").GetComponent<PlayerCtr>();
        PlayerInventory = GameObject.Find("Player").GetComponent<Inventory>() ;
        Equipment = GameObject.Find("Player").GetComponent<EquipmentManager>();
        Score = GameObject.Find("Player").GetComponent<PlayerScore>();
        playerOnPlat = false;
    }


    void Awake() {
        if (type == PlatformType.TRANSLATEPATH || type == PlatformType.ROTATIVE || type == PlatformType.MULTIPLESPEED) { active = true; }
    }

    private void OnCollisionExit(Collision collision)
    {
        playerOnPlat = false;
        playerRigid = null;
        plactr.CurrentSpeed = plactr.Speed;
    }


    private void OnCollisionEnter(Collision collision)
    {
        playerOnPlat = true;
        playerRigid = collision.gameObject.GetComponent<Rigidbody>();
       
    }




    private void FixedUpdate()
    {
        VelocityVector = platformRB.velocity;

        if (active)
        {
            MovePlatform();
        }
        else if (type == PlatformType.ROTATIVETRIGGER)
        {
            platformRB.DORotate(new Vector3(0, 0, 0), 0.5f, RotateMode.Fast);

        }
        else if (type == PlatformType.TRIGGEREXIT)
        {
            //RESETEA LA PLATAFORMA AL PRIMER PUNTO DONDE SE POSICIONO 
            platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[0].position, platformSpeed * Time.deltaTime));
        }

    }

    void MovePlatform() 
    {
        //TRANSLATE PLATFORM MOVES THROUGH DIFERENT POINTS WITHOUT ANY TRIGGER 
        //TRIGGER TRANSLATE PLATFORM MOVES THROUGH DIFERENT POINTS WITH A TRIGGER 
        //TIGGER EXIT TRANSLATE PLATFORM MOVES THROUGH DIFERENT POINTS WITH A TRIGGER THAT STOPS WHEN PLAYER GETS OUT THE PLATFORM
        if (type == PlatformType.TRANSLATEPATH || type == PlatformType.TRIGGERTRANSLATE || type == PlatformType.TRIGGEREXIT)
        {
            if (moveToTheNext)
            {
                StopCoroutine(WaitForMove(0));
                platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[nextposition].position, platformSpeed * Time.deltaTime));
                if (playerOnPlat)
                {

                    float xComponent = playerRigid.velocity.x;
                    float yComponent = playerRigid.velocity.y;
                    float ZComponent = playerRigid.velocity.z;
                    Vector3 newVelocity = new Vector3(xComponent, yComponent, ZComponent);
                    if (plactr.Xvel == 0 && plactr.Yvel == 0)
                    {
                        xComponent = VelocityVector.x;
                        yComponent = playerRigid.velocity.y;
                        ZComponent = VelocityVector.z;
                        newVelocity = new Vector3(xComponent, yComponent, ZComponent);

                    }
                    else if (plactr.Xvel > 0 || plactr.Yvel > 0)
                    {
                         xComponent = playerRigid.velocity.x+ platformRB.velocity.x;
                         yComponent = playerRigid.velocity.y;
                         ZComponent = playerRigid.velocity.z + platformRB.velocity.x;
                        newVelocity = new Vector3(xComponent, yComponent, ZComponent);

                    }
                    playerRigid.velocity = newVelocity;

                }
            }

            if (Vector3.Distance(platformRB.position, positions[nextposition].position) <= 0)
            {
                if (playerOnPlat)
                {
                    playerRigid.velocity = new Vector3(0, 0, 0);
                }
                StartCoroutine(WaitForMove(waitTime));
                actualPosition = nextposition;
                nextposition++;
                if (nextposition > positions.Length - 1)
                {
                    nextposition = 0;
                }
            }
        }
        //PLATFORM THAT CHANGES THE SPEED DEPENDENDING IN THE POINTS IT IS TRAVELING 
        else if (type == PlatformType.MULTIPLESPEED)
        {
            if (speedVariation.Length != 0)
            {
                if (moveToTheNext)
                {
                    StopCoroutine(WaitForMove(0));
                    platformSpeed = speedVariation[actualSpeed];
                    platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[nextposition].position, platformSpeed * Time.deltaTime));
                }

                if (Vector3.Distance(platformRB.position, positions[nextposition].position) <= 0)
                {
                    StartCoroutine(WaitForMove(waitTime));
                    actualPosition = nextposition;
                    actualSpeed++;
                    nextposition++;

                    if (nextposition > positions.Length - 1)
                    {
                        nextposition = 0;
                    }
                    if (actualSpeed > speedVariation.Length - 1)
                    {
                        actualSpeed = 0;
                    }
                }
            }

        }
        //PLATFORM THAT ROTATE INTO AN AXIS CONSTANTLY 
        else if (type == PlatformType.ROTATIVE)
        {
            transform.Rotate(new Vector3(rotationX, rotationY, rotationZ) * platformSpeed * Time.deltaTime);

        }
        //PLATFORM THAT ROTATE WHEN THE PLAYER GETS OVER IT, THE PLATFORM ACTS LIKE A ROOFTRAP
        else if (type == PlatformType.ROTATIVETRIGGER)
        {
            platformRB.DORotate(new Vector3(rotationX, rotationY, rotationZ), 0.10f, RotateMode.Fast);

        }
        //PLATFORM THAT MOVES A RIGIDBODY TO A DETERMINED POSITION WHEN IS TRIGERED 
        else if (type == PlatformType.MOVEMENTESCREENTRIGGERED) 
        {
            platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[0].position, platformSpeed * Time.deltaTime));
        }
        else if (type == PlatformType.TRANSLATEMOVEMENT) 
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
                        active = false;
                    }
                }
        }
    }

    IEnumerator WaitForMove(float time) {
        moveToTheNext = false;
        yield return new WaitForSeconds(time);
        moveToTheNext = true;
    }



}
