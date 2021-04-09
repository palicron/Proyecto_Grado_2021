using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriviaTrigger : MonoBehaviour
{
    public PlaftormController platform;
    public PlaftormController[] movilePlatforms;
    public TriviaManager manager;
    public int cantidadPreguntasTrivia;
    public int option;

    private void Start()
    {
        cantidadPreguntasTrivia = manager.cantidadDePreguntas;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if ( option == manager.correctAnwser) 
            {
                    manager.preguntasRespondidasCorrectamente++;
                if (manager.preguntasRespondidasCorrectamente < movilePlatforms.Length) { movilePlatforms[manager.preguntasRespondidasCorrectamente].active = true; }
                if (cantidadPreguntasTrivia == manager.preguntasRespondidasCorrectamente) 
                {
                    manager.completed = true;

                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            manager.changeQuestion();
        }
       
    }
}
