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
    public Vector3 initialPoint;
    public Vector3 VelocityVector;
    [Header("Platform Rotations")]
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public bool initialDif;
    [Header("Platform Velocity ")]
    public float VelX;
    public float Vely;
    public float Velz;
    public float playerVelx;
    public float playerVely;
    public float playerVelz;
    public float playerDrag;
    [Header("TrapPlatform")]
    public int positionTrap;
    [Header("Button Values")]
    public bool buttonOn;
    [Header("Platform Array")]
    public Transform[] positions;
    public float[] speedVariation;
    public float[] waitVariation;
    public Transform[] rotations;
    private int actualPosition = 0;
    private int nextposition = 1;
    private int actualSpeed=0;
    private int actualWait = 0;
    public bool moveToTheNext = true;
    public bool active = false;
    public float verify=0;
    [Header("banda Trasportadora")]
    public float BandaSpeed=15.0f;
    [SerializeField]
    bool direction=true;
    int directInt = 1;

    public enum PlatformType{ 
        NORMAL,
        TRANSLATEPATH,
        TRIGGERTRANSLATE,
        TRIGGEREXIT,
        ROTATIVE,
        ROTATIVETRIGGER,
        MULTIPLESPEED,
        MOVEMENTESCREENTRIGGERED,
        TRANSLATEMOVEMENT,
        TRANSPORTPLAYER,
        WALLPATHSPEEDVAR,
        ROTATIONTIMER,
        TRAPFALLINGPLATFORM,
        BUTTONPLATFORM,
        TIMEDTRIGGERDMOVE,
       
    }

    void Start()
    {
        initialPoint = this.transform.position;
        plactr = GameObject.Find("Player").GetComponent<PlayerCtr>();
        PlayerInventory = GameObject.Find("Player").GetComponent<Inventory>() ;
        Equipment = GameObject.Find("Player").GetComponent<EquipmentManager>();
        Score = GameObject.Find("Player").GetComponent<PlayerScore>();
        playerOnPlat = false;
        buttonOn = false;
        positionTrap = 1;
        if(!direction)
        {
            directInt = -1;
        }
    }


    void Awake() {
        if (type == PlatformType.TRANSLATEPATH || type == PlatformType.ROTATIVE || type == PlatformType.MULTIPLESPEED || type == PlatformType.TRANSPORTPLAYER || type == PlatformType.ROTATIONTIMER) { active = true; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerOnPlat = true;
        playerRigid = collision.gameObject.GetComponent<Rigidbody>();
        playerRigid.drag = 0.6f;
    }


    private void OnCollisionExit(Collision collision)
    {
        if(playerRigid!=null){playerRigid.drag = 1f;}
        playerOnPlat = false;
        playerRigid = null;
    }
    private void FixedUpdate()
    {
        VelocityVector = platformRB.velocity;

        if (active)
        {
            MovePlatform();
        }
        else if (type == PlatformType.ROTATIONTIMER)
        {
            platformRB.DORotate(new Vector3(0, 0, 0), platformSpeed / 2, RotateMode.Fast);

        }
        //Retorna el Boton a la posicion para ser activado
        else if (type == PlatformType.BUTTONPLATFORM)
        {
            platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[0].position, platformSpeed*Time.deltaTime));
            if (speedVariation.Length != 0) { platformSpeed = speedVariation[1]; }

        }
        else if (type == PlatformType.TRIGGEREXIT)
        {
            if (speedVariation.Length!=0)
            {
                platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[0].position, speedVariation[0] * Time.deltaTime));
            }
            else {
                platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[0].position, platformSpeed * Time.deltaTime));

            }

        }
        else if (type == PlatformType.TIMEDTRIGGERDMOVE)
        {
            platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[0].position, platformSpeed * Time.deltaTime));

        }
        else if (type == PlatformType.TRAPFALLINGPLATFORM)
        {
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
                    playerVelx = VelocityVector.x;
                    playerVelz = VelocityVector.z;
                    if (plactr.Xvel != 0 || plactr.Yvel != 0)
                    {
                        playerVelx = playerRigid.velocity.x;
                        playerVelz = playerRigid.velocity.z;
                    }
                    Vector3 newVelocity = new Vector3(playerVelx, playerRigid.velocity.y, playerVelz);
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
                    if (playerOnPlat)
                     if (playerOnPlat)
                {
                    playerVelx = VelocityVector.x;
                    playerVelz = VelocityVector.z;
                    if (plactr.Xvel != 0 || plactr.Yvel != 0)
                    {
                        playerVelx = playerRigid.velocity.x;
                        playerVelz = playerRigid.velocity.z;
                    }
                    Vector3 newVelocity = new Vector3(playerVelx, playerRigid.velocity.y, playerVelz);
                    playerRigid.velocity = newVelocity;

                }
                }

                if (Vector3.Distance(platformRB.position, positions[nextposition].position) <= 0)
                {

                    if (playerOnPlat)
                    {
                        playerRigid.velocity = new Vector3(0, 0, 0);
                    }
                    if (waitVariation.Length != 0)
                    {
                        waitTime = waitVariation[actualSpeed];
                        StartCoroutine(WaitForMove(waitVariation[actualSpeed]));
                    }
                    else 
                    {
                        StartCoroutine(WaitForMove(waitTime));
                    }
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
            platformRB.DORotate(new Vector3(rotationX, rotationY, rotationZ), platformSpeed, RotateMode.Fast);

        }
        //PLATFORM THAT ROTATES DURING CERTAIN TIME
        else if (type == PlatformType.ROTATIONTIMER)
        {
            platformRB.DORotate(new Vector3(rotationX, rotationY, rotationZ), platformSpeed, RotateMode.Fast);
            StartCoroutine(WaitForRotation(waitTime));
 

        }
        //PLATFORM THAT MOVES A RIGIDBODY TO A DETERMINED POSITION WHEN IS TRIGERED 
        else if (type == PlatformType.MOVEMENTESCREENTRIGGERED) 
        {
            platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[0].position, platformSpeed * Time.deltaTime));
            
        }
        else if (type == PlatformType.TIMEDTRIGGERDMOVE)
        {
            platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[1].position, platformSpeed * Time.deltaTime));
            StartCoroutine(WaitForReset(waitTime));

        }
        //Activa el boton en espera de la reactiacion para volver a su posicion inicial
        else if (type == PlatformType.BUTTONPLATFORM)
        {
            platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[1].position, platformSpeed * Time.deltaTime));
            if (speedVariation.Length != 0) { platformSpeed = speedVariation[0]; }

        }
        else if (type == PlatformType.TRANSLATEMOVEMENT) 
        {
                if (moveToTheNext)
                {
                    StopCoroutine(WaitForMove(0));
                if (speedVariation.Length!=0) { platformSpeed = speedVariation[actualSpeed]; }
                if (waitVariation.Length != 0) { waitTime = waitVariation[actualWait]; }
                 platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[nextposition].position, platformSpeed * Time.deltaTime));
                if (playerOnPlat)
                {
                    playerVelx = VelocityVector.x;
                    playerVelz = VelocityVector.z;
                    if (plactr.Xvel != 0 || plactr.Yvel != 0)
                    {
                        playerVelx = playerRigid.velocity.x;
                        playerVelz = playerRigid.velocity.z;
                    }
                    playerVely = playerRigid.velocity.y;
                    Vector3 newVelocity = new Vector3(playerVelx, playerVely, playerVelz);
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
                    actualSpeed++;
                    actualWait++;
                    nextposition++;
                    if (nextposition > positions.Length - 1)
                    {
                    nextposition = 0;
                    active = false;
                    }
                    if (speedVariation.Length != 0)
                    {
                        if (actualSpeed > speedVariation.Length - 1)
                        {
                            actualSpeed = 0;
                        }
                    }
                    if (waitVariation.Length != 0)
                    {
                        if (actualWait > waitVariation.Length - 1)
                        {
                            actualWait = 0;
                        }
                    }

            }
        }
        //TRASNPORT PLATFORM THAT MOVES PLAEYR WITHOUT MOVING
        else if (type == PlatformType.TRANSPORTPLAYER)
        {
            if (playerOnPlat)
            {

                playerRigid.AddForce(transform.right * directInt * BandaSpeed,ForceMode.Acceleration);
            }
           
        }
        //PLATFORM THAT CHANGES THE SPEED DEPENDENDING IN THE POINTS IT IS TRAVELING 
        else if (type == PlatformType.WALLPATHSPEEDVAR)
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
                    if (actualSpeed > speedVariation.Length - 1)
                    {
                        actualSpeed = 0;
                    }
                    if (nextposition > positions.Length - 1)
                    {
                        nextposition = 0;
                        active = false;
                    }
                }
            }

        }

        else if (type == PlatformType.TRAPFALLINGPLATFORM)
        {
            if (positionTrap < positions.Length)
            {
                platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[positionTrap].position, platformSpeed * Time.deltaTime));
            }
            else 
            {
                platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[positions.Length-1].position, platformSpeed * Time.deltaTime));
            }
            if (playerOnPlat)
               {
                    playerVelx = VelocityVector.x;
                    playerVelz = VelocityVector.z;
                    if (plactr.Xvel != 0 || plactr.Yvel != 0)
                    {
                        playerVelx = playerRigid.velocity.x;
                        playerVelz = playerRigid.velocity.z;
                    }
                    Vector3 newVelocity = new Vector3(playerVelx, playerRigid.velocity.y, playerVelz);
                    playerRigid.velocity = newVelocity;
               }
            StartCoroutine(WaitForReset(waitTime));
        }
    }


    IEnumerator WaitForMove(float time) {
        moveToTheNext = false;
        yield return new WaitForSeconds(time);
        moveToTheNext = true;
    }


    IEnumerator WaitForRotation(float time)
    {
        yield return new WaitForSeconds(time/2);
        active = false;
        yield return new WaitForSeconds(time);
        active = true;
    }


    IEnumerator WaitForReset(float time)
    {
        yield return new WaitForSeconds(time);
        active = false;
    }



}
