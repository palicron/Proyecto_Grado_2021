using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeEnemy : MonoBehaviour
{
    [SerializeField]
    float Damage = 5.0f;
    healthsystems hs = null;

    [SerializeField]
    float BackForce = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        hs = other.GetComponent<healthsystems>();
        if (hs)
        {
            hs.TakeDmg(Damage);
            Vector3 vector = -(other.GetComponent<Rigidbody>().velocity.normalized);
            other.GetComponent<Rigidbody>().AddForce(vector * BackForce, ForceMode.Impulse);

        }
        hs = null;


    }
}
