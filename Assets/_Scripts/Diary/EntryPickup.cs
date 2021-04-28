using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPickup : MonoBehaviour
{
    public DiaryEntry item;
    bool pickedUp = false;
    public bool hover = false;
    float speed = 0.001F;
    float directionSymbol = 1.0F;
    float acceleration = 1.0F;
    public Animator animator;
    public Animator animatorText;


    void Start()
    {
        animator.enabled = false;
        animatorText.SetBool("isInteracting", true);
        gameObject.GetComponent<ParticleSystem>().Pause();
        StartCoroutine(StartP());
    }

    void Update()
    {
        if (hover)
        {
            if (speed <= 0)
            {
                //Debug.Log("Changing direction");
                directionSymbol = (float)(directionSymbol * -1.0);
            }
            if (speed >= 0.3F)
            {
                //Debug.Log("Changing acceleration");
                acceleration = (float)(acceleration * -1.0);
            }
            transform.localPosition += new Vector3(0, speed * directionSymbol * Time.deltaTime, 0);
            speed += (float)(0.002 * directionSymbol * acceleration);
        }
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            RecyclingDiary.instance.AddEntry(item.description, item.title, item.sourceName, item.id);
            animatorText.SetBool("isInteracting", false);
            hover = false;
            UI_SFX.instance.PlayPickUp();
            gameObject.GetComponent<ParticleSystem>().Stop();
            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject()
    {
        animator.SetBool("pickedUp", true);
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    IEnumerator StartP()
    {
        yield return new WaitForSeconds(1.5F);
        gameObject.GetComponent<ParticleSystem>().Play();
        animator.enabled = true;
        animator.applyRootMotion = true;
        animator.SetBool("start", true);
    }
}