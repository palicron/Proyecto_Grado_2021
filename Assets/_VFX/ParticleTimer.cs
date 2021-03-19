using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTimer : MonoBehaviour
{

    public float Time = 1.0f;
    void Start()
    {
        StartCoroutine(Destroid());
    }

    IEnumerator Destroid()
    {
        yield return new WaitForSeconds(Time);
        Destroy(this.gameObject);
    }
}
