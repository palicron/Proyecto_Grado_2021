using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlasticRoomManager : MonoBehaviour
{
    [Header("Manager Dependences")]
    public TextMeshPro Questiontxt;
    public TextMeshPro timeTxt;
    public TextMeshPro timeText2;
    public GameObject[] panelOpciones;
    public TextMeshPro[] panelesOpcionesArriba;
    public MetalRoomOption[] platOpciones;
    public PlaftormController[] OpcionesMoviles;
    public PlaftormController[] platfinales;
    public PlasticQuestionManager questionManager;
    public TextMeshPro oportunidadesTxt;
    public TextMeshPro faltantesTxt;
    public bool activadoPuzzle;
    public bool Inicializar;
    [Header("Timer Dependences")]
    public float tiempoInicial;
    public float tiempoRestante;
    public bool CuentaRegresiva;
    public float waitingTime;
    public float waitTimeTrap;
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
    [Header("Trap depencies")]
    public PlaftormController gasPlat;
    

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
                timeTxt.text = "Liberando gas toxico \n " + tiempoRestante.ToString("f0");
                timeText2.text = "Liberando gas toxico \n " + tiempoRestante.ToString("f0");
            }
            else
            {
                gasPlat.active = true;
                verifyAnwsers();
            }
        }
        if (failed)
        {
            Questiontxt.text = "Fallaste";
            timeTxt.text = "";
            timeText2.text = "";
            platformsFailed.active = true;
        }
        if (completed)
        {
            CuentaRegresiva = false;
            timeTxt.text = "Completo";
            timeText2.text = "";
            Questiontxt.text = "";
            foreach (PlaftormController plat in completedPlat)
            {
                plat.active = true;

            }
            completed = false;
        }
    }

    public void ResetearPlataformas() {
        foreach (PlaftormController plat in OpcionesMoviles)
        {    
            if (plat.playerOnPlat==true) 
            {
                plat.active = true;
            }
        }
    }

    public void verifyAnwsers()
    {
        CuentaRegresiva = false;
        foreach (PlaftormController plat in platfinales)
        {
            plat.active = true;
        }
            foreach (MetalRoomOption met in platOpciones)
        {
            if (met.choosed && listOfAwnsers.Contains(met.opcion))
            {
                Questiontxt.text = "";
                timeTxt.text = "Esperando el activador";
                timeText2.text = "Gas liberado \n correcto";
                resetPanelOpciones();
                StartCoroutine(WaitTrapTime(waitTimeTrap*3f));
                ContCorrectas++;
                faltantes--;
            }
            else if (met.choosed && !listOfAwnsers.Contains(met.opcion))
            {
               
                met.activarTrampa();
                Questiontxt.text = "";
                timeTxt.text = "Esperando el activador";
                timeText2.text = "Gas liberado\n Incorrecto";
                StartCoroutine(WaitTrapTime(waitTimeTrap));
                resetPanelOpciones();
                ContIncorrectas++;
                oportunidades--;
            }
        }
        if (!escogioAlguna())
        {
            Questiontxt.text = "";
            timeTxt.text = "Esperando el activador";
            timeText2.text = "Gas liberado\n incorrecto";
            ContIncorrectas++;
            oportunidades--;
            resetPanelOpciones();
            StartCoroutine(WaitTrapTime(waitTimeTrap));
        }
        StartCoroutine(WaitTimerOut(waitingTime));
    }

    //RESETEA LOS TEXTOS DE LOS PANELES DE OPCIONES
    public void resetPanelOpciones() 
    {
        foreach (MetalRoomOption met in platOpciones)
        {
            panelOpciones[met.opcion].GetComponent<TextMeshPro>().text = "";
            panelesOpcionesArriba[met.opcion].GetComponent<TextMeshPro>().text = "";
        }
    }

    public void generateQuestion()
    {
        if (!activadoPuzzle)
        {
            activadoPuzzle = true;
        }
        current = Random.Range(0, QaA.Count);
        Questiontxt.text = QaA[current].question;
        if (QaA[current].type == QandA.QuestionType.ORDENAR)
        {
            Questiontxt.text = QaA[current].question + " : " + QaA[current].Anagrama();
        }
        for (int i = 0; i < panelOpciones.Length; i++)
        {
            panelOpciones[i].GetComponentInChildren<TextMeshPro>().text = "";
            panelesOpcionesArriba[i].GetComponentInChildren<TextMeshPro>().text = "";
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
                panelesOpcionesArriba[i].GetComponentInChildren<TextMeshPro>().text = QaA[current].opciones[randomOption].respuestaTexto;
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
        questionManager.activated = false;

    }

    IEnumerator WaitTrapTime(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (PlaftormController met in OpcionesMoviles)
        {
            met.active = true; 
        }
    }

    public bool escogioAlguna()
    {
        foreach (MetalRoomOption met in platOpciones)
        {
            if (met.choosed)
            {
                return true;
            }
        }
        return false;
    }
}
