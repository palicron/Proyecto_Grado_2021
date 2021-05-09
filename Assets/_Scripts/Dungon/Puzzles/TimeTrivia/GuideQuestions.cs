using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideQuestions : MonoBehaviour
{
    public TriviaManager manager;
    public TextMeshPro anuncioAlto;
    public TextMeshPro anuncioBajo;

    // Update is called once per frame
    void Update()
    {
      
        anuncioAlto.text = "Restantes: " + manager.Restantes;
        anuncioBajo.text = "Oportunidades: " + manager.Oportunidades;

        if(manager.completed){

            anuncioAlto.text = " ";
            anuncioBajo.text = " ";
        }
    }
}
