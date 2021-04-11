using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    [SerializeField]
    GameObject[] items;
    [SerializeField]
    bool isFloating;
    // Start is called before the first frame update
    void Start()
    {
        GameObject item = Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);
        if(isFloating)
        {
            Destroy(item.GetComponent<Rigidbody>());
            item.GetComponent<BoxCollider>().enabled = false;
        }
        item.transform.parent = gameObject.transform;
    }
}
