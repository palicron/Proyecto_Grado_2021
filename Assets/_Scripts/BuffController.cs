using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{

    private PlayerCtr scriptPlayer;
    healthsystems hs = null;
    public BuffType type;
    public bool isHealing = false;

    public enum BuffType
    {
        SPEED,
        HEALTH,
        JUMP,
        DASH,
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            // other.gameObject.SetActive(false);
            scriptPlayer = other.gameObject.GetComponent<PlayerCtr>();
            if (scriptPlayer != null)
            {
                switch (type)
                {

                    case BuffType.SPEED:
                        scriptPlayer.CurrentSpeed = 25f;
                        break;

                    case BuffType.DASH:
                        scriptPlayer.DashDistance = 5f;
                        break;

                    case BuffType.JUMP:
                        scriptPlayer.JumpForce = 25f;
                        break;

                }

            }
        }
    }


    void OnTriggerStay(Collider other)
    {
       
        scriptPlayer = other.gameObject.GetComponent<PlayerCtr>();
        if (scriptPlayer != null)
        {
            hs = other.GetComponent<healthsystems>();
            switch (type)
            {

                case BuffType.HEALTH:
                    isHealing = true;
                    hs.Heal(5f);
                    break;

            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (scriptPlayer != null)
            {

                switch (type)
                {

                    case BuffType.SPEED:
                        scriptPlayer.CurrentSpeed = scriptPlayer.Speed;
                        break;

                    case BuffType.DASH:
                        scriptPlayer.DashDistance = scriptPlayer.InitialDashDistance;
                        break;

                    case BuffType.JUMP:
                        scriptPlayer.JumpForce = scriptPlayer.InitialJumpForce;
                        break;
                }

            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
