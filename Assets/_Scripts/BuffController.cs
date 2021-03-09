using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{

    private PlayerCtr scriptPlayer;
    healthsystems hs = null;
    public BuffType type;
    [Header("Buff Values")]
    public float SpeedBuff;
    public float HealingBuff;
    public float JumpBuff;
    public float DashBuff;
    public bool IsBuffed = false;
  


    public enum BuffType
    {
        SPEED,
        HEALTH,
        JUMP,
        DASH,
    }

     void Start()
    {
        SpeedBuff = 35f;
        HealingBuff = 10f;
        JumpBuff = 20f;
        DashBuff = 5f;
        
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
                        scriptPlayer.CurrentSpeed = SpeedBuff;
                        IsBuffed = true;
                        break;

                    case BuffType.DASH:
                        scriptPlayer.DashDistance = DashBuff;
                        IsBuffed = true;
                        break;

                    case BuffType.JUMP:
                        scriptPlayer.JumpForce = JumpBuff;
                        IsBuffed = true;
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
                    IsBuffed = true;
                    hs.Heal(HealingBuff);
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
                        IsBuffed = false;
                        break;

                    case BuffType.DASH:
                        scriptPlayer.DashDistance = scriptPlayer.InitialDashDistance;
                        IsBuffed = false;
                        break;

                    case BuffType.JUMP:
                        scriptPlayer.JumpForce = scriptPlayer.InitialJumpForce;
                        IsBuffed = false;
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
