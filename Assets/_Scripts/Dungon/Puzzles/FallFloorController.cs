using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FallFloorController : MonoBehaviour
{
    public GameObject QuestionField;
    public TextMeshPro actualQuestion;
    public float speed;
    public int correctAwnser;
    public string[] questions;
    public bool finalQuestion;
    public Rigidbody MovilePlatfrom;
    public Transform newPosition;

    // Start is called before the first frame update
    void Start()
    {
        correctAwnser = 0;
        speed = 5f;
        QuestionField = transform.GetChild(0).gameObject;
        MovilePlatfrom = transform.GetChild(1).gameObject.GetComponent<Rigidbody>();
        actualQuestion = QuestionField.GetComponent<TextMeshPro>();
        finalQuestion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finalQuestion)
        {
            ChangeQuestion(correctAwnser);
        }
        else if(finalQuestion)
        {
            MovilePlatfrom.MovePosition(Vector3.MoveTowards(MovilePlatfrom.position, newPosition.position, speed* Time.deltaTime));
        }
      
    }

    void ChangeQuestion(int numberQuestion) 
    {
        if (questions.Length == correctAwnser) 
        { 
            finalQuestion = true;
            actualQuestion.SetText("Completed");
        }
        else { actualQuestion.SetText(questions[numberQuestion]); }  
    }


}
