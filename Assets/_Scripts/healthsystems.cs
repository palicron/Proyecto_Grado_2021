/**clase abstracta que respresenta un sitema de salud de un
actor dentro del mundo, el actor peude recivir daño o curase
y cuando su vida llega  a 0 este muere**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class healthsystems : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected float MaxHelath = 100.0f;
    protected float currentHealh;

    public abstract void Init();
    public virtual void TakeDmg(float Damage)
    {

        currentHealh -= Mathf.Clamp((currentHealh - Damage), 0, MaxHelath);
        if (currentHealh <= 0)
        {
            Death();
        }

    }


    public virtual void Heal(float heal)
    {

        currentHealh += Mathf.Clamp((currentHealh + heal), 0, MaxHelath);

    }
    public abstract void Death();


}
