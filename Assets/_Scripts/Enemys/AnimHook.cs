using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHook : MonoBehaviour
{


    Enemy_AI AI;
    [SerializeField]
    GameObject LeftHandTrigger;
    // Start is called before the first frame update
    void Start()
    {
        AI = GetComponentInParent<Enemy_AI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetAttack()
    {
      
        AI.resetAttack();
    }

    public void resetLooting()
    {
        AI.resetLooting();
    }

    public void LeftHandAttak(int attack)
    {
        if(attack==1)
        {
            LeftHandTrigger.SetActive(true);
        }
        else
        {
            LeftHandTrigger.SetActive(false);
        }
    }

    public void test()
    {
        Debug.Log("ACA ESTOY CARAJO");
    }
}
