
/** COdigo encargado solo del movimeinto del jugador**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtr : MonoBehaviour
{



    public Interactable focus;
    [Header("Player Movement")]
    // Start is called before the first frame update
    [SerializeField]
    float Speed = 1.0f;
    float CurrentSpeed;
    [SerializeField]
    float MaxSpeed = 8.0f;
    [SerializeField, Range(0, 1.0f)]
    float GroundControl = 1.0f;
    [SerializeField, Range(0, 1.0f)]
    float AirControl = 0.5f;
    [SerializeField]
    ForceMode WalkForceMode;
    [Header("Player Dash")]

    [SerializeField, Range(0, 10.0f)]
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

    [Header("Player Jump")]
    [SerializeField]
    float JumpForce = 15.0f;
    Animator animator;
    Vector3 MovementVec = Vector3.zero;
    Rigidbody rb;
    healthsystems healthsystems;
    float Xvel = 0;
    float Yvel = 0;
    bool Indash = false;
    bool isGrounded = false;
    bool canMove = true;
    bool WallInfront = false;
    //Hacer el get y set bien
    public bool CanControlPlayer = true;
    Vector3 curDir = Vector3.zero;
    float MovementControl;

    float LerpingVelocity = 0.4f;

    CapsuleCollider col;

    [SerializeField]
    Vector3 LastGroundedPos;
    void Start()
    {
        CurrentSpeed = Speed;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        MovementControl = GroundControl;
        healthsystems = GetComponent<healthsystems>();
        if (healthsystems)
        {
            healthsystems.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {

        Xvel = Input.GetAxisRaw("Horizontal");
        Yvel = Input.GetAxisRaw("Vertical");
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(rb.velocity.magnitude / MaxSpeed));
       
       
        if (!CanControlPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && canMove && !Indash)
        {

            curDir = transform.forward.normalized;
            Dash();
        }
        else
        {
            rb.drag = 2.0f;

        }

        animator.SetBool("Rolling", Indash);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.PauseGame();
        }

    }

    private void FixedUpdate()
    {

        GroundCheck();
        if (!isGrounded)
        {
            rb.AddForce(Vector2.down * 10f);
        }

          if (!CanControlPlayer)
            return;
        if ((Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0) && canMove)
        {
            move(Xvel, Yvel);
        }

    }


    void move(float Xmove, float Ymove)
    {

        MovementVec.x = Xmove;
        MovementVec.y = 0;
        MovementVec.z = Ymove;

        transform.forward = Vector3.Lerp(MovementVec.normalized, transform.forward, LerpingVelocity);
        rb.AddForce(transform.forward * CurrentSpeed * MovementControl * Time.deltaTime, WalkForceMode);
        Vector3 rr = rb.velocity;
        rr.y = 0;
        if (rr.magnitude >= MaxSpeed)
        {
            Vector3 NewSpeed = rb.velocity.normalized * MaxSpeed;
            NewSpeed.y = rb.velocity.y;
            rb.velocity = NewSpeed;
        }
    }
    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
    }

    void Dash()
    {
        StartCoroutine(StarDash());
    }

    void DashForce()
    {
        transform.forward = curDir;
        rb.AddForce(curDir * (CurrentSpeed * DashSpeedMultiplied) * Time.deltaTime, WalkForceMode);
    }


    void jump()
    {
        animator.SetTrigger("Jump");
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }
    void GroundCheck()
    {
        //RaycastHit hit;
        Vector3 down = new Vector3(0, 0, -80);
        if (Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z)
        , col.radius, WhatIsGround))
        //if (Physics.CapsuleCast(transform.position + new Vector3(0, 0.5f, 0), transform.position + new Vector3(0, 0.5f, 0) + down, 0.6f, Vector3.down, 1f, WhatIsGround))
        {
            isGrounded = true;
            MovementControl = GroundControl;
            LerpingVelocity = 0.4f;
            LastGroundedPos = transform.position;
        }
        else
        {
            isGrounded = false;
            MovementControl = AirControl;
            LerpingVelocity = 0.55f;
        }
        if (Physics.Linecast(transform.position + new Vector3(0, 0.5f, 0), (transform.position + new Vector3(0, 0.5f, 0)) + (transform.forward * 1), WhatIsWall))
        {
            WallInfront = true;
        }
        else
        {
            WallInfront = false;
        }

        animator.SetBool("Grounded", isGrounded);
        //Debug.Log(LastGroundedPos);
    }

    public void ApliPlayerFoce(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }
    //@TODO: cambiarlo fb

    IEnumerator StarDash()
    {
        Indash = true;
        bool crash = false;
        Vector3 StarPos = transform.position;
        bool cantmove = false;

        MaxSpeed *= DashSpeedMultiplied;
        col.height = col.height / 2;
        while (Vector3.Distance(StarPos, transform.position) <= DashDistance && !crash)
        {

            DashForce();
            if (WallInfront || cantmove)
            {
                animator.SetTrigger("Chrashing");
                cantmove = true;
                canMove = false;
                rb.AddForce(-curDir * rb.velocity.magnitude * BackForceMultipliyer, ForceMode.VelocityChange);
                yield return new WaitForSeconds(DisableTime);
                crash = true;
                canMove = true;

            }
            yield return new WaitForEndOfFrame();
        }
        col.height = col.height * 2;
        MaxSpeed *= (1 / DashSpeedMultiplied);
        if (!crash)
        {
            rb.velocity = rb.velocity.normalized * MaxSpeed;
            rb.AddForce(curDir * 10, ForceMode.VelocityChange);
        }

        animator.ResetTrigger("Chrashing");
        Indash = false;

    }

    public Vector3 GetLastGroundPos(out Vector3 forward)
    {
        forward = curDir;
        return LastGroundedPos;
    }

    public Vector3 GetLastGroundedPos()
    {
        return LastGroundedPos;
    }

}