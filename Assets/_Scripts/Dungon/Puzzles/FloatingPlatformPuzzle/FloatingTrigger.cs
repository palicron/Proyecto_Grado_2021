using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTrigger : MonoBehaviour
{
    [Header("Floating Option")]
    public FloatingOption optionActual;
    public FloatingOption[] allOptions;
    public FloatingManager manager;
    private void Start()
    {
        optionActual = transform.GetComponentInParent<FloatingOption>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!optionActual.correct) 
            {
                optionActual.active = true;
            }
            else {

                foreach (FloatingOption fOption in allOptions) 
                {
                    if (!fOption.correct)
                    {
                        fOption.active = true;
                    }
                }

                StartCoroutine(MoveMovingObject(optionActual.waitTime));
            }
        }

    }

    IEnumerator MoveMovingObject(float time)
    {
        yield return new WaitForSeconds(time);
        if (manager != null)
        {
            manager.active = true;
        }
        optionActual.active = true;
    }
}
