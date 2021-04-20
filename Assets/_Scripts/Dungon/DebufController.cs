using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebufController : MonoBehaviour
{

    private PlayerCtr scriptPlayer;
    public healthsystems hs = null;
    public DebufType type;

    [Header("DEBUFF VALUES")]
    public float DammageDebuff;
    public float SlowDebuff;
    public float JumpDebuff;
    public float DashDebuff;
    public bool IsDebuffed = false;

    [Header("TRAP VALUES")]
    public float BackForce;
    public bool IsDammageReal = false;


    public enum DebufType{
        SLOW,
        POISON,
        JUMP,
        DASH,
        STATICTRAMP
    }


    void OnTriggerEnter(Collider other) 
    {

        if (other.gameObject.tag == "Player") 
        {
            // other.gameObject.SetActive(false);
            IsDebuffed = true;
            scriptPlayer = other.gameObject.GetComponent<PlayerCtr>();
            if (scriptPlayer != null)
            {
                switch (type){

                    case DebufType.SLOW:
                        scriptPlayer.CurrentSpeed = SlowDebuff;
                        IsDebuffed = true;
                        break;

                    case DebufType.DASH:
                        scriptPlayer.DashDistance = DashDebuff;
                        IsDebuffed = true;
                        break;

                    case DebufType.JUMP:
                        scriptPlayer.JumpForce = JumpDebuff;
                        IsDebuffed = true;
                        break;


                    case DebufType.STATICTRAMP:

                        hs = other.GetComponent<healthsystems>();
                        if (hs)
                        {
                            hs.TakeDmg(5f);
                            Vector3 vector = -(other.GetComponent<Rigidbody>().velocity.normalized);
                            other.GetComponent<Rigidbody>().AddForce(vector * BackForce, ForceMode.Impulse);

                        }
                        hs = null;
                        break;
                }
               
            }
        }
    }


    void OnTriggerStay(Collider other)
    {
        scriptPlayer = other.gameObject.GetComponent<PlayerCtr>();
            if (scriptPlayer != null )
            {
            hs = other.GetComponent<healthsystems>();
            switch (type)
                {

                    case DebufType.POISON:
                    IsDammageReal = true;
                    hs.TakeDmg(DammageDebuff);
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

                    case DebufType.SLOW:
                        scriptPlayer.CurrentSpeed = scriptPlayer.Speed;
                        IsDebuffed = false;
                        break;

                    case DebufType.DASH:
                        scriptPlayer.DashDistance = scriptPlayer.InitialDashDistance;
                        IsDebuffed = false;
                        break;

                    case DebufType.JUMP:
                        scriptPlayer.JumpForce = scriptPlayer.InitialJumpForce;
                        IsDebuffed = false;
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
