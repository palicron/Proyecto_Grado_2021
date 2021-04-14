using UnityEngine;

public class FloatingOption : MonoBehaviour
{
    [Header("Platform characteristics")]
    public Rigidbody optionRB;
    public Transform movementPoint;
    public float speed;
    [Header("Trigger information")]
    public bool active;
    public bool correct;
    public bool playerOnPlat;
    public Rigidbody playerRigid;
    public PlayerCtr plactr;
    [Header("Platform Movement")]
    public float waitTime;
    [Header("Platform Velocity ")]
    public float playerVelx;
    public float playerVely;
    public float playerVelz;



    private void Start()
    {
        plactr = GameObject.Find("Player").GetComponent<PlayerCtr>();
        playerOnPlat = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerOnPlat = true;
        playerRigid = collision.gameObject.GetComponent<Rigidbody>();
        playerRigid.drag = 0.6f;
    }


    private void OnCollisionExit(Collision collision)
    {
        if (playerRigid != null) { playerRigid.drag = 1f; }
        playerOnPlat = false;
        playerRigid = null;
    }


    // Update is called once per frame
    void Update()
    {
        if(active) 
        {
            moveOption();
        }
    }

    void moveOption() 
    {
        optionRB.MovePosition(Vector3.MoveTowards(optionRB.position,movementPoint.position,speed* Time.deltaTime));
        if (playerOnPlat)
        {
            playerVelx = optionRB.velocity.x;
            playerVelz = optionRB.velocity.z;
            if (plactr.Xvel != 0 || plactr.Yvel != 0)
            {
                playerVelx = playerRigid.velocity.x;
                playerVelz = playerRigid.velocity.z;
            }
            Vector3 newVelocity = new Vector3(playerVelx, playerRigid.velocity.y, playerVelz);
            playerRigid.velocity = newVelocity;

        }
    }
}
