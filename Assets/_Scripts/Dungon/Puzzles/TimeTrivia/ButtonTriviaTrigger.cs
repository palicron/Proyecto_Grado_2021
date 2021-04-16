using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonTriviaTrigger : MonoBehaviour
{
    [Header("Trivia Movement")]
    public TriviaManager manager;
    public int cantidadPreguntasTrivia;
    public Rigidbody PuzzleRigid;
    public Transform[] PuzzlePoints;
    public TextMeshPro[] OtrosOpciones;
    public float speed;
    [Header("Button Information")]
    public TextMeshPro anuncio;
    public int option;
    [Header("Completed Dependencies")]
    public float waitTime;

    private void Start()
    {
        cantidadPreguntasTrivia = manager.cantidadDePreguntas;
    }

    void Update()
    {
        if(PuzzlePoints.Length>0 && manager.preguntasRespondidasCorrectamente < PuzzlePoints.Length) 
        {
            PuzzleRigid.MovePosition(Vector3.MoveTowards(PuzzleRigid.position, PuzzlePoints[manager.preguntasRespondidasCorrectamente].position, speed*Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (manager.listOfAwnsers.Contains(option))
            {
                manager.preguntasRespondidasCorrectamente++;
                anuncio.text = "Correcto";
                foreach(TextMeshPro ops in OtrosOpciones)
                {
                ops.text="Incorrecto";
                }
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
            if(manager.QaA.Count > 0 && manager.completed==false)
            {
                StartCoroutine(WaitToChangeQuestion(waitTime));
            }
        }

    }


     

    IEnumerator WaitToChangeQuestion(float time) {
        if(OtrosOpciones.Length!=0)
        {
            foreach(TextMeshPro ops in OtrosOpciones)
            {
                ops.text="";
            }
        }
        anuncio.text="";
        yield return new WaitForSeconds(time);
        manager.changeQuestion();

    }


}
