
/** COdigo encargado solo del movimeinto del jugador**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Cinemachine;
public class PlayerCtr : MonoBehaviour
{

    // Related to equipment and added stats

    int armorModifier = 0;
    int damageModifier = 0;
    int velocityModifier = 0;
    bool hasWeapon = false;
    bool menuOpen = false;
    UI_Status menuStatus;
    float slowFall = 1;

    GameObject CurrentWeaponPb;
    public void ModifyStats(int a, int d, int v)
    {
        armorModifier = a;
        damageModifier = d;
        velocityModifier = v;
    }

    public void SetWeapon(GameObject pWeapon, bool equip)
    {
        hasWeapon = equip;
        if (WeaponVisual != null)
        {
            Destroy(WeaponVisual);
            CurrentWeaponPb = null;
        }
        if (pWeapon != null)
        {
            GameObject aWeapon = Instantiate(pWeapon);
            CurrentWeaponPb = pWeapon;
            aWeapon.transform.SetParent(Weapon.transform, false);
            WeaponVisual = aWeapon;
        }
    }

    public void SetBoots(int i)
    {
        bootsTextures.SetTexture(i);
        if(i == 2)
        {
            canDoubleJump = true;
        }
        else
        {
            canDoubleJump = false;
        }
    }

    public GameObject getWeapon()
    {
        return CurrentWeaponPb;
    }

    public void SetTool(GameObject pTool, bool equip)
    {
        if (ToolVisual != null || !equip)
        {
            Destroy(ToolVisual);
        }
        if (pTool != null)
        {
            GameObject aTool = Instantiate(pTool);
            aTool.transform.SetParent(Tool.transform, false);
            ToolVisual = aTool;
        }
    }

    public void SetHead(GameObject pHead, bool equip, int rarity)
    {
        if (HeadVisual != null || !equip)
        {
            Destroy(HeadVisual);
            slowFall = 1;
        }
        if (pHead != null)
        {
            GameObject aHead = Instantiate(pHead);
            aHead.transform.SetParent(Head.transform, false);
            HeadVisual = aHead;
            if(rarity == 2)
            {
                slowFall = .001F;
            }
        }
    }

    void changeMenuStatus()
    {
        menuOpen = menuStatus.IsMenuOpen();
        if(menuOpen)
        {
            Cursor.lockState = CursorLockMode.Confined;
            freeLockCamera.m_YAxis.m_MaxSpeed = 0;
            freeLockCamera.m_XAxis.m_MaxSpeed = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            freeLockCamera.m_YAxis.m_MaxSpeed = CamYvel;
            freeLockCamera.m_XAxis.m_MaxSpeed = CamXvel;
        }
    }

    // --------------------------------
    public Interactable focus;
    [Header("Player Movement")]
    // Start is called before the first frame update
    [SerializeField]
    public float Speed = 1.0f;
    public float CurrentSpeed;
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
    public float DashDistance = 4f;
    [SerializeField, Range(0, 10.0f)]
    public float InitialDashDistance = 4f;
    [SerializeField, Range(0, 10.0f)]
    public float DashSpeedMultiplied;
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
    public float InitialJumpForce = 15.0f;
    [SerializeField]
    public float JumpForce= 15.0f;
    [SerializeField]
    float NoGroundDownForce = 10.0f;
    [SerializeField]
    ForceMode ForceDownTipe;
    Animator animator;
    Vector3 MovementVec = Vector3.zero;
    Rigidbody rb;
    healthsystems healthsystems;
    public float Xvel = 0;
    public float Yvel = 0;
    bool Indash = false;
    public bool isGrounded = false;
    bool canMove = true;
    
    
    int jumpNum = 0;
    bool WallInfront = false;
    [Header("Equipment Abilitis")]
    public bool canDoubleJump;
    //Hacer el get y set bien
    public bool CanControlPlayer = true;

    Vector3 curDir = Vector3.zero;
    float MovementControl;
    [SerializeField, Range(0, 20.0f)]
    float LerpingVelocity = 15.0f;
    Vector3 curvel;
    CapsuleCollider col;
    [Header("Componentes")]
    [SerializeField]
    GameObject PlayerVcam;
    [SerializeField]
    GameObject PlayerCamera;
    CinemachineVirtualCamera DialogueVcam;
    [SerializeField]
    CinemachineFreeLook freeLockCamera;

    bool CanAdvanceAttack = true;
    bool nextAttack = true;
    public GameObject Tool;
    GameObject ToolVisual;
    public GameObject Weapon;
    GameObject WeaponVisual;
    public GameObject Head;
    GameObject HeadVisual;
    private float CamYvel;
    private float CamXvel;
    private BootsTexture bootsTextures;
    void Start()
    {
        bootsTextures = gameObject.GetComponent<BootsTexture>();
        menuStatus = UI_Status.instance;
        menuStatus.onMenusChangedCallBack += changeMenuStatus;
        CurrentSpeed = Speed;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        MovementControl = GroundControl;
        healthsystems = GetComponent<healthsystems>();
        DialogueVcam = PlayerVcam.GetComponent<CinemachineVirtualCamera>();
        Cursor.lockState = CursorLockMode.Locked;
        CamYvel = freeLockCamera.m_YAxis.m_MaxSpeed;
        CamXvel = freeLockCamera.m_XAxis.m_MaxSpeed;
        if (healthsystems)
        {
            healthsystems.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CanAdvanceAttack);
        Xvel = Input.GetAxisRaw("Horizontal");
        Yvel = Input.GetAxisRaw("Vertical");
        curvel = rb.velocity;
        curvel.y = 0;
        if(Mathf.Abs(Xvel)<=0.01 && Mathf.Abs(Yvel) <= 0.01)
        {
            animator.SetFloat("HorizontalSpeed",0);
            if(isGrounded)
            {
                rb.drag = 1.0f;
            }
            
        }
        else
        {
            rb.drag = 0.8f;
            animator.SetFloat("HorizontalSpeed", Mathf.Abs(curvel.magnitude / MaxSpeed));

        }


        if (!CanControlPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && canMove && !Indash)
        {

            curDir = transform.forward.normalized;
            Dash();
        }
 

        animator.SetBool("Rolling", Indash);
        if (Input.GetButtonDown("Jump") )
        {
            jump();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.PauseGame();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(hasWeapon && !menuOpen)
            {
                NextAttack();
            }
        }

       
    }

    private void FixedUpdate()
    {

        GroundCheck();
        if (!isGrounded)
        {
            rb.AddForce(Vector2.down * slowFall * NoGroundDownForce*Time.deltaTime, ForceDownTipe);
        }

        if (!CanControlPlayer)
            return;
        if ((Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0) && canMove)
        {
            move(Xvel, Yvel);
        }

    }


    public void move(float Xmove, float Ymove)
    {

            Vector3 Cforward = PlayerCamera.transform.right;
            Cforward.y = 0;
            Cforward *= Xmove;
            Vector3 Croright = PlayerCamera.transform.forward;
            Croright.y = 0;
            Croright *= Ymove;

            MovementVec = Cforward + Croright;
           // transform.forward = Vector3.Lerp(MovementVec.normalized, transform.forward, LerpingVelocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MovementVec.normalized), Time.deltaTime * LerpingVelocity);
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
        if(isGrounded)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            jumpNum++;
        }
        else if(!isGrounded && canDoubleJump && jumpNum<1)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            jumpNum++;
        }

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
            jumpNum = 0;
           
        }
        else
        {
            isGrounded = false;
            MovementControl = AirControl;
            LastAttack();
         
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
      
    }

    public void ApliPlayerFoce(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Acceleration);
    }
    //@TODO: cambiarlo fb


    public void SetDialogue(bool state, Vector3 lookpos, Transform tolook = null)
    {
        if (state)
        {
            CanControlPlayer = false;
            lookpos.y = transform.position.y;
            transform.LookAt(lookpos);
            PlayerVcam.SetActive(true);
    
            DialogueVcam.LookAt = tolook;
        }
        else
        {
            CanControlPlayer = true;
            PlayerVcam.SetActive(false);
            DialogueVcam.LookAt = null; 
        }
    }


    IEnumerator StarDash()
    {
        Indash = true;
        bool crash = false;
        Vector3 StarPos = transform.position;
        bool cantmove = false;
        MaxSpeed *= DashSpeedMultiplied;
        col.height = col.height / 2;
        col.center = new Vector3(0, 0.5f, 0);
        while (Vector3.Distance(StarPos, transform.position) <= DashDistance && !crash)
        {
            DashForce();
            if (WallInfront || cantmove)
            {
                col.height = col.height * 2;
                col.center = new Vector3(0, 1f, 0);
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
        MaxSpeed *= (1 / DashSpeedMultiplied);
        if (!crash)
        {
            col.height = col.height * 2;
            col.center = new Vector3(0, 1f, 0);
            rb.velocity = rb.velocity.normalized * MaxSpeed;
            rb.AddForce(curDir * 10, ForceMode.VelocityChange);
        }
        animator.ResetTrigger("Chrashing");
        Indash = false;

    }

    public void ResetAttack()
    {
        if(!nextAttack)
        {
            animator.SetInteger("Attack", 0);
            CanAdvanceAttack = true;
            nextAttack = false;
            Weapon.SetActive(false);
        }


    }

    public void NextAttack()
    {
        Debug.Log(CanAdvanceAttack+" "+ !Indash);
        if(CanAdvanceAttack && !Indash)
        {
            Weapon.SetActive(true);
            animator.SetInteger("Attack", animator.GetInteger("Attack") + 1);
            CanAdvanceAttack = false;
            nextAttack = true;
        }
       
    }

    public void enableAttack()
    {
        CanAdvanceAttack = true;
        nextAttack = false;
    }

    public void DisableAttack()
    {
      
        CanAdvanceAttack = false;
      

    }

    public void LastAttack()
    {
        animator.SetInteger("Attack", 0);
        CanAdvanceAttack = true;
        nextAttack = false;
        Weapon.SetActive(false);

    }

    public void cancelAllMovment()
    {
        rb.velocity = Vector3.zero;
        if(Indash)
        {
            StopAllCoroutines();
            Indash = false;
            col.height = col.height * 2;
            col.center = new Vector3(0, 1f, 0);
        }
    }
}

