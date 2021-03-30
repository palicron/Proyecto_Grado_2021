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
 


    private void Start()
    {
        generateQuestion();
    }

    public void changeQuestion() 
    {
        generateQuestion(); 
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
