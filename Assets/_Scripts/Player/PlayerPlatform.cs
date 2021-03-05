using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatform : MonoBehaviour
{

    public PlayerCtr playerController;

    Vector3 groundPos;
    Vector3 lastGroundPos;
    Vector3 CurrentPos;

    string groundName;
    string lastgroundName;

    bool isJump;
    public string tagface;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerCtr>();
    }

    private void OnTriggerStay(Collider other)
    {
        tagface = other.tag;
        if (other.tag =="Platform") 
        {
            tagface = other.tag;
            if (!isJump) 
            {

             RaycastHit hit;
                if (Physics.SphereCast(transform.position, playerController.col.radius, -transform.up, out hit)) 
                {
                    GameObject inGround = hit.collider.gameObject;
                    groundName = inGround.name;
                    groundPos = inGround.transform.position;

                    if (groundPos != lastGroundPos && groundName == lastgroundName) 
                    {
                        CurrentPos = Vector3.zero;
                        CurrentPos += groundPos - lastGroundPos;
                        playerController.move(CurrentPos.x, CurrentPos.y);
                    
                    }
                    lastgroundName = groundName;
                    lastGroundPos = groundPos;
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (!playerController.isGrounded) 
                {
                    CurrentPos = Vector3.zero;
                    lastGroundPos = Vector3.zero;
                    lastgroundName = null;
                    isJump = true;
                }
            }
            if(playerController.isGrounded )
            {
                isJump = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
