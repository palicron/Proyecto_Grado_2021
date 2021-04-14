using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FloatingManager : MonoBehaviour
{
    [Header("Manager Dependencies")]
    public List<FloatingOption> opciones;
    public List<AnswerScript> respuestas;
    public GameObject[] panelOpciones;
    [Header("Correct Information")]
    public int platCorrecSpeed;
    public Transform posicionCorrecta;
    public int plataformaCorrecta;
    public float waitTime;
    public bool active;
 

    private void Update()
    {
        if (active && respuestas.Count>0)
        {
            generateOptions();
        }
    }

    public void generateOptions() 
    {
        int randomOption = 0;
        for (int i = 0; i < panelOpciones.Length; i++)
        {
            randomOption = Random.Range(0, respuestas.Count);
            panelOpciones[i].GetComponent<TextMeshPro>().text = respuestas[randomOption].respuestaTexto;
            opciones[i].waitTime = waitTime;
            if (respuestas[randomOption].correct)
            {
                opciones[i].correct = true;
                opciones[i].speed = platCorrecSpeed;
                opciones[i].movementPoint = posicionCorrecta;
                plataformaCorrecta=i;
            }
            respuestas.RemoveAt(randomOption);
        }

    }
}
