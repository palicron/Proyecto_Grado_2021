using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    [SerializeField]
    GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {
        GameObject item = Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);
        item.transform.parent = gameObject.transform;
    }
}
