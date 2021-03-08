using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHook : MonoBehaviour
{

    Enemy_AI AI;
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
}
