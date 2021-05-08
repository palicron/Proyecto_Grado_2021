using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MetalRoomManager : MonoBehaviour
{
    [Header("Manager Dependences")]
    public TextMeshPro Questiontxt;
    public TextMeshPro timeTxt;
    public GameObject[] panelOpciones;
    public MetalRoomOption[] platOpciones;
    public MetalQuestionTrigger questionManager;
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
    [Header("Trap depencies")]
    public PlaftormController lavaFloor;
    public PlaftormController[] platSaltos;
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
        if (activadoPuzzle) 
        {
            lavaFloor.active = true;
            foreach (PlaftormController plat in platSaltos)
            {
                plat.active = true;
            }
            activadoPuzzle = false;
        }
        if (CuentaRegresiva)
        {
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
                timeTxt.text = "" + tiempoRestante.ToString("f0");
            }
            else
            {
                verifyAnwsers();
            }
        }
        if (failed)
        {
            Questiontxt.text = "Fallaste";
            timeTxt.text = "";
            platformsFailed.active = true;
        }
        if (completed)
        {
            CuentaRegresiva = false;
            lavaFloor.active = false;
            timeTxt.text = "Completado";
            Questiontxt.text = "Pase a la siguiente fase";
            faltantesTxt.text = ">>>";
            oportunidadesTxt.text = "Reciclaste " + oportunidades + " oportunidades";
            foreach (PlaftormController plat in completedPlat)
            {
                plat.active = true;

            }
            foreach (PlaftormController plat in platSaltos)
            {
                plat.active = false;

            }
        }
    }

    public void verifyAnwsers()
    {
        CuentaRegresiva = false;
        questionManager.plat.active = true;
        foreach (PlaftormController plat in platSaltos)
        {
            plat.active = false;
        }
        foreach (MetalRoomOption met in platOpciones)
        {
            if (met.choosed && listOfAwnsers.Contains(met.opcion))
            {
                Questiontxt.text = "Vuelva al activador";
                timeTxt.text = "Correcto";  
                ContCorrectas++;
                faltantes--;
            }
            else if (met.choosed && !listOfAwnsers.Contains(met.opcion))
            {
                Questiontxt.text = "Vuelva al activador";
                timeTxt.text = "Incorrecto";
                StartCoroutine(WaitTrapTime(waitTimeTrap, met));
                ContIncorrectas++;
                oportunidades--;
            }
            panelOpciones[met.opcion].GetComponent<TextMeshPro>().text = "";
        }

        if (!escogio()) 
        {
                Questiontxt.text = "Vuelva al activador";
                timeTxt.text = "Sin elección";
                ContIncorrectas++;
                oportunidades--;
        }

        StartCoroutine(WaitTimerOut(waitingTime));
        if (faltantes==0)
        {
            completed = true;
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
        questionManager.plat.active = false;
        foreach (PlaftormController plat in platSaltos)
        {
            plat.active = true;
        }
        questionManager.stateText.text = "Activdaor \n en espera";
        questionManager.activated = false;

    }

    IEnumerator WaitTrapTime(float time, MetalRoomOption met)
    {
        met.activarTrampa();
        yield return new WaitForSeconds(time);
        met.desactivarTrampa();

    }

    public bool escogio() 
    {
        foreach (MetalRoomOption met in platOpciones) 
        {
            if (met.choosed) { return true; }
        }
        return false;
    }
}
