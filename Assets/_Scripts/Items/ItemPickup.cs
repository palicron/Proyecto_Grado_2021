using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    bool pickedUp = false;

    void Start()
    {
        StartCoroutine(StartP());
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