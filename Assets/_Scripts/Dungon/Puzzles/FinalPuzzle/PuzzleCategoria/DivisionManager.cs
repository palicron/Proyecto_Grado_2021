using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DivisionManager : MonoBehaviour
{
    [Header("Question Dependences")]
    public TextMeshPro Questiontxt;
    public TextMeshPro timeTxt;
    public GameObject[] panelOpciones;
    public PlaftormController[] platforms;
    public TextMeshPro oportunidadesTxt;
    public TextMeshPro faltantesTxt;
    public bool Inicializar;
    [Header("Timer Dependences")]
    public float tiempoInicial;
    public float tiempoRestante;
    public bool CuentaRegresiva;
    private bool changingTimer;
    [Header("Questions/Anwsers")]
    public List<QandA> QaA;
    public int current;
    public List<int> listOfAwnsers;
    [Header("Task Failed")]
    public PlaftormController platformsFailed;
    [Header("Task Completed")]
    public int oportunidades;
    public int faltantes;
    public int ContIncorrectas;
    public int ContCorrectas;
    public bool completed;


    private void Start()
    {
        CuentaRegresiva = false;
        changingTimer = false;
        ContCorrectas = 0;
        ContIncorrectas = 0;
        completed = false;
        if (Inicializar) { generateQuestion(); }
    }


    private void FixedUpdate()
    {
        oportunidadesTxt.text = "Oportunidades \n " + oportunidades;
        faltantesTxt.text = "Faltan \n " + faltantes;
        if (CuentaRegresiva)
        {
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
                timeTxt.text = "" + tiempoRestante.ToString("f0");
            }
            else
            {
                timeTxt.text = "Se acabo el tiempo";
                changeQuestionTimer();
            }
        }
    }

    public void changeQuestionTimer()
    {
        CuentaRegresiva = false;
        StartCoroutine(WaitTimerOut(1f));
    }

    public void changeQuestionChossing() 
    {
        CuentaRegresiva = false;
        StartCoroutine(WaitTimeChoosed(1.5f));
    }

    void generateQuestion()
    {
        current = Random.Range(0, QaA.Count);
        Questiontxt.text = QaA[current].question;
        if (QaA[current].type == QandA.QuestionType.ORDENAR)
        {
            Questiontxt.text = QaA[current].question + " : " + QaA[current].Anagrama();
        }
        for (int i = 0; i < panelOpciones.Length; i++)
        {
            panelOpciones[i].GetComponentInChildren<TextMeshPro>().text = "";
        }
        tiempoRestante = tiempoInicial;
        SetAwnsers();
        QaA.RemoveAt(current);
    }

    void SetAwnsers()
    {
        listOfAwnsers = new List<int>();
        CuentaRegresiva = true;
        if (QaA[current].type == QandA.QuestionType.SIMPLEANSWER)
        {
            int randomOption = 0;
            for (int i = 0; i < panelOpciones.Length; i++)
            {
                randomOption = Random.Range(0, QaA[current].opciones.Count);
                panelOpciones[i].GetComponentInChildren<TextMeshPro>().text = QaA[current].opciones[randomOption].respuestaTexto; 
                if (QaA[current].opciones[randomOption].correct)
                {
                    listOfAwnsers.Add(i);
                }
                QaA[current].opciones.RemoveAt(randomOption);
            }
        }
    }


    IEnumerator WaitTimerOut(float time)
    {
        yield return new WaitForSeconds(time);
        ContIncorrectas++;
        oportunidades--;
        generateQuestion();
    }

    IEnumerator WaitTimeChoosed(float time)
    {
        Questiontxt.text = "";
        yield return new WaitForSeconds(time);
        generateQuestion();
    }
}
