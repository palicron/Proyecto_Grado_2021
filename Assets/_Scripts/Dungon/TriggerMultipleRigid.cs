using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerMultipleRigid : MonoBehaviour
{
    public PlaftormController[] platforms;
    public TextMeshPro anuncio;
    public string textoAnuncio;
    public float sizeText;
    public float speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (platforms.Length!=0)
            {
                foreach (PlaftormController pt in platforms)
                {
                    pt.platformSpeed = speed;
                    pt.active = true;
                }
            }
            if (anuncio!=null) 
            {
                anuncio.fontSize = sizeText;
                anuncio.text = textoAnuncio;
            }
        }
    }
}
