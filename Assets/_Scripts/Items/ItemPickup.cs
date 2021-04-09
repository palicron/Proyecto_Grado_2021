using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    bool pickedUp = false;
    public bool hover = false;
    float speed = 0.001F;
    float directionSymbol = 1.0F;
    float acceleration = 1.0F;

    void Start()
    {
        StartCoroutine(StartP());
    }

    void Update()
    {
        if(hover)
        {
            if (speed <= 0)
            {
                //Debug.Log("Changing direction");
                directionSymbol = (float) (directionSymbol * -1.0);
            }
            if (speed >= 0.3F)
            {
                //Debug.Log("Changing acceleration");
                acceleration = (float) (acceleration * -1.0);
            }
            transform.localPosition += new Vector3(0,speed * directionSymbol * Time.deltaTime, 0);
            speed += (float) (0.002 * directionSymbol * acceleration);
        }
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player" && !pickedUp)
        {
            pickedUp = Inventory.instance.Add(item);
            if (pickedUp)
            {
                UI_SFX.instance.PlayPickUp();
                gameObject.GetComponent<ParticleSystem>().Stop();
                StartCoroutine(DestroyObject());
            }
        }
    }

    IEnumerator DestroyObject()
    {
        Destroy(gameObject.GetComponent<BoxCollider>());
        Destroy(gameObject.GetComponent<BoxCollider>());
        gameObject.GetComponent<Animator>().SetBool("pickedUp", true);
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    IEnumerator StartP()
    {
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<Animator>().SetBool("start", true);
    }
}