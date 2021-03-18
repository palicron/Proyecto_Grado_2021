using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHs : healthsystems
{

    public Enemy_AI ai;
    public override void Death()
    {
        ai.death();
    }

    public override void Init()
    {
        currentHealh = MaxHelath;
      
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
