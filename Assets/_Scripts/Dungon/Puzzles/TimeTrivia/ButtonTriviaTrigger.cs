using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonTriviaTrigger : MonoBehaviour
{
    [Header("Trivia Movement")]
    public TriviaManager manager;   
    public Rigidbody PuzzleRigid;
    public Transform[] PuzzlePoints;
    public Transform puntoFallido;
    public TextMeshPro[] OtrosOpciones;
    public float speed;
    [Header("Button Information")]
    public TextMeshPro anuncio;
    public int option;
    [Header("Completed Dependencies")]
    public float waitTime;


    void FixedUpdate()
    {
        if(PuzzlePoints.Length>0 && manager.ContCorrectas < PuzzlePoints.Length) 
        {
            PuzzleRigid.MovePosition(Vector3.MoveTowards(PuzzleRigid.position, PuzzlePoints[manager.ContCorrectas].position, speed*Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (manager.listOfAwnsers.Contains(option))
            {
                manager.Restantes--;
                anuncio.text = "";
                manager.Questiontxt.text = "Correcto"; 
                foreach(TextMeshPro ops in OtrosOpciones)
                {
                     ops.text="";
                }
                if (manager.Restantes == 0)
                {
                    manager.completed = true;
                }   
                manager.ContCorrectas++;
            }
            else {
                manager.ContIncorrectas++;
                anuncio.text = "";
                manager.Questiontxt.text = "Incorrecto";
                manager.Oportunidades--;
                manager.ContIncorrectas++;
                if (0==manager.Oportunidades)
                {
                    StartCoroutine(WaitToComplete(waitTime/2));
                }
            }
        }

    }

       private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (manager.QaA.Count > 0 && manager.completed == false)
            {
                StartCoroutine(WaitToChangeQuestion(waitTime));
            }
            else if (manager.QaA.Count ==0 || manager.Oportunidades==0)
            {
                PuzzleRigid.MovePosition(Vector3.MoveTowards(PuzzleRigid.position, puntoFallido.position, speed * Time.deltaTime));
            }
        }
       

    }



    IEnumerator WaitToComplete(float time)
    {
        yield return new WaitForSeconds(time);
        manager.completed = true;
    }


    IEnumerator WaitToChangeQuestion(float time) {
        if(OtrosOpciones.Length!=0)
        {
            foreach(TextMeshPro ops in OtrosOpciones)
            {
                ops.text="";
            }
        }
        manager.Questiontxt.text="";
        anuncio.text="";
        yield return new WaitForSeconds(time);
        if(manager.QaA.Count !=0)
        {
            manager.changeQuestion();
        }
    }

}
