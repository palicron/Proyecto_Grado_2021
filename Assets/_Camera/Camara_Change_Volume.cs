using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara_Change_Volume : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 ForwardVector;
    [SerializeField]
    GameObject Vcam1;
    [SerializeField]
    GameObject Vcam2;


    void Start()
    {
        ForwardVector = transform.forward.normalized;
        ForwardVector.y = 0;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            Vector3 pos = other.gameObject.transform.position-transform.position;
            pos.y = 0;
            if(Vector3.Dot(pos.normalized, ForwardVector)>0)
            {
                Vcam2.SetActive(true);
                Vcam1.SetActive(false);
            }
            else
            {
                Vcam1.SetActive(true);
                Vcam2.SetActive(false);
            
            }
            
        }
    }



}
