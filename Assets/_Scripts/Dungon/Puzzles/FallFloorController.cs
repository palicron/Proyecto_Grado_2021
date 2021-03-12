using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FallFloorController : MonoBehaviour
{
    public GameObject QuestionField;
    public TextMeshPro actualQuestion;
    public int correctAwnser;
    public string[] questions;
    public bool finalQuestion;


    // Start is called before the first frame update
    void Start()
    {
        QuestionField = transform.GetChild(0).gameObject;
        actualQuestion = QuestionField.GetComponent<TextMeshPro>();
        finalQuestion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finalQuestion) { 
            ChangeQuestion(correctAwnser); 
        }
      
    }

    void ChangeQuestion(int numberQuestion) 
    {
        if (questions.Length == correctAwnser) { finalQuestion = true; }
        else { actualQuestion.SetText(questions[numberQuestion]); }  
    }

    public void addCorrectAwnser() { correctAwnser++; }
}
