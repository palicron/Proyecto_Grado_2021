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
    }

    public override void Death()
    {
        throw new System.NotImplementedException();
    }

 
}
