using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtr : MonoBehaviour
{
    [Header("Player Movement")]
    // Start is called before the first frame update
    [SerializeField]
    float Speed = 1.0f;
    float CurrentSpeed;
    [SerializeField]
    float MaxSpeed=8.0f;
    [Header("Player Dash")]

    [SerializeField,Range(0,10.0f)]
    float DashDistance = 4f;
    [SerializeField, Range(0, 10.0f)]
    float DashSpeedMultiplied;
   [SerializeField, Range(0, 1.0f)]
    float DisableTime = 0.5f;
       [SerializeField, Range(0, 5.0f)]
    float BackForceMultipliyer = 2f;
    

    [SerializeField]
    LayerMask WhatIsGround;

    [SerializeField]
    LayerMask WhatIsWall;

    Animator animator;
    Vector3 MovementVec = Vector3.zero;
    Rigidbody rb;
    float Xvel = 0;
    float Yvel = 0;
    bool Indash = false;
    bool isGrounded = false;
    bool canMove = true;
    bool WallInfront = false;
    Vector3 curDir = Vector3.zero;
    void Start()
    {
        CurrentSpeed = Speed;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Xvel = Input.GetAxisRaw("Horizontal");
        Yvel = Input.GetAxisRaw("Vertical");
        animator.SetFloat("HorizontalSpeed",Mathf.Abs(rb.velocity.magnitude/MaxSpeed));
  
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canMove && !Indash) 
        {
            rb.drag = 0.5f;
            curDir = transform.forward.normalized;
            Dash();
        }
        else
        {
            rb.drag =4.0f;
        }
        animator.SetBool("Rolling",Indash);
       
      
    }

    private void FixedUpdate()
    {

        GroundCheck();

        if ((Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0) && canMove && isGrounded)
        {

            move(Xvel, Yvel);
        }

    }


    void move(float Xmove,float Ymove)
    {
   
        MovementVec.x = Xmove;
        MovementVec.y = 0;
        MovementVec.z = Ymove;
        transform.forward = Vector3.Lerp(MovementVec.normalized,transform.forward ,0.4f) ;
        rb.AddForce(transform.forward * CurrentSpeed * Time.deltaTime,ForceMode.VelocityChange);

        if (rb.velocity.magnitude>=MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * MaxSpeed;
        }

    }


    void Dash()
    {
        StartCoroutine(StarDash());
    }

    void DashForce()
    {
        transform.forward = curDir;
        rb.AddForce(curDir * (CurrentSpeed*DashSpeedMultiplied) * Time.deltaTime,ForceMode.Impulse);
    }

    void GroundCheck()
    {
        RaycastHit hit;
        Vector3 down = new Vector3(0, 0, -80);
  
        if (Physics.CapsuleCast(transform.position + new Vector3(0,0.5f,0), transform.position+ new Vector3(0, 0.5f, 0) + down, 0.6f, Vector3.down, 50f, WhatIsGround))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if(Physics.Linecast(transform.position + new Vector3(0,0.5f,0),(transform.position + new Vector3(0,0.5f,0))+(transform.forward*1),WhatIsWall))
        {
          WallInfront =true;
        }
        else
        {
               WallInfront =false;
        }
    }

    //@TODO: cambiarlo fb

    IEnumerator StarDash()
    {
        Indash = true;
        bool crash = false;
        Vector3 StarPos = transform.position;
        bool cantmove = false;
        
        MaxSpeed *=DashSpeedMultiplied;
       
         Debug.Log("aaa");
        while (Vector3.Distance(StarPos,transform.position)<=DashDistance && !crash)
        { 
          
        DashForce();
        if(WallInfront || cantmove )
          {
            animator.SetTrigger("Chrashing");
           cantmove =true;
           canMove =false;
            rb.AddForce(-curDir * rb.velocity.magnitude * BackForceMultipliyer ,ForceMode.VelocityChange);
            yield return new WaitForSeconds(DisableTime);
            crash = true;
            canMove =true;
             
          } 
          yield return new WaitForEndOfFrame();          
        }
        MaxSpeed *= (1/DashSpeedMultiplied);
        if(!crash)
        {
           rb.velocity = rb.velocity.normalized * MaxSpeed;
           rb.AddForce(curDir * 10  ,ForceMode.VelocityChange);
        }
        
        animator.ResetTrigger("Chrashing");
        Indash = false;
    
    }


}
