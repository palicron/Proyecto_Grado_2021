using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FallinFloorManager : MonoBehaviour
{
    [Header("Screen Information")]
    public Rigidbody questionScreen;
    public Transform[] screenPositions;
    [Header("Question Dependences")]
    public TextMeshPro Questiontxt;
    public GameObject[] panelOpciones;
    public List<FloatingManager> managers;
    [Header("Position")]
    public int ActualFloatingPosition;
    [Header("Questions/Anwsers")]
    public List<QandA> QaA;
    public int current;
    [Header("Task Completed")]
    public Rigidbody movileCompleted;
    public Transform movilePoints;
    public bool completed;

    private void Start()
    {
        
        ActualFloatingPosition = 0;
        completed = false;
        generateQuestion();
    }


    private void Update()
    {
        if (completed) { movileCompleted.MovePosition(Vector3.MoveTowards(movileCompleted.position, movilePoints.position, 2f * Time.deltaTime)); }
        questionScreen.MovePosition(Vector3.MoveTowards(questionScreen.position, screenPositions[ActualFloatingPosition].position, 5f * Time.deltaTime));

    }

    public void changeQuestion()
    {
        if (QaA.Count > 0)
        {
            generateQuestion();
        }
        else {

            Questiontxt.text = "Completado";
            completed = true;
        }
    }


    void SetAwnsers()
    {
        managers[ActualFloatingPosition].respuestas = QaA[current].opciones;
        managers[ActualFloatingPosition].active = true;
    }


    void generateQuestion()
    {
        current = Random.Range(0, QaA.Count);
        Questiontxt.text = QaA[current].question;
        SetAwnsers();
        QaA.RemoveAt(current);
    }

}
