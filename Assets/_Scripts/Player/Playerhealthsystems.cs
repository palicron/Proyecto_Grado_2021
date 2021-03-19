/** clase heradada del sistema de salud encargado de la vida
del jugador **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerhealthsystems : healthsystems
{

       public override void Init()
    {
        currentHealh = MaxHelath;
        anim = GetComponent<Animator>();
    }

    public override void Death()
    {
        deathNotify();
        GetComponent<Animator>().SetTrigger("Die");
    }

 
}
