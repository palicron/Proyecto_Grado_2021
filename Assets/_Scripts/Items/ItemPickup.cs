using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    bool pickedUp = false;
    void OnTriggerEnter(Collider collider)
    {

        if(collider.gameObject.tag == "Player" && !pickedUp)
        {
            pickedUp = Inventory.instance.Add(item);
            if(pickedUp)
            {
                gameObject.GetComponent<ParticleSystem>().Stop();
                StartCoroutine(DestroyObject());
                gameObject.transform.localScale = new Vector3(0,0,0);
            }
        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
