using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebufController : MonoBehaviour
{

    private PlayerCtr scriptPlayer;
    healthsystems hs = null;
    public DebufType type;


    public enum DebufType{
        SLOW,
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
                switch (type){

                    case DebufType.SLOW:
                        scriptPlayer.CurrentSpeed = 10f;
                        break;

                    case DebufType.DASH:
                        scriptPlayer.DashDistance = 1f;
                        break;

                    case DebufType.JUMP:
                        scriptPlayer.JumpForce = 5f;
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

                    case DebufType.HEALTH:
                        hs.TakeDmg(3f);
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
                        break;

                    case DebufType.DASH:
                        scriptPlayer.DashDistance = scriptPlayer.InitialDashDistance;
                        break;

                    case DebufType.JUMP:
                        scriptPlayer.JumpForce = scriptPlayer.InitialJumpForce;
                        break;
                }
                
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
