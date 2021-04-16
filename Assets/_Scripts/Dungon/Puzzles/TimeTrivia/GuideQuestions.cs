using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideQuestions : MonoBehaviour
{
    public TriviaManager manager;
    public TextMeshPro anuncioAlto;
    public TextMeshPro anuncioBajo;
    private int faltantes;


    // Update is called once per frame
    void Update()
    {
        faltantes = manager.cantidadDePreguntas - manager.preguntasRespondidasCorrectamente;
        anuncioAlto.text = "Correctas: " + manager.preguntasRespondidasCorrectamente;
        anuncioBajo.text = "Faltan: " + faltantes;

        if(manager.completed){

             anuncioAlto.text = " ";
            anuncioBajo.text = " ";


        }
    }
}
