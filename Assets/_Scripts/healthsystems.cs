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
    [SerializeField]
    protected float currentHealh;
    [SerializeField]
    protected float invisivilitiTime = 1.0f;

    protected float lastTimeHit = 0;
    public abstract void Init();

    //delgados apra update de vida en porcetaje;
    public delegate void OnhealthUpdate(float newlife);
    public OnhealthUpdate healthUpdate;

    public delegate void OnDeathNotify();
    public  OnDeathNotify deathNotify;
    public GameObject particle;

    protected Animator anim;
    public virtual void TakeDmg(float Damage)
    {

        anim.SetTrigger("Hit");

        if (Time.time - lastTimeHit > invisivilitiTime)
        {
            lastTimeHit = Time.time;
            currentHealh = Mathf.Clamp((currentHealh - Damage), 0, MaxHelath);
            GameObject.Instantiate(particle, this.gameObject.transform.position+ new Vector3(0,0,0.8f), Quaternion.identity);
            if(this.gameObject.tag.Equals("Player"))
            {
                healthUpdate(getHealthPorcentage());
            }
         
            if (currentHealh <= 0)
            {
        
                Death();
            }
            GetComponent<Animator>().ResetTrigger("Hit");
        }

    }

    public virtual void Heal(float heal)
    {

        if (Time.time - lastTimeHit > invisivilitiTime)
        {
            lastTimeHit = Time.time;
            currentHealh += Mathf.Clamp((currentHealh + heal), 0, MaxHelath);
            healthUpdate(-getHealthPorcentage());
        }
      
    }
    public abstract void Death();

    public virtual float getHealthPorcentage()
    {
        return currentHealh / MaxHelath;
    }


}
