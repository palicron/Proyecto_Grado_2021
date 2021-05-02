using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaperRoomManager : MonoBehaviour
{
    [Header("Manager Dependences")]
    public TextMeshPro Questiontxt;
    public TextMeshPro timeTxt;
    public GameObject[] panelOpciones;
    public PlaftormController[] platforms;
    public TextMeshPro oportunidadesTxt;
    public TextMeshPro faltantesTxt;
    public bool Inicializar;
    public PaperQuestionTrigger questionManger;
    [Header("Timer Dependences")]
    public float tiempoInicial;
    public float tiempoRestante;
    public bool CuentaRegresiva;
    public float waitingTime;
    [Header("Questions/Anwsers")]
    public List<QandA> QaA;
    public int current;
    public List<int> listOfAwnsers;
    [Header("Task Failed")]
    public PlaftormController platformsFailed;
    [Header("Task Information")]
    public int oportunidades;
    public int faltantes;
    public int ContIncorrectas;
    public int ContCorrectas;
    public bool completed;
    public bool failed;
    [Header("Completed depencies")]
    public PlaftormController[] completedPlat;


    private void Start()
    {
        CuentaRegresiva = false;
        ContCorrectas = 0;
        ContIncorrectas = 0;
        failed = false;
        completed = false;
        if (Inicializar) { generateQuestion(); }
    }


    private void FixedUpdate()
    {

        oportunidadesTxt.text = oportunidades + "\n Oportunidades";
        faltantesTxt.text = faltantes + "\n Restantes";
        if (CuentaRegresiva)
        {
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
                timeTxt.text = "" + tiempoRestante.ToString("f0");
            }
            else
            {
                Questiontxt.text = "Vuelve al activador";
                timeTxt.text = "Tiempo agotado";
                changeQuestionTimer();
            }
        }
        if ( failed) 
        {
            Questiontxt.text = "Fallaste";
            timeTxt.text = "";
            platformsFailed.active = true;
        }
        if (completed) 
        {
            CuentaRegresiva = false;
            timeTxt.text = "Completado";
            Questiontxt.text = "Ve a la siguiente fase";
            faltantesTxt.text = ">>>";
            oportunidadesTxt.text = "Reciclaste " + oportunidades + " oportunidades";
            foreach (PlaftormController plat in completedPlat)
            {
                 plat.active = true;

            }
            foreach (PlaftormController plat in platforms)
            {
                plat.active = false;
            }
        }
    }

    public void changeQuestionTimer()
    {
        CuentaRegresiva = false;
        ContIncorrectas++;
        oportunidades--;
        questionManger.activated = false;
        questionManger.stateText.text = "Activador \n en espera";
        foreach (PlaftormController palt in platforms)
        {
            palt.active = false;
        }
        StartCoroutine(WaitTimerOut(waitingTime));

    }


   public void generateQuestion()
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
        foreach (PlaftormController palt in platforms)
        {
            palt.active = true;
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
       
        //generateQuestion();
    }

}
