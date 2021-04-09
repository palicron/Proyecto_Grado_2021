using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonTriviaTrigger : MonoBehaviour
{
    public PlaftormController platform;
    public PlaftormController[] movilePlatforms;
    public TriviaManager manager;
    public TextMeshPro anuncio;
    public int cantidadPreguntasTrivia;
    public int option;
    [Header("Fail Trivia Dependences")]
    public Rigidbody PuzzleRigid;
    public Transform newPostion;
    public float speed;

    private void Start()
    {
        cantidadPreguntasTrivia = manager.cantidadDePreguntas;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (option == manager.correctAnwser)
            {
                manager.preguntasRespondidasCorrectamente++;
                anuncio.text = "Correcto";
                if (manager.preguntasRespondidasCorrectamente < movilePlatforms.Length) { movilePlatforms[manager.preguntasRespondidasCorrectamente].active = true; }
                if (cantidadPreguntasTrivia == manager.preguntasRespondidasCorrectamente)
                {
                    manager.completed = true;

                }
            }
            else {
                anuncio.text = "Incorrecto";
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (manager.QaA.Count > 0) { manager.changeQuestion(); }
            else {
                PuzzleRigid.MovePosition(Vector3.MoveTowards(PuzzleRigid.position, newPostion.position, speed* Time.deltaTime ));
            }

           
        }
       
    }
}
