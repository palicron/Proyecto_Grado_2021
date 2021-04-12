using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriviaManager : MonoBehaviour
{


    [Header("Question Dependences")]
    public TextMeshPro Questiontxt;
    public GameObject[] panelOpciones;
    [Header("True/Flase Dependences")]
    public GameObject[] panelVF;
    [Header("Multiawnsers Dependences")]
    public int CantRespCorrectas;
    [Header("Order Dependences")]
    public string[] correctOrder;
    [Header("Questions/Anwsers")]
    public List<QandA> QaA;
    public int current;
    public int correctAnwser;
    [Header("Task Completed")]
    public Rigidbody movileCompleted;
    public Transform movilePoints;
    public int preguntasRespondidasCorrectamente;
    public int cantidadDePreguntas;
    public bool completed;


    private void Start()
    {
        CantRespCorrectas = 0;
        completed = false;
        generateQuestion();
    }

    public void changeQuestion() 
    {
        generateQuestion(); 
    }

    private void Update()
    {
        if (completed) { movileCompleted.MovePosition(Vector3.MoveTowards(movileCompleted.position, movilePoints.position, 5f * Time.deltaTime)); }
       
    }

    void SetAwnsers() 
    {

        if (QaA[current].type == QandA.QuestionType.SIMPLEANSWER || QaA[current].type == QandA.QuestionType.COMPLETAR)
        {
            CantRespCorrectas = 1;
            int randomOption = 0;
            for (int i = 0; i < panelOpciones.Length; i++)
            {
                randomOption = Random.Range(0, QaA[current].opciones.Count);
                panelOpciones[i].GetComponent<TextMeshPro>().text = QaA[current].opciones[randomOption].respuestaTexto; ;
                if (QaA[current].opciones[randomOption].correct)
                {
                    correctAnwser = i;
                }
                QaA[current].opciones.RemoveAt(randomOption);
            }
        }
        else if (QaA[current].type == QandA.QuestionType.VF)
        {
            CantRespCorrectas = 1;
            int randomOption = 0;
            for (int i = 0; i < panelVF.Length; i++)
            {
                randomOption = Random.Range(0, panelVF.Length);
                panelVF[i].GetComponent<TextMeshPro>().text = QaA[current].opciones[randomOption].respuestaTexto;

                if (QaA[current].opciones[randomOption].correct)
                {
                    correctAnwser = i;
                }
                QaA[current].opciones.RemoveAt(randomOption);
            }
        }
        else if (QaA[current].type == QandA.QuestionType.MULTPIPLEANSWERS)
        {
            CantRespCorrectas = 0;
            int randomOption = 0;
            for (int i = 0; i < panelOpciones.Length; i++)
            {
                randomOption = Random.Range(0, panelOpciones.Length);
                panelOpciones[i].GetComponent<TextMeshPro>().text = QaA[current].opciones[randomOption].respuestaTexto;
                if (QaA[current].opciones[randomOption].correct)
                {
                    CantRespCorrectas++;
                    correctAnwser = i;
                }
                QaA[current].opciones.RemoveAt(randomOption);
            }
        }
        else if (QaA[current].type == QandA.QuestionType.ORDENAR)
        {
            int randomOption = 0;
            correctOrder =new string[QaA[current].opciones.Count];
            for (int i = 0; i < panelOpciones.Length; i++)
            {
                randomOption = Random.Range(0, panelOpciones.Length);
                panelOpciones[i].GetComponent<TextMeshPro>().text = QaA[current].opciones[randomOption].respuestaTexto;
                correctOrder[QaA[current].opciones[randomOption].numeroOrden] = QaA[current].opciones[randomOption].respuestaTexto;
                QaA[current].opciones.RemoveAt(randomOption);
            }
        }
    }

    void generateQuestion() 
    {
        current = Random.Range(0, QaA.Count);
        Questiontxt.text = QaA[current].question;
        SetAwnsers();
        QaA.RemoveAt(current);
    }
}
