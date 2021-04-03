using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriviaManager : MonoBehaviour
{
    public TextMeshPro Questiontxt;
    public List<QandA> QaA;
    public GameObject[] options;
    public int current;
    public int correctAnwser;
    public Rigidbody movileCompleted;
    public Transform movilePoints;
    public bool completed;



    private void Start()
    {
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
        for (int i =0; i < options.Length; i++) 
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].GetComponent<TextMeshPro>().text = QaA[current].Awnsers[i];

            if (QaA[current].CorrectAwnser==i+1) 
            {
                correctAnwser = i+1 ;
                options[i].GetComponent<AnswerScript>().isCorrect = true;

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
