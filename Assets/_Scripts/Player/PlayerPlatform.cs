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
    public bool moviendose;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        tagface = other.tag;
        if (other.tag =="Platform") 
        {
            if (!isJump) 
            {   
                  
                    GameObject inGround = other.gameObject;
                    groundName = inGround.name;
                    groundPos = inGround.transform.position;
                    PlaftormController speed = other.gameObject.GetComponent<PlaftormController>();
                    if (groundPos != lastGroundPos && groundName == lastgroundName) 
                    {
                        moviendose = true;
                        CurrentPos = Vector3.zero;
                        CurrentPos += groundPos- lastGroundPos;
 
                        playerController.move(CurrentPos.x, CurrentPos.y);
                    }
                    playerController.CurrentSpeed = playerController.Speed;
                    lastgroundName = groundName;
                    lastGroundPos = groundPos;
                
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

}
