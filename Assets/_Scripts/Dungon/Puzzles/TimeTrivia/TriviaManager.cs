using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriviaManager : MonoBehaviour
{

    [Header("Trivia Dependences")]
    public TextMeshPro Questiontxt;
    public TextMeshPro timerTxt;
    public GameObject[] panelOpciones;
    public bool Inicializar;
    [Header("Questions/Anwsers")]
    public List<QandA> QaA;
    public int current;
    public int correctAnwser;
    public List<int> listOfAwnsers;
    [Header("Timer Dependences")]
    public float tiempoInicial;
    public float tiempoRestante;
    public bool CuentaRegresiva;
    [Header("Task Completed")]
    public PlaftormController[] movileCompleted;
    public float waitTimeCompleted;
    public int preguntasRespondidasCorrectamente;
    public int ContIncorrectas;
    public int LimiteIncorrectas;
    public int cantidadDePreguntas;
    public bool completed;


    private void Start()
    {
        CuentaRegresiva = false;
        completed = false;
        if (Inicializar) { generateQuestion(); }
    }

    public void changeQuestion() 
    {
        generateQuestion(); 
    }

    private void Update()
    {
        if (completed) 
        { 
            Questiontxt.text= "Completado";
            StartCoroutine(WaitForMove(waitTimeCompleted));
            
        }
        if (CuentaRegresiva) 
        {
            if (tiempoRestante >= 1)
            {
                tiempoRestante -= Time.deltaTime;
                timerTxt.text = "" + tiempoRestante.ToString("f0");
            }
            else 
            {
                timerTxt.text = "--";
                changeQuestion();
                CuentaRegresiva = false;
            }
        }
       
    }

    void SetAwnsers() 
    {
        listOfAwnsers = new List<int>();
        if (QaA[current].type == QandA.QuestionType.SIMPLEANSWER)
        {
            int randomOption = 0;
            for (int i = 0; i < panelOpciones.Length; i++)
            {
                randomOption = Random.Range(0, QaA[current].opciones.Count);
                panelOpciones[i].GetComponent<TextMeshPro>().text = QaA[current].opciones[randomOption].respuestaTexto; ;
                if (QaA[current].opciones[randomOption].correct)
                {
                    listOfAwnsers.Add(i);   
                }
                QaA[current].opciones.RemoveAt(randomOption);
            }
        }
    }

    void generateQuestion() 
    {
        current = Random.Range(0, QaA.Count);
        Questiontxt.text = QaA[current].question;
        for (int i = 0; i < panelOpciones.Length; i++)
        {   
            panelOpciones[i].GetComponent<TextMeshPro>().text = "";
        }
        tiempoRestante = tiempoInicial;
        SetAwnsers();
        QaA.RemoveAt(current);
    }


    IEnumerator WaitForMove(float time) {
        yield return new WaitForSeconds(time);
           for(int i=0; i < movileCompleted.Length; i++)
            {
                movileCompleted[i].active=true;

            }
    }

    
}
